using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenTools.Mathematics;
using System.Drawing;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace RPGProject.MapGen
{
    class Voronoi
    {


        #region Type Declaration

        public class Center
        {
            public Dictionary<string, object> Properties = new Dictionary<string, object>();

            public Dictionary<Vector2, Center> neighbors = new Dictionary<Vector2, Center>();
            public Dictionary<int, Edge> borders = new Dictionary<int, Edge>();
            public Dictionary<Vector2, Corner> corners = new Dictionary<Vector2, Corner>();

            public Vector2 position;

            public Center(Vector2 pos)
            {
                position = pos;
            }
        }

        public class Edge
        {
            static int NUM = 0;

            public Dictionary<string, object> Properties = new Dictionary<string, object>();

            public Center d0, d1;
            public Corner v0, v1;
            int id;

            public int ID
            { get { return id; } }

            public Edge()
            {
                id = NUM;
                NUM++;
            }

            public override int GetHashCode()
            {
                return id.GetHashCode();
            }
        }

        public class Corner
        {
            public Dictionary<string, object> Properties = new Dictionary<string, object>();

            public Dictionary<Vector2, Center> touches = new Dictionary<Vector2, Center>();
            public Dictionary<int, Edge> protrudes = new Dictionary<int, Edge>();
            public Dictionary<Vector2, Corner> adjacent = new Dictionary<Vector2, Corner>();

            public Vector2 position;

            public Corner(Vector2 pos)
            {
                position = pos;
            }
        }

        #endregion

        public Dictionary<Vector2, Center> Centers = new Dictionary<Vector2, Center>();
        public Dictionary<int, Edge> Edges = new Dictionary<int, Edge>();
        public Dictionary<Vector2, Corner> Corners = new Dictionary<Vector2, Corner>();

        public Voronoi()
        {
        }

        public void Clear()
        {
            Centers.Clear();
            Edges.Clear();
            Corners.Clear();
        }

        public void GenVoronoi(IEnumerable<Vector> points)
        {
            VoronoiGraph graph = Fortune.ComputeVoronoiGraph(points);

            foreach (VoronoiEdge vedge in graph.Edges)
            {
                if (double.IsNaN(vedge.VVertexB[0]))
                {
                    Vector2 vectLeft = Tools.Functions.VectorToVector2(vedge.LeftData);
                    Vector2 vectRight = Tools.Functions.VectorToVector2(vedge.RightData);
                    Vector2 start = Tools.Functions.VectorToVector2(vedge.VVertexA);

                    Vector2 dir = new Vector2(-vectRight.Y + vectLeft.Y, vectRight.X - vectLeft.X);
                    dir.Normalize();
                    dir /= 2;
                    Vector2 end = dir + start;

                    if (end.X <= 1 && end.X >= 0 && end.Y >= 0 && end.Y <= 1)
                        end = start - dir;


                    vedge.VVertexB = Tools.Functions.Vector2ToVector(end);


                }
                if (!((vedge.VVertexA[0] < 0 || vedge.VVertexA[1] < 0 || vedge.VVertexA[0] > 1 || vedge.VVertexA[1] > 1)
                    && (vedge.VVertexB[0] < 0 || vedge.VVertexB[1] < 0 || vedge.VVertexB[0] > 1 || vedge.VVertexB[1] > 1))
                    && !double.IsNaN(vedge.VVertexB[0])
                    )
                {
                    Edge e = new Edge();



                    Center cent1, cent2;
                    Corner corn1, corn2;

                    if (!Centers.TryGetValue(Tools.Functions.VectorToVector2(vedge.LeftData), out cent1))
                    {
                        cent1 = new Center(Tools.Functions.VectorToVector2(vedge.LeftData));
                        Centers.Add(cent1.position, cent1);
                    }


                    if (!Centers.TryGetValue(Tools.Functions.VectorToVector2(vedge.RightData), out cent2))
                    {
                        cent2 = new Center(Tools.Functions.VectorToVector2(vedge.RightData));
                        Centers.Add(cent2.position, cent2);
                    }


                    if (!Corners.TryGetValue(Tools.Functions.VectorToVector2(vedge.VVertexA), out corn1))
                    {
                        corn1 = new Corner(Tools.Functions.VectorToVector2(vedge.VVertexA));
                        Corners.Add(corn1.position, corn1);
                    }


                    if (!Corners.TryGetValue(Tools.Functions.VectorToVector2(vedge.VVertexB), out corn2))
                    {
                        corn2 = new Corner(Tools.Functions.VectorToVector2(vedge.VVertexB));
                        Corners.Add(corn2.position, corn2);
                    }

                    if (corn1.position != corn2.position)
                    {
                        fill(corn1, corn2, cent1, cent2, e);
                        Edges.Add(e.ID, e);
                    }
                }
            }
        }

        private void fill(Corner p1, Corner p2, Center c1, Center c2, Edge e)
        {
            e.d0 = c1;
            c1.borders.Add(e.ID, e);
            e.d1 = c2;
            c2.borders.Add(e.ID, e);
            e.v0 = p1;
            p1.protrudes.Add(e.ID, e);
            e.v1 = p2;
            p2.protrudes.Add(e.ID, e);

            if (!c1.neighbors.ContainsKey(c2.position))
            {
                c1.neighbors.Add(c2.position, c2);
                c2.neighbors.Add(c1.position, c1);
            }

            p1.adjacent.Add(p2.position, p2);
            p2.adjacent.Add(p1.position, p1);

            if (!c1.corners.ContainsKey(p1.position))
            {
                c1.corners.Add(p1.position, p1);
                p1.touches.Add(c1.position, c1);
            }
            if (!c1.corners.ContainsKey(p2.position))
            {
                c1.corners.Add(p2.position, p2);
                p2.touches.Add(c1.position, c1);
            }
            if (!c2.corners.ContainsKey(p1.position))
            {
                c2.corners.Add(p1.position, p1);
                p1.touches.Add(c2.position, c2);
            }
            if (!c2.corners.ContainsKey(p2.position))
            {
                c2.corners.Add(p2.position, p2);
                p2.touches.Add(c2.position, c2);
            }
        }

        public void SaveAsImage(string path)
        {
            int imgsize = 2048;

            Bitmap b = new Bitmap(imgsize, imgsize);

            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.Blue);

            foreach (KeyValuePair<int, Edge> ke in Edges)
            {
                Edge e = ke.Value;

                Center c = e.d0;
                Tools.Functions.SetPointsSortCenter(new Point((int)(c.position.X * imgsize), (int)(c.position.Y * imgsize)));
                List<Point> l = new List<Point>();
                foreach (Corner p in c.corners.Values)
                {
                    l.Add(new Point((int)(p.position.X * imgsize), (int)(p.position.Y * imgsize)));
                }
                l.Sort(Tools.Functions.SortPointsClockwise);
                Color col;
                if ((bool)c.Properties["Land"])
                    if ((bool)c.Properties["Lake"])
                    {
                        int val = (int)((double)c.Properties["Height"]*255);
                        col = Color.FromArgb(val, val, val);
                    }
                    else
                        col = Color.SandyBrown;
                else
                    if ((bool)c.Properties["Lake"])
                        col = Color.SlateBlue;
                    else
                        col = Color.Blue;
                g.FillPolygon(new SolidBrush(col), l.ToArray());

            }//*/
            foreach (KeyValuePair<int, Edge> ke in Edges)
            {
                Edge e = ke.Value;

                Center c = e.d0;

                Point start = new Point((int)(e.v0.position.X * imgsize), (int)(e.v0.position.Y * imgsize));
                Point end = new Point((int)(e.v1.position.X * imgsize), (int)(e.v1.position.Y * imgsize));
                Pen p;
                object r;
                if (e.Properties.TryGetValue("River", out r))
                {
                    p = new Pen(Color.Blue, (float)Math.Min(Math.Sqrt((double)((int)r)),15));
                }
                else
                    p = new Pen(Color.Black);


                g.FillEllipse(new SolidBrush(Color.Black), (int)(c.position.X * imgsize) - 2, (int)(c.position.Y * imgsize) - 2, 4, 4);
                g.DrawLine(p, start, end);

            }

            b.Save(path);
        }
    }
}
