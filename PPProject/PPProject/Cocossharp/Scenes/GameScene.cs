using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using Microsoft.Xna.Framework.Graphics;
using PPProject.Cocossharp.Layers;

namespace PPProject.Cocossharp.Scenes
{
    public class GameScene : CCScene
    {

        GameLayer gameLayer;

        public GameScene(CCGameView gameView) : base(gameView)
        {
            //Création de l'arrière-plan
            var backgroundLayer = new CCLayer();
            CreateBackground(gameView, backgroundLayer);
            AddChild(backgroundLayer);
            //Création du Layer de jeu
            gameLayer = new GameLayer();
            AddChild(gameLayer);
        }

        /*
         * Ajout de l'arrière-plan
         */
        private void CreateBackground(CCGameView gameview, CCLayer backgroundLayer)
        {
            var background = new CCSprite("settingsBackground.png")
            { 
                AnchorPoint = new CCPoint(0, 0),
                IsAntialiased = false,
                ContentSize = new CCSizeI(500, 1000),
            };
            backgroundLayer.AddChild(background);
        }

        public GameLayer GetGameLayer()
        {
            return gameLayer;
        }

    }
}
