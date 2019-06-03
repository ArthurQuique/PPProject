using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPProject.Cocossharp.Entities
{
    public class Grid : CCNode
    {

        private int HEI; //Hauteur de la grille
        private int WID; //Largeur de la grille
        private Puyo p; //Puyo exemple
        private Puyo[,] pTab; //Tableau des Puyos 
        private int[] columns; //Columns
        private CCPoint[,] points; //Tableau de points
        private CCRect bounds;
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
            HEI = 13;
            
            points = new CCPoint[WID, HEI];
            CreatePoints();

            pTab = new Puyo[WID, HEI];
            columns = new int[6];
            for(int i = 0; i < WID; i++)
            {
                columns[i] = 0;
            }
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
                    points[i, j] = new CCPoint(tx,ty); //Création des points
                    ty += p.GetSpriteSize();
                }
                ty = 0;
                tx += p.GetSpriteSize();
            }
            bounds = new CCRect(0, 0, WID * p.GetSpriteSize(), HEI * p.GetSpriteSize()+22000);
        }

        //Ajoute un Puyo à la grille
        public void AddElement(int x, int y, Puyo p1)
        {
            p1.SetPosition(points[x, y]);
            pTab[x, y] = p1;
            columns[x]++;
            AddChild(p1);
        }

        public void AddPair(Pair pair)
        {
            Puyo p1, p2;
            int x1, x2, y1, y2;
            p1 = pair.GetP1();
            p2 = pair.GetP2();
            x1 = p1.GetColumn();
            x2 = p2.GetColumn();            
            if (pair.GetPlacement() == 3)
            {
                y2 = columns[x2];
                pTab[x2, y2] = p2;
                columns[x2]++;
                p2.SetPosition(points[x2, y2]);
                AddChild(p2);
                y1 = columns[x1];
                pTab[x1, y1] = p1;
                columns[x1]++;
                p1.SetPosition(points[x1, y1]);
                AddChild(p1);
            }
            else
            {
                y1 = columns[x1];
                pTab[x1, y1] = p1;
                columns[x1]++;
                p1.SetPosition(points[x1, y1]);
                AddChild(p1);
                y2 = columns[x2];
                pTab[x2, y2] = p2;
                columns[x2]++;
                p2.SetPosition(points[x2, y2]);
                AddChild(p2);
            }
            

           
        }

        //Donne le point de départ
        public CCPoint GetStartingPoint()
        {
            return points[2, 12];
        }

        //Donne le Puyo à une case donnée
        public Puyo GetPuyoAtPoint(int x, int y)
        {
            return pTab[x, y];
        }

        //Retourne la première case d'une colonne donnée
        public CCPoint GetPointDown(int b)
        {
            return points[b, columns[b]];
        }

        //Retourne le bord gauche de la grille
        public float GetLeftBorder()
        {
            return points[0, 0].X;

        }
        
        public bool IsInBounds(Pair pair)
        {
            return (bounds.IntersectsRect(pair.GetP1().BoundingBoxTransformedToWorld) && bounds.IntersectsRect(pair.GetP2().BoundingBoxTransformedToWorld));
        }


    }
}
