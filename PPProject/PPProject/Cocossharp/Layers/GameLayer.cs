using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using CocosSharp;
using Microsoft.Xna.Framework;
using PPProject.Cocossharp.Entities;
using System.Timers;

namespace PPProject.Cocossharp.Layers
{
    public class GameLayer : CCLayer
    {

        // Define a label variable
        private Pair pair = null;
        private Puyo p;
        private Grid grid;
        private CCRect bounds;
        private static System.Timers.Timer aTimer;

        // Define CCTileMap
        // CCTileMap tileMap;


        public GameLayer()
        {
            grid = new Grid();
            AddChild(grid);
            SetTimer();
            StartGame();
        }

        //Démarrage du jeu
        public void StartGame()
        {
             /*
             * Descente de la paire
             */
            pair = new Pair(grid); //Création d'une nouvelle paire
            AddChild(pair);
            GetPairStarted(); //Place la paire au point de départ
            Schedule(ApplyVelocity); //Lance la paire
            
        }

        //Fait descendre la Paire progressivement
        private void ApplyVelocity(float timer)
        {
            pair.UpdatePointDown();
            if(pair.PositionY > pair.GetPointDown().Y)  //Si la paire est au-dessus du PointDown on la fait descendre
            {
                pair.PositionY-=2.5f;
            }
            else //Sinon on gère l'arrêt
            {
                Unschedule(ApplyVelocity);
                StopPair();
            }
        }

        public void StopPair()
        {
            grid.AddPair(pair); //On ajoute les Puyos à la grille
            RemoveChild(pair); //On supprime la Pair
            pair = null;
            grid.Chain4Loop(); //On regarde les chaines de couleur
            if (!WatchGameOver()) //On recommence le cycle si le jeu n'est pas terminé
            {
                StartGame();
            }

        }

        //Regarder si il y a un game over (3è colonne remplie)
        public bool WatchGameOver()
        {
            if (grid.GetPuyoAtPoint(2, 11) != null)
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

        public void GoDown()
        {
            if(pair != null)
            {
                pair.GoDown();
            }
        }

        public void GoLeft()
        {
            if (pair != null)
            {
                pair.GoLeft();
            }
        }

        public void GoRight()
        {
            if (pair != null)
            {
                pair.GoRight();
            }
        }

        public void SpinL()
        {
            if(pair != null)
            {
                pair.SpinL();
            }
        }

        public void SpinR()
        {
            if(pair != null)
            {
                pair.SpinR();
            }
        }


        static void SetTimer()
        {
            int secondsToWait = 3; // Attendre 20 secondes

            while (secondsToWait != 0)
            {
                Console.WriteLine("Temps: " + secondsToWait--);
                Thread.Sleep(1000);
            }

            Console.Read();
        }

        public Pair GetPair()
        {
            return pair;
        }
    }
}