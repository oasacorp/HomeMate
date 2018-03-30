using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HomeMate
{
	public partial class MainPage : ContentPage
	{


        public MainPage()


		{
            
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
            {

               conn.CreateTable<Shortcut>();
            

            }

            
            InitializeComponent();


        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            Title = "Home Mate";
            loadButton();
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            string test = Split_Desplit.DeSplit("Reddit", "https://www.reddit.com", 3);
            Console.WriteLine(test + "\n");
            test = Split_Desplit.DeSplit("Gmail", "http://mail.google.com", 3);
            Console.WriteLine(test + "\n");
            test = Split_Desplit.DeSplit("Test IP", "http://192.168.20.20", 3);
            Console.WriteLine (test + "\n");
            DisplayAlert("yes", test, "OK");
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            shortcutclicked(2);
        }

        private void Button3_Clicked(object sender, EventArgs e)
        {
            shortcutclicked(3);
        }
        private void Button4_Clicked(object sender, EventArgs e)
        {
            shortcutclicked(4);
        }

        private void Button5_Clicked(object sender, EventArgs e)
        {
            shortcutclicked(5);
        }


        private void Button6_Clicked(object sender, EventArgs e)
        {
            using (var db = new SQLiteConnection(App.DB_PATH))
            {
                var stock = db.Execute("DELETE FROM Shortcut");
                loadButton();
              
            }

        }

        private void shortcutclicked(int id)
        {
            //use using {}
            try
            {
                using (var db = new SQLiteConnection(App.DB_PATH))
                {
                    var stock = db.Get<Shortcut>(id);
                    string navurl = Crypto.Decrypt(stock.Address);
                    Navigation.PushAsync(new WebViewPage(navurl,stock.Title));

                }
            }
            catch (SQLiteException e)
            {
                Navigation.PushAsync(new BarcodePage(id));
            }

            catch(System.InvalidOperationException e)
            {
                Navigation.PushAsync(new BarcodePage(id));

            }
        }

        public void loadButton()
        {
            //use using {}
            string[] buttonText = new string[6];
            var db = new SQLiteConnection(App.DB_PATH);
            for (int i = 1; i <= 6; i++)
            {
                try
                {
                    
                    var stock = db.Get<Shortcut>(i);
                    buttonText[i - 1] = stock.Title;

                }
           
            catch (SQLiteException e)
            {
                    buttonText[i - 1] = "Add";
                }

            catch (System.InvalidOperationException e)
            {
                buttonText[i - 1] = "Add";

            }
            }
            s1.Text = "Encrypt";
            s2.Text = buttonText[1];
            s3.Text = buttonText[2];
            s4.Text = buttonText[3];
            s5.Text = buttonText[4];
            s6.Text = "Clear";



        }
    }
}
