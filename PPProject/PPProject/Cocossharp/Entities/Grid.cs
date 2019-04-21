using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPProject
{
    public class Grid : CCNode
    {

        private int HEI;
        private int WID;
        private Puyo p;
        private Puyo[,] pTab;
        private CCPoint[,] points;
        private CCDrawNode drawNode;

        /*
         * Constructeur
         */
        public Grid() : base()
        {
            p = new Puyo();
            CreateGrid();

        }

        /*
         * Création de la grille
         */
        public void CreateGrid()
        {
            WID = 6;
            HEI = 12;
        
            
            points = new CCPoint[WID, HEI];
            CreatePoints();

            pTab = new Puyo[WID, HEI];
           // AddChild(drawNode);

        }

        /*
         * Créer les points pour la grille
         */
        public void CreatePoints()
        {
            float tx = 0;
            float ty = 0;
            for (int i = 0; i < WID; i++) //Pour toute la largeur
            {
                for (int j = 0; j < HEI; j++) //Pour toute la hauteur
                {
                    points[i, j] = new CCPoint(tx + (p.GetSpriteSize() / 2), ty + (p.GetSpriteSize() / 2)); //Création des points
                    ty += p.GetSpriteSize();
                }
                ty = 0;
                tx += p.GetSpriteSize();
            }
        }

        //Ajoute un Puyo à la grille
        public void AddElement(int x, int y, Puyo p1)
        {
            p1.SetPosition(points[x, y]);
            pTab[x, y] = p1;
            AddChild(p1);
        }

        //Donne le point de départ
        public CCPoint GetStartingPoint()
        {
            return points[2, 11];
        }

        public CCPoint GetPointDown(int b)
        {
            return points[b, 0];
        }

        

    }
}
