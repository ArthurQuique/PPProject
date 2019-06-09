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
	public partial class PuzzleMenuPage : ContentPage
	{
		public PuzzleMenuPage()
		{
			InitializeComponent();
		}

        private void Puzzle1Page_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Puzzle1Page());
        }

        private void Puzzle2Page_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Puzzle2Page());
        }

    }
}