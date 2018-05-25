using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace HomeMate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodePage : ContentPage
    {
        ZXingScannerPage scanPage;
        public string barcoderesult = string.Empty;

        private int id;
        private Shortcut s;
        int flag=0;
        public BarcodePage()
        {
           
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            Title = "Add Area";

        }


        public BarcodePage(int _id)
        {
            id = _id;
            s = new Shortcut();
            InitializeComponent();
            //iconPicker.SelectedIndex = 1;
        }

        private async void ButtonScanDefault_Clicked(object sender, EventArgs e)
        {
            
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;
                //Do Something with result

                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    barcoderesult = result.Text;
                    s = Split_Desplit.Split(barcoderesult);
                    ShortcutTitle.Text = s.Title;
                    iconPicker.SelectedIndex = s.Icon;
                    
                    if (s.Title == "NaN")
                    {
                        DisplayAlert("Failure", "Incorrect Barcode Format", "Ok");
                        
                    }
                    else
                    {
                        ShortcutAddress.Text = Crypto.Encrypt(s.Address);
                        flag = 1;

                    }
                });
            };

            await Navigation.PushAsync(scanPage);

        }

        private void AddtoDB_Clicked(object sender, EventArgs e)
        {

           

            if (flag == 1)
            {

                //   if (barcoderesult != string.Empty)
                // {
                Shortcut shortcut = new Shortcut()
                {
                    Address = ShortcutAddress.Text,
                    Id = id,
                    Title = ShortcutTitle.Text,
                    Icon = iconPicker.SelectedIndex
                };

                using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.DB_PATH))
                {
                    conn.CreateTable<Shortcut>();
                    try
                    {
                        var numberOfRows = conn.Insert(shortcut);

                        if (numberOfRows > 0)
                        {
                            DisplayAlert("Success", "Successfully Added", "Ok");
                           
                            Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                            DisplayAlert("Oops", "Something went wrong!", "Ok");

                        }
                    }

                    catch (SQLiteException ex)
                    {//tocheck on how to use
                        conn.Update(shortcut);
                        DisplayAlert("Success", "Updated", "Ok");
                        Application.Current.MainPage.Navigation.PopAsync();


                    }

                }
            }
            else
            {
                if (ShortcutAddress.Text == "")
                {
                    DisplayAlert("Failure", "No valid Barcode Scanned", "Ok");
                    flag = 0;

                }
                else
                {
                    s = Split_Desplit.Split(ShortcutAddress.Text);

                    if (s.Title == "NaN")
                    {
                        DisplayAlert("Failure", "Incorrect Format", "Ok");
                        flag = 0;
                    }
                    else
                    {
                        ShortcutAddress.Text = Crypto.Encrypt(s.Address);
                        if (ShortcutTitle.Text==null)
                        {
                            ShortcutTitle.Text = s.Title;
                            iconPicker.SelectedIndex = s.Icon;
                        }
                        DisplayAlert("Success", "Please confirm again to save", "Ok");
                        flag = 1;

                    }
                }
                

            }


        }
    
    }
}
