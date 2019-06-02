using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using PPProject.CocosSharp.Actions;

namespace PPProject.Cocossharp.Entities
{
    public class Pair : CCNode
    {

        private Puyo p1; //Puyo Central
        private Puyo p2; //Puyo Extérieur
        private Grid grid; //Grille
        private CCEventListenerTouchAllAtOnce touchListener;
        private int placement; //Placement du Puyo 2 par rapport au Puyo 1 : 1:dessus 2:droite 3:dessous 4:gauche
        private CCPoint pivot;
        private CCMoveTo moveAction;
        private CCPoint pointDown;
        

        /*
         * A commenter pour le produit final
         */
        private CCDrawNode collisionBox = new CCDrawNode(); //représentation graphique du rectangle bounds
        
        
        
        public Pair(Grid grid):base()
        {            
            p1 = new Puyo();
            p2 = new Puyo();
            this.grid = grid;
           
            AddChild(p1);
            AddChild(p2);
            p2.Position = new CCPoint(p1.AnchorPointInPoints.X, p1.AnchorPointInPoints.Y + p1.GetSpriteSize()); //On commence avec le Puyo p2 au-dessus
            p1.SetColumn(2);
            p2.SetColumn(2); //3e colonne
            placement = 1; //Puyo 2 au-dessus
            ContentSize = new CCSize(p1.ContentSize.Width, p1.ContentSize.Height * 2);
            UpdatePointDown();
            collisionBox = new CCDrawNode();
            collisionBox.DrawRect(BoundingBox, CCColor4B.Transparent, 2f, CCColor4B.Red);    
            AddChild(collisionBox);
            

        }

        public void SetPosition(CCPoint point)
        {
            var coreAction = new CCMoveTo(0, point);
            AddAction(coreAction);
            UpdatePointDown();
        }

        public void TurnOnVelocity()
        {
            UpdatePointDown();
            moveAction = new CCMoveTo(PositionY / 300f, pointDown);
            AddAction(moveAction);
           
        }

        //Déplacement au sol
        public void GoDown()
        {
            UpdatePointDown();
            StopAllActions();
            var coreAction = new CCMoveTo(0.1f, pointDown);
            AddAction(coreAction);
        }

        //Déplacement à gauche
        public void GoLeft()
        {
            if (p1.GetColumn() > 0 && p2.GetColumn() > 0)
            {
                StopAllActions();
                p1.LowerColumn();
                UpdatePointDown();
                var coreAction = new CCMoveBy(0.025f, new CCPoint(-p1.GetSpriteSize(), 0));
                moveAction = new CCMoveTo(PositionY / 300f, pointDown);
                CCSequence sequence = new CCSequence(coreAction, moveAction);
                RunAction(sequence);
            }
        }

        //Déplacement à droite
        public void GoRight()
        {
            if (p1.GetColumn() < 5 && p2.GetColumn() < 5)
            {
                StopAllActions();
                p1.UpperColumn();
                UpdatePointDown();
                var coreAction = new CCMoveBy(0.025f, new CCPoint(p1.GetSpriteSize(), 0));
                moveAction = new CCMoveTo(PositionY / 300f, pointDown);
                CCSequence sequence = new CCSequence(coreAction, moveAction);
                RunAction(sequence);
            }
        }

        //Fonction tourner à gauche
        public void SpinL()
        {
            CCAction coreAction;
            switch (placement)
            {
                case 3: //Dessous vers droite
                    coreAction = new CCRotateAroundTo(0.025f, pivot, 0, 1);
                    p2.AddAction(coreAction);
                    placement--;
                    UpdatePointDown();
                    UpdatePointDown();
                    moveAction = new CCMoveTo(PositionY / 300f, pointDown);
                    RunAction(moveAction);
                    break;
                case 4: //Gauche vers dessous
                    coreAction = new CCRotateAroundTo(0.025f, pivot, 270, 1);
                    p2.AddAction(coreAction);
                    placement--;
                    UpdatePointDown();
                    moveAction = new CCMoveTo(PositionY / 300f, pointDown);
                    RunAction(moveAction);
                    break;
                case 1: //Dessus vers gauche
                    coreAction = new CCRotateAroundTo(0.025f, pivot, 180, 1);
                    p2.AddAction(coreAction);
                    placement=4;
                    break;
                case 2: //Droite vers dessus
                    coreAction = new CCRotateAroundTo(0.025f, pivot, 90, 1);
                    p2.AddAction(coreAction);
                    placement--;
                    break;
                default:
                    break;
            }
            UpdatePointDown();
        }

        //Fonction tourner à droite
        public void SpinR()
        {
            CCAction coreAction;
            switch (placement)
            {
                case 1: //Dessus vers droite
                    coreAction = new CCRotateAroundTo(0.025f, pivot, 0);
                    p2.AddAction(coreAction);
                    placement++;
                    break;
                case 2: //Droite vers dessous
                    coreAction = new CCRotateAroundTo(0.025f, pivot, 270);
                    p2.AddAction(coreAction);
                    placement++;
                    UpdatePointDown();
                    moveAction = new CCMoveTo(PositionY/300f, pointDown);
                    RunAction(moveAction);
                    break;
                case 3: //Dessous vers gauche
                    coreAction = new CCRotateAroundTo(0.025f, pivot, 180);
                    p2.AddAction(coreAction);
                    placement++;
                    UpdatePointDown();
                    moveAction = new CCMoveTo(PositionY / 300f, pointDown);
                    RunAction(moveAction);
                    break;
                case 4: //Gauche vers dessus
                    coreAction = new CCRotateAroundTo(0.025f, pivot, 90);
                    p2.AddAction(coreAction);
                    placement =1;
                    break;
                default:
                    break;       
            }
            UpdatePointDown();
        }
      

       
        public Puyo GetP1() { return p1; } //Retourne le Puyo p1
        public Puyo GetP2() { return p2; } //Retourne le Puyo p2
        public CCPoint GetPositionP1() { return p1.Position; }
        public CCPoint GetPositionP2() { return p2.Position; }
        public int GetPlacement() { return placement; }
        public CCPoint GetPointDown() { return pointDown; }
        

        public void UpdateColumnP2()
        {
            switch (placement)
            {
                case 1: p2.SetColumn(p1.GetColumn());
                    break;
                case 2: p2.SetColumn(p1.GetColumn() + 1);
                    break;
                case 3: p2.SetColumn(p1.GetColumn());
                    break;
                case 4: p2.SetColumn(p1.GetColumn() - 1);
                    break;
                default:
                    break;
            }
        }

        public void UpdatePointDown()
        {
            pointDown = grid.GetPointDown(p1.GetColumn());
            if (placement == 3)
            {
                pointDown = new CCPoint(pointDown.X, pointDown.Y + p1.GetSpriteSize());
            }
        }
    }

    
}
