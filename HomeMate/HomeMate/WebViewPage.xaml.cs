using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeMate
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WebViewPage : ContentPage
	{ private string pagetitle; private int id;
		public WebViewPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {

            base.OnAppearing();
            Title = pagetitle;

        }

        private async void Clear_Clicked(object sender, EventArgs e)
        {



            var confirmed = await DisplayAlert("Confirm", "Are you sure you wish to clear access to "+Title+" in your phone?", "Yes", "No");
            if (confirmed)
            {
                using (var db = new SQLiteConnection(App.DB_PATH))
                {
                    //var stock = db.Execute("DELETE FROM Shortcut WHERE Title LIKE '"+Title+"'");
                    String query = "DELETE FROM Shortcut WHERE Id = " + id;
                    
                    var stock = db.Execute(query);
                    Application.Current.MainPage.Navigation.PopAsync();


                }
            }
            else
            {
            }

  

        }

        public WebViewPage(string urladdr,string _pagetitle,int _id)
        {
            InitializeComponent();
            bool status;
            pagetitle = _pagetitle;
            id = _id;
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body><h1>Connectivity Error</h1><p>Please connect to the correct Network.</p></body></html>";
           

            status = IsConnected(urladdr);
            if(status==true)
            WebView1.Source =urladdr;
            else
            WebView1.Source = htmlSource;
            //tocheck local page saying instruction
        }

        public bool IsConnected(string CheckUrl)
        {
 
            bool rtn = false; //assume no connection

            try
            {
                HttpWebRequest iRequest = (HttpWebRequest)WebRequest.Create(CheckUrl);

                iRequest.Timeout = 5000;
                iRequest.MaximumAutomaticRedirections = 100;
                iRequest.AllowAutoRedirect = true;
                iRequest.CookieContainer = new CookieContainer();
                iRequest.Method = "GET";
                iRequest.UserAgent = " Mozilla/5.0 (Windows NT 10.0; Win64; x64)";
                HttpWebResponse iResponse = iRequest.GetResponse() as HttpWebResponse;
           

                
                iResponse.Close();

              
                rtn = true;

            }
            catch (Exception g)
            {

            }
            return rtn;
        }
    }
}