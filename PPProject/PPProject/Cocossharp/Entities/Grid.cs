﻿using CocosSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPProject.Cocossharp.Entities
{
    public class Grid : CCNode
    {

        private static int HEI; //Hauteur de la grille
        private static int WID; //Largeur de la grille
        private Puyo p; //Puyo exemple
        private Puyo[,] pTab; //Tableau des Puyos 
        private int[] columns; //Columns
        private CCRect[] columnHitBoxes;
        private CCPoint[,] points; //Tableau de points
        private CCRect bounds;
        private CCDrawNode drawNode;
        private List<Puyo> killList;
        private List<Puyo> chain;
        private List<Puyo> floatingPuyos;

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
            for(int i = 0; i < WID ; i++)
            {
                for(int j = 0 ; j < HEI ; j++)
                {
                    pTab[i, j] = null;
                }
            }
            columns = new int[WID];
            columnHitBoxes = new CCRect[WID];
            killList = new List<Puyo>();
            chain = new List<Puyo>();
            floatingPuyos = new List<Puyo>();

            for (int i = 0; i < WID; i++)
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

        //Ajoute une paire à la grille
        public void AddPair(Pair pair)
        {
            if (pair!=null)
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
                    if (y2 < HEI-1)
                    {
                        y1 = columns[x1];
                        pTab[x1, y1] = p1;
                        columns[x1]++;
                        p1.SetPosition(points[x1, y1]);
                        AddChild(p1);
                    }                    
                }
                else
                {
                    y1 = columns[x1];
                    pTab[x1, y1] = p1;
                    columns[x1]++;
                    p1.SetPosition(points[x1, y1]);
                    AddChild(p1);
                    if (y1 < HEI-1)
                    {
                        y2 = columns[x2];
                        pTab[x2, y2] = p2;
                        columns[x2]++;
                        p2.SetPosition(points[x2, y2]);
                        AddChild(p2);
                    }
                    
                }
                UpdateColumnHitBoxes();
                UpdateLinks();
            }
        }

        //Met à jour le tableau columns
        public void UpdateColumns()
        {
            int nb = 0;
            for(int i = 0; i < WID; i++)
            {
                nb = 0;
                for(int j = 0; j<HEI; j++)
                {
                    if(!(pTab[i,j] is null))
                    {
                        nb++;
                    }
                }
                columns[i] = nb;
            }
        }

        //Met à jour la hitbox des colonnes
        public void UpdateColumnHitBoxes()
        {
            for(int i = 0; i < WID; i++)
            {
                columnHitBoxes[i] = new CCRect(points[i,0].X,
                                                points[i,0].Y,
                                                p.ContentSize.Width,
                                                GetPointDown(i).Y);
            }
        }

        //Met à jour les liens des Puyos
        public void UpdateLinks()
        {
            for(int i = 0; i < WID; i++)
            {
                for(int j = 0; j < HEI; j++)
                {
                    if (!(pTab[i, j] is null))
                    {
                        //Link en haut
                        if (j < HEI - 1)
                        {
                            if (!(pTab[i, j + 1] is null))
                            {
                                if (pTab[i, j].GetColor().Equals(pTab[i, j + 1].GetColor()))
                                {
                                    pTab[i, j].LinkUp();
                                }
                                else
                                {
                                    pTab[i, j].NoLinkUp();
                                }
                            }
                            else
                            {
                                pTab[i, j].NoLinkUp();
                            }
                        }
                        else
                        {
                            pTab[i, j].NoLinkUp();
                        }


                        //Link à droite
                        if (i < WID-1)
                        {
                            if (!(pTab[i + 1, j] is null))
                            {
                                if (pTab[i, j].GetColor().Equals(pTab[i + 1, j].GetColor()))
                                {
                                    pTab[i, j].LinkRight();
                                }
                                else
                                {
                                    pTab[i, j].NoLinkRight();
                                }
                            }
                            else
                            {
                                pTab[i, j].NoLinkRight();
                            }
                        }
                        else
                        {
                            pTab[i, j].NoLinkRight();
                        }

                        //Link en bas
                        if (j > 0)
                        {
                            if (!(pTab[i, j - 1] is null))
                            {
                                if (pTab[i, j].GetColor().Equals(pTab[i, j - 1].GetColor()))
                                {
                                    pTab[i, j].LinkDown();
                                }
                                else
                                {
                                    pTab[i, j].NoLinkDown();
                                }
                            }
                            else
                            {
                                pTab[i, j].NoLinkDown();
                            }
                        }
                        else
                        {
                            pTab[i, j].NoLinkDown();
                        }

                        //Link à gauche
                        if (i > 0)
                        {
                            if (!(pTab[i - 1, j] is null))
                            {
                                if (pTab[i, j].GetColor().Equals(pTab[i - 1, j].GetColor()))
                                {
                                    pTab[i, j].LinkLeft();
                                }
                                else
                                {
                                    pTab[i, j].NoLinkLeft();
                                }
                            }
                            else
                            {
                                pTab[i, j].NoLinkLeft();
                            }
                        }
                        else
                        {
                            pTab[i, j].NoLinkLeft();
                        }

                    }
                }
            }
        }

        //Loop du Chain4
        public void Chain4Loop()
        {
            UpdateLinks();
            bool res = true;
            while (res)
            {
                Chain4Scan();
                if (killList.Count == 0)
                {
                    res = false;
                }
                else
                {
                    ExecuteKillList();
                }
            }
        }

        //Vérifie les chaines de 4 ou plus et les ajoute à la KillList
        public void Chain4Scan()
        {
            UnverifyAll();
            for(int i = 0; i < WID; i++)
            {
                for(int j = 0; j < HEI ; j++)
                {
                    if (!(pTab[i, j] is null))
                    {
                        if (!(pTab[i, j ].Verified()))
                        {                            
                            Chain4(i, j);
                            if (chain.Count >= 4)
                            {
                                
                                killList.AddRange(chain);
                            }
                            chain.Clear();
                        }
                    }
                }
            }
        }
        
        //Vérifie une chaine de 4
        public void Chain4(int i, int j)
        {
            chain.Add(pTab[i, j]);
            pTab[i, j].Verify();
            if (pTab[i, j].LinkedUp() && !(pTab[i, j+1].Verified()))
            {
                Chain4(i, (j + 1));
            }
            if (pTab[i, j].LinkedRight() && !(pTab[i+1, j].Verified()))
            {
                Chain4((i + 1), j);
            }
            if (pTab[i, j].LinkedDown() && !(pTab[i, j-1].Verified()))
            {
                Chain4(i, (j - 1));
            }
            if (pTab[i, j].LinkedLeft() && !(pTab[i-1, j].Verified()))
            {
                Chain4((i - 1), j);
            }
        }

       //Exécute la killList
        public void ExecuteKillList()
        {
            for(int i = 0; i < WID; i++)
            {
                for(int j = 0; j < HEI; j++)
                {
                    if(pTab[i,j]!= null)
                    {
                        if (killList.Contains(pTab[i, j]))
                        {
                            RemoveChild(pTab[i, j]);
                            pTab[i, j] = null; 
                        }
                    }
                    
                }
            }
            killList.Clear();
        }

        //Grosse fonction pour faire tomber les Puyos
        public void LawOfGravity()
        {
            

        }

        //Trouve les Puyo qui flottent 
        public void FindFloatingPuyos()
        {
            bool hole;
            for(int i = 0; i < WID; i++)
            {
                hole = false;
                for(int j = 0; j < HEI; j++)
                {
                    if(pTab[i,j] is null)
                    {
                        hole = true;
                    }
                    if(hole && !(pTab[i, j] is null))
                    {
                        floatingPuyos.Add(pTab[i, j]);
                    }
                } 
            }
        }

        //Fait descendre les Puyos flottants
        public void BringPuyosDown()
        {

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

        public bool HitsTheLeftColumnOnSpin(Pair pair)
        {
            bool res = false;
            pair.GetP2().PositionX -= p.ContentSize.Width;
            if (columnHitBoxes[pair.GetP1().GetColumn()-1].IntersectsRect(pair.GetP2().BoundingBoxTransformedToWorld))
            {
                res = true;
            }
            return res;
        }

        public bool HitsTheRightColumnOnSpin(Pair pair)
        {
            bool res = false;
            pair.GetP2().PositionX += p.ContentSize.Width;
            if (columnHitBoxes[pair.GetP1().GetColumn() + 1].IntersectsRect(pair.GetP2().BoundingBoxTransformedToWorld))
            {
                res = true;
            }
            return res;
        }

        public void UnverifyAll()
        {
            for (int i = 0; i < WID; i++)
            {
                for (int j = 0; j < HEI; j++)
                {
                    if (!(pTab[i, j] is null))
                    {
                        pTab[i, j].Unverify();
                    }
                }
            }
        }
        
    }
}
