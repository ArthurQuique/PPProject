using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CocosSharp;
using PPProject.Cocossharp.Scenes;

namespace PPProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrainingPage : ContentPage
    {
        GameScene gameScene;

        /*
         * Constructeur
         */
        public TrainingPage()
        {
            InitializeComponent();
            Content = ALayout;
            CreateGameView(GameLayout);

            /*
             * Commandes/Appels
             */
            DownButton.Clicked += (sender, e) => gameScene.GetGameLayer().GoDown();
            LeftButton.Clicked += (sender, e) => gameScene.GetGameLayer().GoLeft();
            RightButton.Clicked += (sender, e) => gameScene.GetGameLayer().GoRight();
            SpinLButton.Clicked += (sender, e) => gameScene.GetGameLayer().SpinL();
            SpinRButton.Clicked += (sender, e) => gameScene.GetGameLayer().SpinR();
        }

        /*
         * Création de la gameview
         */
        private void CreateGameView(AbsoluteLayout GameLayout)
        {
            var gameView = new CocosSharpView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ViewCreated = HandleViewCreated
            };
            GameLayout.Children.Add(gameView);
        }
        /*
         * Création de la game view part 2
         */
        private void HandleViewCreated(object sender, EventArgs e)
        {
            if (sender is CCGameView gameView)
            {
                gameView.DesignResolution = new CCSizeI(600, 1000);
                gameScene = new GameScene(gameView);
                gameView.RunWithScene(gameScene);
            }
        }
    }
}