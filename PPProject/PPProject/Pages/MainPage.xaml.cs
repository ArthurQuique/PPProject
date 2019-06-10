using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PPProject
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void GameMenuPage_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GameMenuPage());
        }

        private void Training_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TrainingPage());
        }

        private void RulesPage_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RulesPage());
        }

        private void Settings_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        async void Quit_Button_Clicked(object sender, EventArgs e)
        {
           Boolean response = await DisplayAlert("", "Voulez-vous quitter le jeu ?", "Oui", "Non");
           if(response)
           {
                Environment.Exit(1);
           }
        }

    }
}