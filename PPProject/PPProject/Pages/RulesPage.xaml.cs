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
	public partial class RulesPage : ContentPage
	{
		public RulesPage()
		{
			InitializeComponent();
            openRules();
		}

        private void rulesButton(object sender, EventArgs e)
        {
            openRules();
        }

        private void openRules()
        {

            /*** Cherche le fichier et le place dans une variable ***/
            var editor = new Label {};

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(RulesPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("PPProject.regles.txt");

            string text = "";
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
       
            /*** Style pour la police du fichier ***/
            editor.Text = text;
            editor.TextColor = Color.White;
            editor.FontSize = 20;
            editor.FontFamily = "Orbitron-Bold.ttf#Orbitron-Bold";

            /*** Ecrit le fichier  sur la page de l'app ***/
            Content = new ScrollView { // ScrollBar

                Content = new FlexLayout
                {

                    Margin = 20,

                    Children =
                    {
                        editor
                    }

                },
            };
        }

    }
}