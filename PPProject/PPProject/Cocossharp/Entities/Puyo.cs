using System;
using CocosSharp;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace PPProject.Cocossharp.Entities
{ 

    public class Puyo : CCSprite
    {

        //Sprite du Puyo
        private CCSprite sprite;
        private int color;
        private int column;
        private bool[] links;
        private bool verified;
      
        /*
         * Creation du Puyo avec Sprite aléatoire
         */
        public Puyo() : base()
        {
            color = RandomNumber();
            String path = String.Format("puyo/puyo-{0}", color);
            sprite = new CCSprite(path)
            {
                AnchorPoint = CCPoint.AnchorLowerLeft,
                IsAntialiased = false,
                Scale = 2.5f
            };
            links = new bool[4];
            for(int i = 0; i < 4; i++)
            {
                links[i] = false;
            }
            AddChild(sprite);
        }
        /*
         * Renvoie un nombre aléatoire
         */
        public int RandomNumber()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] b = new byte[4];
            rng.GetBytes(b);
            int n = BitConverter.ToInt32(b, 0);
            n = n % 5;
            if (n <= 0)
            {
                return RandomNumber();
            }
            else
            {
                return n;
            }

        }

        /*
         * Efface le Puyo de l'aire de jeu
         */
        public void Delete()
        {
            //removeFromPhysicsWorld()       
        }

        public float GetSpriteSize() { return sprite.ScaledContentSize.Width; }
        public void SetX(float x) { PositionX = x; }
        public void SetY(float y) { PositionY = y; }
        public void SetPosition(float x, float y){ SetX(x); SetY(y); }
        public void SetPosition(CCPoint point) { SetX(point.X); SetY(point.Y); }
        public void SetColumn(int x) { column = x; }
        public int GetColumn() { return column; }
        public int GetColor() { return color; }
        public void LowerColumn() { column--; }
        public void UpperColumn() { column++; }
        public void LinkUp() { links[0] = true; }
        public void LinkRight() { links[1] = true; }
        public void LinkDown() { links[2] = true; }
        public void LinkLeft() { links[3] = true; }
        public void NoLinkUp() { links[0] = false; }
        public void NoLinkRight() { links[1] = false; }
        public void NoLinkDown() { links[2] = false; }
        public void NoLinkLeft() { links[3] = false; }
        public bool LinkedUp() { return links[0]; }
        public bool LinkedRight() { return links[1]; }
        public bool LinkedDown() { return links[2]; }
        public bool LinkedLeft() { return links[3]; }
        public void Verify() { verified = true; }
        public void Unverify() { verified = false; }
        public bool Verified() { return verified; }
    } 
}
