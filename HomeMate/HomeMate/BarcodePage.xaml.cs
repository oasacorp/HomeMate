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

        public BarcodePage ()
		{
			InitializeComponent ();
		}

        private async void ButtonScanDefault_Clicked(object sender, EventArgs e)
        {

                scanPage = new ZXingScannerPage();
                scanPage.OnScanResult += (result) => {
                    scanPage.IsScanning = false;
                    //Do Something with result

                    Device.BeginInvokeOnMainThread(() => {
                        Navigation.PopAsync();
                        DisplayAlert("Scanned Barcode", result.Text, "OK");
                    });
                };

                await Navigation.PushAsync(scanPage);
            }

    }
}