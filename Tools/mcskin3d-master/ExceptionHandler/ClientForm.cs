﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MCSkin3D.ExceptionHandler
{
	public partial class ExceptionForm : Form
	{
		public ExceptionForm()
		{
			InitializeComponent();
		}

		public Exception Exception { get; set; }
		
		public void InitSizes()
		{
			int oldHeight = label3.Height;

			using (Graphics g = CreateGraphics())
			{
				SizeF size = g.MeasureString(label3.Text, label3.Font, label3.Width);
				label3.Size = new Size((int)size.Width, (int)size.Height + 12);
			}

			int offs = label3.Height - oldHeight;

			Height += offs;

			linkLabel1.Location = new Point(label2.Location.X + label2.Width - 4, label2.Location.Y);
		}

		private void ExceptionForm_Load(object sender, EventArgs e)
		{
			InitSizes();
		}

		private void panel2_Paint_1(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawLine(Pens.White, 0, 0, panel2.Width, 0);
		}

		private void panel3_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawLine(SystemPens.ControlLight, 0, 0, panel3.Width, 0);
			e.Graphics.DrawLine(SystemPens.ControlLight, 0, panel3.Height - 1, panel3.Width, panel3.Height - 1);
		}

		#region Is64BitOperatingSystem (IsWow64Process)

		/// <summary>
		/// The function determines whether the current operating system is a 
		/// 64-bit operating system.
		/// </summary>
		/// <returns>
		/// The function returns true if the operating system is 64-bit; 
		/// otherwise, it returns false.
		/// </returns>
		public static bool Is64BitOperatingSystem()
		{
			if (IntPtr.Size == 8) // 64-bit programs run only on Win64
				return true;
			else // 32-bit programs run on both 32-bit and 64-bit Windows
			{
				// Detect whether the current process is a 32-bit process 
				// running on a 64-bit system.
				bool flag;
				return ((DoesWin32MethodExist("kernel32.dll", "IsWow64Process") &&
						 IsWow64Process(GetCurrentProcess(), out flag)) && flag);
			}
		}

		/// <summary>
		/// The function determins whether a method exists in the export 
		/// table of a certain module.
		/// </summary>
		/// <param name="moduleName">The name of the module</param>
		/// <param name="methodName">The name of the method</param>
		/// <returns>
		/// The function returns true if the method specified by methodName 
		/// exists in the export table of the module specified by moduleName.
		/// </returns>
		private static bool DoesWin32MethodExist(string moduleName, string methodName)
		{
			IntPtr moduleHandle = GetModuleHandle(moduleName);
			if (moduleHandle == IntPtr.Zero)
				return false;
			return (GetProcAddress(moduleHandle, methodName) != IntPtr.Zero);
		}

		[DllImport("kernel32.dll")]
		private static extern IntPtr GetCurrentProcess();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr GetModuleHandle(string moduleName);

		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule,
													[MarshalAs(UnmanagedType.LPStr)] string procName);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process(IntPtr hProcess, out bool wow64Process);

		#endregion

		public static ErrorReport Construct(Exception topException)
		{
			ErrorReport report = new ErrorReport();

			report.Data = new List<ExceptionData>();

			report.UserData = new Dictionary<string, string>();

			for (var ex = topException; ex != null; ex = ex.InnerException)
				report.Data.Add(new ExceptionData(ex));

			// build GL info
			report.OpenGLData = new Dictionary<string, string>();

			if (!string.IsNullOrWhiteSpace(Editor.GLVendor))
				report.OpenGLData.Add("OpenGL", Editor.GLVendor + " " + Editor.GLVersion + " " + Editor.GLRenderer);
			else
				report.OpenGLData.Add("OpenGL", "Not Loaded");

			// build software info
			report.SoftwareData = new Dictionary<string, string>();

			report.SoftwareData.Add("OS", Environment.OSVersion.ToString() + " " + (Is64BitOperatingSystem() ? "x64" : "x86"));
			report.SoftwareData.Add(".NET Version", Environment.Version.ToString());
			report.SoftwareData.Add("MCSkin3D Version", Program.Version.ToString());

			return report;
		}

		private ErrorReport BuildErrorReport()
		{
			return Construct(Exception);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			SimpleReportViewer.ShowReportViewer(BuildErrorReport().ToString());
		}
	}
}