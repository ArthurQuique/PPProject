using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PPProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();
            fadeTo();
            PlaySong();
        }

        private void PlaySong()
        {
            var assembly = typeof(HomePage).GetTypeInfo().Assembly;
            Stream audioFile = assembly.GetManifestResourceStream("PPProject.Stealer.mp3");
            var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            audio.Load(audioFile);
            audio.Play();
        }

        private void StopSong()
        {
            var assembly = typeof(HomePage).GetTypeInfo().Assembly;
            Stream audioFile = assembly.GetManifestResourceStream("PPProject.Stealer.mp3");
            var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            audio.Load(audioFile);
            audio.Stop();
        }

        private async void fadeTo()
        {
            logo.Opacity = 0;
            await logo.FadeTo(1, 3000);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
            //StopSong();
        }

    }
}