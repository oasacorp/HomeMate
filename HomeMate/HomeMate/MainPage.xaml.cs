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
			InitializeComponent();
		}

        private void Button1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WebViewPage(true));
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BarcodePage());
        }

    }
}
