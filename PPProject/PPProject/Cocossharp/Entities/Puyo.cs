using System;
using CocosSharp;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace PPProject
{ 

    public class Puyo : CCNode
    {

        //Sprite du Puyo
        private CCSprite sprite;
      
        /*
         * Creation du Puyo avec Sprite aléatoire
         */
        public Puyo() : base()
        {
            int x = RandomNumber();
            String path = String.Format("puyo/puyo-{0}", x);
            sprite = new CCSprite(path)
            {
                AnchorPoint = CCPoint.AnchorMiddle,
                IsAntialiased = false,
                Scale = 2.5f
            };
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
            n = n % 6;
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
            //removeFromPhysicsWorld ()       
        }

        public float GetSpriteSize() { return sprite.ScaledContentSize.Width; }
        public void SetX(float x) { PositionX = x; }
        public void SetY(float y) { PositionY = y; }
        public void SetPosition(float x, float y){ SetX(x); SetY(y); }
        public void SetPosition(CCPoint point) { SetX(point.X); SetY(point.Y); }
    }
}
