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
	public partial class PuzzleMenuPage : ContentPage
	{
		public PuzzleMenuPage()
		{
			InitializeComponent();

            puzzle_1.Clicked += (sender, e) => Puzzle1Page_Button_Clicked(1);

        }

        private void Puzzle1Page_Button_Clicked(int i)
        {
            
            Navigation.PushAsync(new Puzzle1Page(i));
        }

    }
}