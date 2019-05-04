﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CocosSharp;

namespace PPProject
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
	{
        GameScene gameScene;
  
        /*
         * Constructeur
         */
        public GamePage ()
		{
			InitializeComponent ();
            this.Content = ALayout;
            CreateGameView(GameLayout);

            /*
             * Commandes/Appels
             */
            DownButton.Clicked += (sender, e) => gameScene.GetGameLayer().GetPair().GoDown();
            LeftButton.Clicked += (sender, e) => gameScene.GetGameLayer().GetPair().GoLeft();
            RightButton.Clicked += (sender, e) => gameScene.GetGameLayer().GetPair().GoRight();
            SpinLButton.Clicked += (sender, e) => gameScene.GetGameLayer().GetPair().SpinL();
            SpinRButton.Clicked += (sender, e) => gameScene.GetGameLayer().GetPair().SpinR();
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
                gameView.DesignResolution = new CCSizeI(600,1000);
                gameScene = new GameScene(gameView);
                gameView.RunWithScene(gameScene);                
            }
        }
	}
}