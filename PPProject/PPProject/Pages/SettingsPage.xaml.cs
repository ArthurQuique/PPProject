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
    public partial class SettingsPage : ContentPage
    {
        bool MusicON = true;
        bool SoundON = true;

        public SettingsPage()
        {
            InitializeComponent();
        }

        private void ButtonMusic_Clicked(object sender, EventArgs e)
        {
            if(MusicON)
            {
                MusicOnOff.Text = "Music → OFF";
                MusicON = false;
            }
            else
            {
                MusicOnOff.Text = "Music → ON";
                MusicON = true;
            }
        }

        private void ButtonSound_Clicked(object sender, EventArgs e)
        {
            if (SoundON)
            {
                SoundOnOff.Text = "Sound → OFF";
                SoundON = false;
            }
            else
            {
                SoundOnOff.Text = "Sound → ON";
                SoundON = true;
            }
        }

    }
}