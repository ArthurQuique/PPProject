﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPProject
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{

		public HomePage ()
		{
            InitializeComponent();
            fadeTo();
        }

        private async void fadeTo()
        {
            logo.Opacity = 0;
            await logo.FadeTo(1, 3000);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

    }


}