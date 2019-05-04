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
        private CCRect bounds; //Rectangle représentant les bords de la paire

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
            column = 2; //3e colonne
            placement = 1; //Puyo 2 au-dessus
            this.ContentSize = new CCSize(p1.ContentSize.Width, p1.ContentSize.Height * 2);
            bounds = new CCRect(this.PositionX, this.PositionY, this.ContentSize.Width, this.ContentSize.Height);
        }

        public void SetPosition(CCPoint point)
        {
            var coreAction = new CCMoveTo(0, point);
            this.AddAction(coreAction);
        }

        public void TurnOnVelocity()
        {
            Schedule(ApplyVelocity);
        }
        

        /*
         * Application de la vitesse vers le bas
         */
       public void ApplyVelocity(float time)
        {
            PositionY -= 50 * time; //Descente
            UpdateBounds();
            /*
             * Affichage du rectangle bounds (à commenter sur le produit final)
             */
            /*Console.WriteLine(bounds);
            RemoveChild(collisionBox);
            collisionBox = new CCDrawNode();
            collisionBox.DrawRect(this.bounds, CCColor4B.Transparent, 2f, CCColor4B.Red);
            AddChild(collisionBox);*/
            
            
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
                        this.ContentSize = new CCSize(p1.ContentSize.Width * 2, p1.ContentSize.Height);

                    }
                    break;
                case 4: //Gauche vers dessous
                    coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X, p1.AnchorPointInPoints.Y - p1.GetSpriteSize()));
                    p2.AddAction(coreAction);
                    placement--;
                    this.ContentSize = new CCSize(p1.ContentSize.Width, p1.ContentSize.Height * 2);
                    break;
                case 1: //Dessus vers gauche
                    if (column > 0)
                    {
                        coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X - p1.GetSpriteSize(), p1.AnchorPointInPoints.Y));
                        p2.AddAction(coreAction);
                        placement=4;
                        this.ContentSize = new CCSize(p1.ContentSize.Width * 2, p1.ContentSize.Height);
                    }
                    break;
                case 2: //Droite vers dessus
                    coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X, p1.AnchorPointInPoints.Y + p1.GetSpriteSize()));
                    p2.AddAction(coreAction);
                    placement--;
                    this.ContentSize = new CCSize(p1.ContentSize.Width, p1.ContentSize.Height * 2);
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
                        this.ContentSize = new CCSize(p1.ContentSize.Width * 2, p1.ContentSize.Height);
                    }
                    break;
                case 2: //Droite vers dessous
                    coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X, p1.AnchorPointInPoints.Y-p1.GetSpriteSize()));
                    p2.AddAction(coreAction);
                    placement++;
                    this.ContentSize = new CCSize(p1.ContentSize.Width, p1.ContentSize.Height * 2);
                    break;
                case 3: //Dessous vers gauche
                    if (column > 0)
                    {
                        coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X - p1.GetSpriteSize(), p1.AnchorPointInPoints.Y));
                        p2.AddAction(coreAction);
                        placement++;
                        this.ContentSize = new CCSize(p1.ContentSize.Width*2, p1.ContentSize.Height);
                    }
                    break;
                case 4: //Gauche vers dessus
                    coreAction = new CCMoveTo(0, new CCPoint(p1.AnchorPointInPoints.X, p1.AnchorPointInPoints.Y + p1.GetSpriteSize()));
                    p2.AddAction(coreAction);
                    placement=1;
                    this.ContentSize = new CCSize(p1.ContentSize.Width, p1.ContentSize.Height * 2);
                    break;
                default:
                    break;       
            }
        }
      
        public void UpdateBounds()
        {
            this.bounds= new CCRect(this.PositionX, this.PositionY, this.ContentSize.Width, this.ContentSize.Height);
        }

        public CCPoint GetPositionP1() { return p1.Position; }
        public CCPoint GetPositionP2() { return p2.Position; }
    }
}
