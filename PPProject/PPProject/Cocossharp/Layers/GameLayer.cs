using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using PPProject.Cocossharp.Entities;

namespace PPProject.Cocossharp.Layers
{
    public class GameLayer : CCLayer
    {

        // Define a label variable
        private Pair pair = null;
        private Puyo p;
        private Grid grid;
        private CCRect bounds;
        // Define CCTileMap
       // CCTileMap tileMap;

        
        public GameLayer()
        {
            grid = new Grid();
            AddChild(grid);
            StartGame();
        }

        //Démarrage du jeu
        public void StartGame()
        {
            if (!WatchGameOver()) 
            {
                /*
                 * Descente de la paire
                 */
                pair = new Pair(grid); //Création d'une nouvelle paire
                AddChild(pair);
                GetPairStarted(); //Place la paire au point de départ
                pair.TurnOnVelocity();
                /*
                 * Arrivée en bas
                 */
                 
            }
        }

        //Regarder si il y a un game over (3è colonne remplie)
        public bool WatchGameOver()
        {
            if (grid.GetPuyoAtPoint(2, 12) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * Placement de la paire
         */
        public void GetPairStarted()
        {
            pair.Position = grid.GetStartingPoint();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            // Use the bounds to layout the positioning of our drawable assets
            bounds = VisibleBoundsWorldspace;
            ContentSize = new CCSize(bounds.Size.Width, bounds.Size.Height);
            
            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce
            {
                OnTouchesEnded = OnTouchesEnded
            };
            AddEventListener(touchListener, this);
        }
        
        private void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                // Perform touch handling here
            }
        }
        
      
        public Pair GetPair()
        {
            return pair;
        }
    }
}