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
    public class PuzzleLayer : CCLayer
    {

        // Define a label variable
        private int HEI = 13;
        private int WID = 6;
        private Pair pair = null;
        private Pair waitingPair = null;
        private Pair waitingPair2 = null;
        private Puyo p;
        private Grid grid;
        private CCRect bounds;
        private CCSprite frame;
        private int score;
        private CCLabel scoreLabel;
        private string[] puzzle;
        private int[,] colorGrid;


        private static System.Timers.Timer aTimer;

        public PuzzleLayer(string[] puzzle)
        {
            this.puzzle = puzzle;
            colorGrid = new int[WID , HEI];
            CreatePuzzleEnvironment();

            score = 0;
            grid = new Grid(colorGrid);
            frame = new CCSprite("frame.png");
            frame.AnchorPoint = CCPoint.AnchorLowerLeft;
            frame.Position = new CCPoint(0, 0);
            frame.Scale = 0.85f;
            waitingPair = new Pair(grid);
            waitingPair2 = new Pair(grid);
            AddChild(grid);
            AddChild(frame);
            //SetTimer();
            StartGame();
        }

        public void CreatePuzzleEnvironment()
        {
            //Création des Puyos dans la grille
            for(int i = 0; i < HEI; i++)
            {
                string[] tokens = puzzle[i].Split(',');
                int[] colorGridTemp = Array.ConvertAll<string, int>(tokens, int.Parse);
                for(int j = 0; j < WID; j++)
                {
                    colorGrid[j, i] = colorGridTemp[j];
                }
            }
        }

        //Démarrage du jeu
        public void StartGame()
        {
            /*
            * Descente de la paire
            */
            scoreLabel = new CCLabel(String.Format("Score: {0}", score), " ", 12);
            scoreLabel.AnchorPoint = CCPoint.AnchorLowerLeft;
            scoreLabel.Position = new CCPoint(27, 982);
            pair = new Pair(grid, waitingPair.GetP1().GetColor(), waitingPair.GetP2().GetColor());
            waitingPair = new Pair(grid, waitingPair2.GetP1().GetColor(), waitingPair2.GetP2().GetColor());
            waitingPair2 = new Pair(grid);
            waitingPair.Position = new CCPoint(507, 802);
            waitingPair2.Position = new CCPoint(507, 625);
            AddChild(pair);
            AddChild(waitingPair);
            AddChild(waitingPair2);

            AddChild(scoreLabel);
            GetPairStarted(); //Place la paire au point de départ
            Schedule(ApplyVelocity); //Lance la paire

        }

        //Fait descendre la Paire progressivement
        private void ApplyVelocity(float timer)
        {
            pair.UpdatePointDown();
            if (pair.PositionY > pair.GetPointDown().Y)  //Si la paire est au-dessus du PointDown on la fait descendre
            {
                pair.PositionY -= 2.5f;
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
            score = grid.Chain4Loop(score); //On regarde les chaines de couleur
            if (!WatchGameOver()) //On recommence le cycle si le jeu n'est pas terminé
            {
                RemoveChild(scoreLabel);
                StartGame();
            }
            else
            {
                GameOver();
            }

        }

        public void GameOver()
        {
            RemoveChild(grid);
            var label = new CCLabel("Game Over\n", " ", 80);
            var label2 = new CCLabel("Votre score est de\n" + score, " ", 40, CCTextAlignment.Center);
            label.Color = CCColor3B.White;
            label.PositionX = 250;
            label.PositionY = 500;
            label2.PositionX = 250;
            label2.PositionY = 300;
            AddChild(label);
            AddChild(label2);
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
            if (pair != null)
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
            if (pair != null)
            {
                pair.SpinL();
            }
        }

        public void SpinR()
        {
            if (pair != null)
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