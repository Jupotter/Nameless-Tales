using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace RPGProject.MapGen
{
    class MapVoronoi
    {
        int seed;

        Voronoi cells;
        Vector2 minPos;

        public MapVoronoi(int seed)
        {
            this.seed = seed;
            cells = new Voronoi();
        }

        public void Generate()
        {
            GenGraph();
            GenWater();
            GenHeight();
            GenRivers(50);
        }

        public void GenGraph()
        {
            
            Random r = new Random(seed);
            List<BenTools.Mathematics.Vector> lv = new List<BenTools.Mathematics.Vector>();
            for (int i = 0; i < 10000; i++)
            {
                BenTools.Mathematics.Vector v = new BenTools.Mathematics.Vector(new double[2] { r.Next(1, 99) / 100f, r.Next(1, 99) / 100f });
                if (!lv.Contains(v))
                    lv.Add(v);
            }
            cells.GenVoronoi(lv);
            
            for (int i = 0; i < 3; i++)
            {
                //Print(String.Format("Iter{0}", x));
                minPos = new Vector2(1f, 1f);
                lv.Clear();
                foreach (Voronoi.Center c in cells.Centers.Values)
                {
                    Vector2[] points = c.corners.Keys.ToArray();
                    Vector2 cent = CalculateCentroid(points, c.corners.Count - 1);
                    
                    if (cent.X > 0 && cent.X < 1 && cent.Y > 0 && cent.Y < 1 && !lv.Contains(Tools.Functions.Vector2ToVector(cent)))
                    {
                        if (cent.X <= minPos.X && cent.Y <= minPos.Y)
                            minPos = cent;
                        lv.Add(Tools.Functions.Vector2ToVector(cent));
                    }
                }

                cells.Clear();
                cells.GenVoronoi(lv);
            }
        }

        public void GenWater()
        {
            int[,] heightmap = new int[Map.MAPSIZE, Map.MAPSIZE];
            int[,] seedval = new int[3, 3];
            seedval[1, 1] = 150;
            MidPntDisplmnt midpoint = new MidPntDisplmnt(ref heightmap, 1f, seedval, seed);
            midpoint.Generate();
            foreach (Voronoi.Center c in cells.Centers.Values)
            {
                c.Properties.Add("Land", heightmap[(int)(c.position.X * 513), (int)(c.position.Y * 513)] > 75);
                c.Properties.Add("Lake", true);
                foreach (Voronoi.Corner corn in c.corners.Values)
                {
                    if (!corn.Properties.ContainsKey("Land"))
                    {
                        corn.Properties.Add("Land", c.Properties["Land"]);
                        corn.Properties.Add("Lake", true);
                    }
                }
            }
            setOcean(cells.Centers[minPos]);
        }

        void setOcean(Voronoi.Center c)
        {

            c.Properties["Lake"] = false;
            if (!(bool)c.Properties["Land"])
            {
                foreach (Voronoi.Corner corn in c.corners.Values)
                    corn.Properties["Land"] = false;
                foreach (Voronoi.Center next in c.neighbors.Values)
                {
                    if ((bool)next.Properties["Lake"])
                        setOcean(next);
                }
            }
        }

        public void GenHeight()
        {
            int nextHeight, currHeight;

            Queue<Voronoi.Corner> Q = new Queue<Voronoi.Corner>();
            Dictionary<Vector2, Voronoi.Corner>.Enumerator e = cells.Centers[minPos].corners.GetEnumerator();
            e.MoveNext();
            Voronoi.Corner corn = cells.Centers[minPos].corners.Values.ElementAt(0);
            e.Dispose();
            corn.Properties.Add("Height", 0);
            Q.Enqueue(corn);
            while (Q.Count > 0)
            {
                corn = Q.Dequeue();
                currHeight = (int)corn.Properties["Height"];
                foreach (Voronoi.Corner next in corn.adjacent.Values)
                {
                    object outobj;
                    if (next.Properties.TryGetValue("Height", out outobj))
                    {
                        nextHeight = (int)outobj;
                        if ((bool)next.Properties["Land"])
                        {
                            if (nextHeight > currHeight + 1)
                            {
                                next.Properties["Height"] = currHeight + 1;
                                Q.Enqueue(next);
                            }
                        }
                        else
                            if (nextHeight > (int)corn.Properties["Height"])
                            {
                                next.Properties["Height"] = currHeight;
                                Q.Enqueue(next);
                            }
                    }
                    else
                    {
                        if ((bool)next.Properties["Land"])
                            next.Properties.Add("Height", (int)corn.Properties["Height"] + 1);
                        else
                            next.Properties.Add("Height", (int)corn.Properties["Height"]);
                        Q.Enqueue(next);
                    }
                }
            }
            redistHeight();
            setCenterHeight();
        }

        void setCenterHeight()
        {
            foreach (Voronoi.Center cent in cells.Centers.Values)
            {
                double height = 0;
                int val = 0;
                foreach (Voronoi.Corner corn in cent.corners.Values)
                {
                    height += (double)corn.Properties["Height"];
                    val++;
                    if (!corn.Properties.ContainsKey("DownSlope"))
                    {
                        corn.Properties.Add("DownSlope", corn);
                        foreach (Voronoi.Corner next in corn.adjacent.Values)
                            if ((double)next.Properties["Height"] < (double)((Voronoi.Corner)corn.Properties["DownSlope"]).Properties["Height"])
                                corn.Properties["DownSlope"] = next;
                    }
                }
                cent.Properties.Add("Height", height / val);
            }
        }

        void redistHeight()
        {
            double scale = 1.1;
            List<Voronoi.Corner> location = new List<Voronoi.Corner>(cells.Corners.Values);
            location.Sort((c1, c2) => ((int)c1.Properties["Height"]).CompareTo((int)c2.Properties["Height"]));

            for (int i = 0; i < location.Count; i++)
            {
                double y = (double)i / (location.Count);
                double x = Math.Sqrt(scale) - Math.Sqrt(scale * (1 - y));
                if (x > 1.0)
                    x = 1.0;
                location[i].Properties["Height"] = x;
            }
        }

        public void GenRivers(int num)
        {
            Random r = new Random(seed);

            for (int n = 0; n < num; n++)
            {
                Voronoi.Center cent = cells.Centers[minPos];
                while (!(bool)cent.Properties["Lake"])
                {
                    cent = cells.Centers.Values.ElementAt(r.Next(cells.Centers.Values.Count));
                }

                Voronoi.Corner corn = cent.corners.Values.ElementAt(0);
                if (!(bool)corn.Properties.ContainsKey("River"))
                    corn.Properties.Add("River", 1);
                else
                    corn.Properties["River"] = (int)corn.Properties["River"] + 1;
                createRiver(corn);
            }
        }

        void createRiver(Voronoi.Corner corn)
        {
            Voronoi.Corner next = (Voronoi.Corner)corn.Properties["DownSlope"];
            Voronoi.Edge edge = corn.protrudes.Values.First(e => e.v0 == next || e.v1 == next);
            if ((bool)corn.Properties["Land"] && (bool)corn.Properties["Lake"])
            {
                if (next.Properties.ContainsKey("River"))
                    next.Properties["River"] = (int)corn.Properties["River"] + (int)next.Properties["River"] + 1;
                else
                    next.Properties.Add("River", (int)corn.Properties["River"] + 1);
                if (edge.Properties.ContainsKey("River"))
                    edge.Properties["River"] = (int)corn.Properties["River"] + (int)edge.Properties["River"] + 1;
                else
                    edge.Properties.Add("River", (int)corn.Properties["River"] + 1);
                createRiver(next);
            }
        }

        public void Print(string name)
        {
            cells.SaveAsImage(String.Format("{0}.bmp", name));
        }

        public void Convert()
        {
            MapConverter.Convert(cells);
        }

        static Vector2 CalculateCentroid(Vector2[] points, int lastPointIndex)
        {
            float area = 0.0f;
            float Cx = 0.0f;
            float Cy = 0.0f;
            float tmp = 0.0f;
            int k;

            for (int i = 0; i <= lastPointIndex; i++)
            {
                k = (i + 1) % (lastPointIndex + 1);
                tmp = points[i].X * points[k].Y -
                      points[k].X * points[i].Y;
                area += tmp;
                Cx += (points[i].X + points[k].X) * tmp;
                Cy += (points[i].Y + points[k].Y) * tmp;
            }
            area *= 0.5f;
            Cx *= 1.0f / (6.0f * area);
            Cy *= 1.0f / (6.0f * area);

            return new Vector2(Cx, Cy);
        }

    }
}
