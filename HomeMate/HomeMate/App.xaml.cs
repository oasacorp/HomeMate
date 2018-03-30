using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace HomeMate
{
	public partial class App : Application

	{
        public static string DB_PATH = string.Empty;

		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new HomeMate.MainPage());
		}

        public App(string _DB_PATH)
        {
            InitializeComponent();
            DB_PATH = _DB_PATH;
            MainPage = new NavigationPage(new HomeMate.MainPage());



        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
