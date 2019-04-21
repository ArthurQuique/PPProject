using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace PPProject
{
    public class GameLayer : CCLayer
    {

        // Define a label variable
        private CCLabel label;
        private Pair pair;
        private Puyo p;
        private Grid grid;
        // Define CCTileMap
       // CCTileMap tileMap;

        
        public GameLayer()
        {

            
            grid = new Grid();
            pair = new Pair(grid);
            pair.SetPosition(grid.GetStartingPoint()); //Place la paire au point de départ
           
            this.AddChild(grid);
            this.AddChild(pair);



        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;

            
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