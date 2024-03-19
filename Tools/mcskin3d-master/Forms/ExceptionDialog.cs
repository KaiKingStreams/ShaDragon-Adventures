﻿//
//    MCSkin3D, a 3d skin management studio for Minecraft
//    Copyright (C) 2013 Altered Softworks & MCSkin3D Team
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace Paril.Windows.Dialogs
{
	public partial class ExceptionDialog : Form
	{
		private readonly Exception _exception;

		public ExceptionDialog()
		{
			InitializeComponent();
		}

		public ExceptionDialog(Exception e) :
			this()
		{
			_exception = e;
			exceptionName.Text = e.GetType().FullName;

			// general
			generalMessage.Text = e.Message;
			generalSource.Text = e.Source;
			generalTargetMethod.Text = FormatMethodBase(e.TargetSite);
			generalHelpLink.Text = e.HelpLink;

			// stack
			stackTrace.Text = e.StackTrace;

			// inner
			TreeNode curNode = null;
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				var node = new TreeNode(ex.GetType().ToString());

				if (curNode != null)
					curNode.Nodes.Add(node);
				else
					treeView1.Nodes.Add(node);

				curNode = node;

				var exceptionNode = new TreeNode(ex.Message);
				var messageNode = new TreeNode(FormatMethodBase(ex.TargetSite));

				curNode.Nodes.Add(exceptionNode);
				curNode.Nodes.Add(messageNode);
			}
		}

		public static string FormatMethodBase(MethodBase method)
		{
			if (method == null)
				return "Unknown";

			string str = "[" + method.Module.Name + "]" + " " + method;

			str += "(";
			bool started = false;
			foreach (ParameterInfo x in method.GetParameters())
			{
				if (started)
					str += ", ";
				else
					started = true;

				str += x.ParameterType + " " + x.Name;
			}
			str += ")";

			return str;
		}

		public static void Show(Exception e)
		{
			var d = new ExceptionDialog(e);
			SystemSounds.Asterisk.Play();
			d.ShowDialog();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog())
			{
				sfd.RestoreDirectory = true;
				sfd.Filter = "Text files (*.txt)|*.txt";
				sfd.RestoreDirectory = true;

				if (sfd.ShowDialog() == DialogResult.OK)
					File.WriteAllText(sfd.FileName, _exception.ToString());
			}
		}

		private void ExceptionDialog_Load(object sender, EventArgs e)
		{
		}
	}
}