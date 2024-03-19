﻿using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using MCSkin3D.Languages;
using MCSkin3D.Swatches;

namespace MCSkin3D.Forms
{
	public partial class Splash : Form
	{
		static Thread _loaderThread;

		public Splash()
		{
			InitializeComponent();
		}

		public static string LoadingValue = "(nothing?)";

		public void SetLoadingString(string s)
		{
			LoadingValue = s;

			if (InvokeRequired)
			{
				Invoke((Action<string>)SetLoadingString, s);
				return;
			}

			label1.Text = s;
		}

		private void label1_Click(object sender, EventArgs e)
		{
		}

		Language LoadLanguages()
		{
			LanguageLoader.LoadLanguages(GlobalSettings.GetDataURI("Languages"));

			Language useLanguage = null;
			try
			{
				// stage 1 (prelim): if no language, see if our languages contain it
				if (string.IsNullOrEmpty(GlobalSettings.LanguageFile))
				{
					useLanguage =
						LanguageLoader.FindLanguage((CultureInfo.CurrentUICulture.IsNeutralCulture == false)
														? CultureInfo.CurrentUICulture.Parent.Name
														: CultureInfo.CurrentUICulture.Name);
				}

				// stage 2: load from last used language
				if (useLanguage == null)
					useLanguage = LanguageLoader.FindLanguage(GlobalSettings.LanguageFile);

				// stage 3: use English file, if it exists
				if (useLanguage == null)
					useLanguage = LanguageLoader.FindLanguage("English");
			}
			catch
			{
			}
			finally
			{
				// stage 4: fallback to built-in English file
				if (useLanguage == null)
				{
					Program.Context.SplashForm.Invoke((Action)(() => MessageBox.Show(this, "For some reason, the default language files were missing or failed to load (did you extract?) - we'll supply you with a base language of English just so you know what you're doing!")));
					useLanguage = LanguageLoader.LoadDefault();
				}
			}

			return useLanguage;
		}

		object _lockObj = new object();

		Action ErrorHandlerWrap(Action a)
		{
			return () =>
			{
				try
				{
					a();
				}
				catch (Exception ex)
				{
					Program.Context.SplashForm.Invoke((Action)(() => { Close(); }));
					Program.RaiseException(new Exception("Failed to initialize program during \"" + LoadingValue + "\"", ex));
					Application.Exit();
				}
			};
		}

		void PerformLoading()
		{
			ErrorHandlerWrap(() =>
			{
				SetLoadingString("Loading Languages...");

				var language = LoadLanguages();

				SetLoadingString("Initializing base forms...");

				Program.Context.SplashForm.Invoke(ErrorHandlerWrap(() =>
					{
						Editor.MainForm.FinishedLoadingLanguages();
						Editor.MainForm.Initialize(language);
					}));

				if (GlobalSettings.AutoUpdate)
				{
					SetLoadingString("Checking for updates...");

					if (Program.Context.Updater.CheckForUpdates())
					{
						Program.Context.SplashForm.Invoke(ErrorHandlerWrap(() =>
						{
							Editor.ShowUpdateDialog(Program.Context.SplashForm);
						}));
					}
				}

				SetLoadingString("Loading swatches...");

				SwatchLoader.LoadSwatches();
				Program.Context.SplashForm.Invoke(ErrorHandlerWrap(SwatchLoader.FinishedLoadingSwatches));

				SetLoadingString("Loading models...");

				ModelLoader.LoadModels();
				Program.Context.SplashForm.Invoke(ErrorHandlerWrap(Editor.MainForm.FinishedLoadingModels));

				SetLoadingString("Loading skins...");

				SkinLoader.LoadSkins();

				Program.Context.SplashForm.Invoke(ErrorHandlerWrap(Program.Context.DoneLoadingSplash));

				Program.Context.Form.Invoke(ErrorHandlerWrap(() =>
					{
						Program.Context.SplashForm.Close();
						GC.Collect();
					}));
			})();
		}

		public static void BeginLoaderThread()
		{
			_loaderThread = new Thread(Program.Context.SplashForm.PerformLoading);
			_loaderThread.Start();
		}

		private void Splash_Load(object sender, EventArgs e)
		{
			label4.Text = Program.Name + " v" + Program.Version.ToString();
			SetLoadingString("Doing nothing yet...");

			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Interval = 100;
			timer.Elapsed += (s, ev) => Splash.BeginLoaderThread();
			timer.AutoReset = false;
			timer.Start();
		}
	}
}
