using CocosSharp;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

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

        public Grid(int n) : base()
        {
            p = new Puyo();
            CreateGridPuzzle(n);
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
            float tx = p.GetSpriteSize()/2 - 10;
            float ty = p.GetSpriteSize() / 2;
            for (int i = 0; i < WID; i++) //Pour toute la largeur
            {
                for (int j = 0; j < HEI; j++) //Pour toute la hauteur
                {
                    points[i, j] = new CCPoint(tx,ty); //Création des points
                    ty += p.GetSpriteSize();
                }
                ty = p.GetSpriteSize() / 2;
                tx += p.GetSpriteSize();
            }
            bounds = new CCRect(p.GetSpriteSize() / 2 - 10, p.GetSpriteSize() / 2, WID * p.GetSpriteSize(), HEI * p.GetSpriteSize()+22000);
            
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
                    if (!ColumnFull(x2))
                    {
                        y2 = columns[x2];
                        pTab[x2, y2] = p2;
                        columns[x2]++;
                        p2.SetPosition(points[x2, y2]);
                        AddChild(p2);
                        if (!ColumnFull(x1))
                        {
                            y1 = columns[x1];
                            pTab[x1, y1] = p1;
                            columns[x1]++;
                            p1.SetPosition(points[x1, y1]);
                            AddChild(p1);
                        }         
                    }
                               
                }
                else
                {
                    if (!ColumnFull(x1))
                    {
                        y1 = columns[x1];
                        pTab[x1, y1] = p1;
                        columns[x1]++;
                        p1.SetPosition(points[x1, y1]);
                        AddChild(p1);
                        if (!ColumnFull(x2))
                        {
                            y2 = columns[x2];
                            pTab[x2, y2] = p2;
                            columns[x2]++;
                            p2.SetPosition(points[x2, y2]);
                            AddChild(p2);
                        }
                    }
                    
                    
                }
                UpdateColumnHitBoxes();
                UpdateLinks();
            }
        }

        //Met à jour le tableau columns
        public void UpdateColumns()
        {
            int j = 0;
            for(int i = 0; i < WID; i++)
            {
                j = 0;
                while(!(pTab[i,j] is null))
                {
                    j++;
                }
                columns[i] = j;
            }
        }

        //Met à jour la hitbox des colonnes
        public void UpdateColumnHitBoxes()
        {
            Puyo pu = new Puyo();
            for(int i = 0; i < WID; i++)
            {
                columnHitBoxes[i] = new CCRect(points[i,0].X,
                                                points[i,0].Y,
                                                pu.GetSpriteSize(),
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
            bool res = true;
            while (res==true)
            {
                UpdateLinks();
                Chain4Scan();
                if (killList.Count == 0)
                {
                    res = false;
                }
                else
                {
                    ExecuteKillList();
                    LawOfGravity();
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
            UpdateColumns();
            FindFloatingPuyos();
            BringPuyosDown();
            UpdateColumns();
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
                        pTab[i, j] = null;
                    }
                } 
            }
        }

        //Fait descendre les Puyos flottants
        public void BringPuyosDown()
        {
            if (floatingPuyos.Count > 0)
            {
                for(int i = 0; i < floatingPuyos.Count; i++)
                {
                    floatingPuyos[i].Position = points[floatingPuyos[i].GetColumn(),columns[floatingPuyos[i].GetColumn()]];
                    pTab[floatingPuyos[i].GetColumn(), columns[floatingPuyos[i].GetColumn()]] = floatingPuyos[i];
                    columns[floatingPuyos[i].GetColumn()]++;
                }
                
                floatingPuyos.Clear();
                
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

        public void CreateGridPuzzle(int n)
        {
            WID = 6;
            HEI = 13;

            points = new CCPoint[WID, HEI];
            CreatePoints();

            pTab = new Puyo[WID, HEI];
            columns = new int[6];
            for (int i = 0; i < WID; i++)
            {
                columns[i] = 0;
            }

            //string[] lines = System.IO.File.ReadAllLines(@"Puzzle_1.txt");
            
            /*foreach (string line in lines)
            {
                while (line != null)
                {
                    for (int j = 0; j < HEI; j++)
                    {
                        for (int i = 0; i < WID; i++)
                        {
                            if (line[i] != 0)
                            {
                                p = new Puyo(line[i]);
                                AddElement(j, i, p);
                            }
                        }
                    }
                }
            }*/
            using (StreamReader sr = new StreamReader("C:/Users/T420s/Source/Repos/ArthurQuique/PPProject/PPProject/PPProject/Cocossharp/Entities/Puzzle_1.txt"))
            {
                /*string line;
                while ((line = sr.ReadLine()) != null)
                {
                    for (int j = 0; j < HEI; j++)
                    {
                        for (int i = 0; i < WID; i++)
                        {
                            if (line[i] != 0)
                            {
                                p = new Puyo(line[i]);
                                AddElement(j, i, p);
                            }
                        }
                    }
                }*/
            }
        }

        public bool HitsTheLeftColumnOnMove(Pair pair)
        {
            bool res = false;
            float f1 = pair.GetP1().PositionWorldspace.X;
            float f2 = pair.GetP2().PositionWorldspace.X;
            f1 -= p.GetSpriteSize()/2;
            f2 -= p.GetSpriteSize()/2;
            if (columnHitBoxes[pair.GetP1().GetColumn() - 1].ContainsPoint(new CCPoint(f1, pair.GetP1().BoundingBoxTransformedToWorld.Origin.Y)) ||
                columnHitBoxes[pair.GetP2().GetColumn() - 1].ContainsPoint(new CCPoint(f2, pair.GetP2().BoundingBoxTransformedToWorld.Origin.Y)))
            {
                res = true;
            }
            return res;
        }

        public bool HitsTheRightColumnOnMove(Pair pair)
        {
            bool res = false;
            float f1 = pair.GetP1().PositionWorldspace.X;
            float f2 = pair.GetP2().PositionWorldspace.X;
            f1 += p.GetSpriteSize();
            f2 += p.GetSpriteSize();
            if (columnHitBoxes[pair.GetP1().GetColumn() + 1].ContainsPoint(new CCPoint(f1, pair.GetP1().BoundingBoxTransformedToWorld.Origin.Y)) ||
                columnHitBoxes[pair.GetP2().GetColumn() + 1].ContainsPoint(new CCPoint(f2, pair.GetP2().BoundingBoxTransformedToWorld.Origin.Y)))
            {
                res = true;
            }
            return res;
        }

        public bool HitsTheLeftColumnOnSpin(Pair pair)
        {
            bool res = false;
            float f = pair.GetP2().PositionWorldspace.X;
            f -= p.GetSpriteSize()/2;
            if (columnHitBoxes[pair.GetP1().GetColumn() - 1].ContainsPoint(new CCPoint(f, pair.GetP1().BoundingBoxTransformedToWorld.Origin.Y)))
            {
                res = true;
            }
            return res;
        }

        public bool HitsTheRightColumnOnSpin(Pair pair)
        {
            bool res = false;
            float f = pair.GetP2().PositionWorldspace.X;
            f += p.GetSpriteSize();
            if (columnHitBoxes[pair.GetP1().GetColumn() + 1].ContainsPoint(new CCPoint(f,pair.GetP1().BoundingBoxTransformedToWorld.Origin.Y)))
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

        public bool ColumnFull(int c)
        {
            if (columns[c] == 12)
            {
                return true;
            }
            else return false;
        }
        
    }
}
