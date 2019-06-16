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

            puzzle_1.Clicked += (sender, e) => Puzzle1Page_Button_Clicked(1);

            puzzle_2.Clicked += (sender, e) => Puzzle2Page_Button_Clicked(2);

        }

        private void Puzzle1Page_Button_Clicked(int i)
        {
            
            Navigation.PushAsync(new Puzzle1Page());
        }

        private void Puzzle2Page_Button_Clicked(int i)
        {

            Navigation.PushAsync(new Puzzle2Page());
        }

    }
}