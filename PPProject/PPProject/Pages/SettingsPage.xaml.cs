using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        private void PlaySong()
        {
            var assembly = typeof(HomePage).GetTypeInfo().Assembly;
            Stream audioFile = assembly.GetManifestResourceStream("PPProject.test.mp3");
            var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            audio.Load(audioFile);
            audio.Play();
        }

        private void StopSong()
        {
            var assembly = typeof(HomePage).GetTypeInfo().Assembly;
            Stream audioFile = assembly.GetManifestResourceStream("PPProject.test.mp3");
            var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            audio.Load(audioFile);
            audio.Stop();
        }

        private void ButtonMusic_Clicked(object sender, EventArgs e)
        {
            if(MusicON)
            {
                MusicOnOff.Text = "Music → OFF";
                MusicON = false;
                StopSong();
            }
            else
            {
                MusicOnOff.Text = "Music → ON";
                MusicON = true;
                PlaySong();
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