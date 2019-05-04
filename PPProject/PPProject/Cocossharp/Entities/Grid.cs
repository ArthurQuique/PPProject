using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPProject
{
    public class Grid : CCNode
    {

        private int HEI; //Hauteur de la grille
        private int WID; //Largeur de la grille
        private Puyo p; //Puyo exemple
        private Puyo[,] pTab; //Tableau des Puyos 
        private int[] columns; //Columns
        private CCPoint[,] points; //Tableau de points
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
        }

        //Ajoute un Puyo à la grille
        public void AddElement(int x, int y, Puyo p1)
        {
            p1.SetPosition(points[x, y]);
            pTab[x, y] = p1;
            columns[x]++;
            AddChild(p1);
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
        

    }
}
