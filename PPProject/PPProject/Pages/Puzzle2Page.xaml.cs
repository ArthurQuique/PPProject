using System;
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
    public partial class Puzzle2Page : ContentPage
    {
        PuzzleScene gameScene;
        string[] puzzle;

        /*
         * Constructeur
         */
        public Puzzle2Page()
        {
            int i = 2;
            puzzle = OpenPuzzle(i);
            InitializeComponent();
            Content = ALayout;
            CreateGameView(GameLayout);

            /*
             * Commandes/Appels
             */
            DownButton.Clicked += (sender, e) => gameScene.GetPuzzleLayer().GetPair().GoDown();
            LeftButton.Clicked += (sender, e) => gameScene.GetPuzzleLayer().GetPair().GoLeft();
            RightButton.Clicked += (sender, e) => gameScene.GetPuzzleLayer().GetPair().GoRight();
            SpinLButton.Clicked += (sender, e) => gameScene.GetPuzzleLayer().GetPair().SpinL();
            SpinRButton.Clicked += (sender, e) => gameScene.GetPuzzleLayer().GetPair().SpinR();
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
            Stream stream = assembly.GetManifestResourceStream("PPProject.Cocossharp.Puzzles.Puzzle_2.txt");

            string[] text;
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd().Split(')');
            }
            return text;
        }
    }
}