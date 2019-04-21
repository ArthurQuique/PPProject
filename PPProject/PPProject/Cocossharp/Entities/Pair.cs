using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;

namespace PPProject
{
    public class Pair : CCNode
    {

        private Puyo p1; //Puyo Central
        private Puyo p2; //Puyo Extérieur
        private Grid grid; //Grille
        private CCEventListenerTouchAllAtOnce touchListener;
        private int column; //Colonne de la paire
        private int placement; //Placement du Puyo 2 par rapport au Puyo 1 : 1:dessus 2:droite 3:dessous 4:gauche
        

        public Pair(Grid grid):base()
        {            
            p1 = new Puyo();
            p2 = new Puyo();
            this.grid = grid;
           
            AddChild(p1);
            AddChild(p2);
            p2.Position = new CCPoint(p1.AnchorPointInPoints.X, p1.AnchorPointInPoints.Y + p1.GetSpriteSize()); //On commence avec le Puyo p2 au-dessus
            column = 2; //3e colonne
            placement = 1; //Puyo 2 au-dessus
           // Schedule(ApplyVelocity);

        }

        public void SetPosition(CCPoint point)
        {
            this.Position = point;
        }

        /*
         * Application de la vitesse vers le bas
         */
       public void ApplyVelocity(float timer)
        {           
            var coreAction = new CCMoveTo(0, new CCPoint(this.PositionX, this.PositionY--));
            this.AddAction(coreAction);
        }

        //Déplacement au sol
        public void GoDown()
        { 
            var coreAction = new CCMoveTo((float) .1, grid.GetPointDown(column));
            this.AddAction(coreAction);
        }

        //Déplacement à gauche
        public void GoLeft()
        {
            if (column > 0)
            {
                var coreAction = new CCMoveTo(0, new CCPoint(this.PositionX - p1.GetSpriteSize(), this.PositionY));
                this.AddAction(coreAction);
                column--;
            }
        }

        //Déplacemet à droite
        public void GoRight() {
            if (column < 5)
            {
                var coreAction = new CCMoveTo(0, new CCPoint(this.PositionX + p1.GetSpriteSize(), this.PositionY));
                this.AddAction(coreAction);
                column++;
            }
        }
        //Fonction tourner à gauche
        public void SpinL()
        {
            CCAction coreAction;
            switch (placement)
            {
                case 3: //Dessous vers droite
                    if (column < 5)
                    {
                        coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X + p1.GetSpriteSize(), p1.AnchorPointInPoints.Y));
                        p2.AddAction(coreAction);
                        placement--;
                    }
                    break;
                case 4: //Gauche vers dessous
                    coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X, p1.AnchorPointInPoints.Y - p1.GetSpriteSize()));
                    p2.AddAction(coreAction);
                    placement--;
                    break;
                case 1: //Dessus vers gauche
                    if (column > 0)
                    {
                        coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X - p1.GetSpriteSize(), p1.AnchorPointInPoints.Y));
                        p2.AddAction(coreAction);
                        placement=4;
                    }
                    break;
                case 2: //Droite vers dessus
                    coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X, p1.AnchorPointInPoints.Y + p1.GetSpriteSize()));
                    p2.AddAction(coreAction);
                    placement--;
                    break;
                default:
                    break;
            }
        }
        //Fonction tourner à droite
        public void SpinR()
        {
            CCAction coreAction;
            switch (placement)
            {
                case 1: //Dessus vers droite
                    if (column < 5)
                    {
                        coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X + p1.GetSpriteSize(), p1.AnchorPointInPoints.Y));
                        p2.AddAction(coreAction);
                        placement++;
                    }
                    break;
                case 2: //Droite vers dessous
                    coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X, p1.AnchorPointInPoints.Y-p1.GetSpriteSize()));
                    p2.AddAction(coreAction);
                    placement++;
                    break;
                case 3: //Dessous vers gauche
                    if (column > 0)
                    {
                        coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X - p1.GetSpriteSize(), p1.AnchorPointInPoints.Y));
                        p2.AddAction(coreAction);
                        placement++;
                    }
                    break;
                case 4: //Gauche vers dessus
                    coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X, p1.AnchorPointInPoints.Y + p1.GetSpriteSize()));
                    p2.AddAction(coreAction);
                    placement=1;
                    break;
                default:
                    break;       
            }
        }
      
        public CCPoint GetPositionP1() { return p1.Position; }
        public CCPoint GetPositionP2() { return p2.Position; }
    }
}
