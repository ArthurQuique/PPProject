using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPProject
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameMenuPage : ContentPage
	{
		public GameMenuPage()
		{
			InitializeComponent();
		}

        private void GamePage_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GamePage());
        }

        private void PuzzleMenuPage_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PuzzleMenuPage());
        }

    }
}