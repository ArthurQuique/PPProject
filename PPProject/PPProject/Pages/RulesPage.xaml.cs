using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
   
		}

       // String fileContent;

       /* private void openRules()
            {
            using (StreamReader sr = File.OpenText("regles.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    fileContent = sr.ReadToEnd();
                }
            }
        }*/
	}
}