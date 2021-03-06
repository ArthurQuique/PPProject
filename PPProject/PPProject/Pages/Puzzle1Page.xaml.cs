﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CocosSharp;
using PPProject.Cocossharp.Scenes;
using System.Reflection;
using System.IO;

namespace PPProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Puzzle1Page : ContentPage
    {
        PuzzleScene gameScene;
        string[] puzzle;

        /*
         * Constructeur
         */
        public Puzzle1Page(int i) 
        {
            puzzle = OpenPuzzle(i);
            InitializeComponent();
            Content = ALayout;
            CreateGameView(GameLayout);

            /*
             * Commandes/Appels
             */
            DownButton.Clicked += (sender, e) => gameScene.GetPuzzleLayer().GoDown();
            LeftButton.Clicked += (sender, e) => gameScene.GetPuzzleLayer().GoLeft();
            RightButton.Clicked += (sender, e) => gameScene.GetPuzzleLayer().GoRight();
            SpinLButton.Clicked += (sender, e) => gameScene.GetPuzzleLayer().SpinL();
            SpinRButton.Clicked += (sender, e) => gameScene.GetPuzzleLayer().SpinR();
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
                gameScene = new PuzzleScene(gameView, puzzle);
                gameView.RunWithScene(gameScene);
            }
        }

        private string[] OpenPuzzle(int i)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(RulesPage)).Assembly;
            string puzzleName = String.Format("PPProject.Cocossharp.Puzzles.Puzzle_{0}.txt", i);
            Stream stream = assembly.GetManifestResourceStream(puzzleName);

            string[] text;
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd().Split(')');
            }
            return text;
        }
    }
}