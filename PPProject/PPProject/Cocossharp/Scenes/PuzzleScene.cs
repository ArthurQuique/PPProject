using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using Microsoft.Xna.Framework.Graphics;
using PPProject.Cocossharp.Layers;

namespace PPProject.Cocossharp.Scenes
{
    public class PuzzleScene : CCScene
    {

        PuzzleLayer puzzleLayer;

        public PuzzleScene(CCGameView gameView, string[] puzzle) : base(gameView)
        {
            //Création de l'arrière-plan
            var backgroundLayer = new CCLayer();
            CreateBackground(gameView, backgroundLayer);
            AddChild(backgroundLayer);
            //Création du Layer de jeu
            puzzleLayer = new PuzzleLayer(puzzle);
            AddChild(puzzleLayer);
        }

        /*
         * Ajout de l'arrière-plan
         */
        private void CreateBackground(CCGameView gameview, CCLayer backgroundLayer)
        {
            var background = new CCSprite("background.png")
            {
                AnchorPoint = new CCPoint(0, 0),
                IsAntialiased = false,
                ContentSize = new CCSizeI(500, 1000)
            };
            backgroundLayer.AddChild(background);
        }

        public PuzzleLayer GetPuzzleLayer()
        {
            return puzzleLayer;
        }


    }
}