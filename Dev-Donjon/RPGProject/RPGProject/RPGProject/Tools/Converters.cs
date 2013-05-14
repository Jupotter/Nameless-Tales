using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Point = System.Drawing.Point;

namespace RPGProject.Tools
{
    static class Functions
    {
        static public Comparison<Point> SortPointsClockwise = sortPointsClockwise;
        static Point pointsSortCenter;

        static public Vector2 VectorToVector2(BenTools.Mathematics.Vector v)
        {
            if (v.Dim < 2)
                throw new ArgumentException("There is not enough coordinates in this Vector");
            return (new Vector2((float)v[0], (float)v[1]));
        }

        static public BenTools.Mathematics.Vector Vector2ToVector(Vector2 v)
        {
            return new BenTools.Mathematics.Vector(new double[2] { v.X, v.Y });
        }

        static public void SetPointsSortCenter(Point c)
        {
            pointsSortCenter = c;
        }

        static int sortPointsClockwise(Point p1, Point p2)
        {
            if (p1.X >= 0 && p2.X < 0)
                return -1;
            if (p1.X ==0 && p2.X == 0)
                return p1.Y > p2.Y?-1:1;
            Point c = pointsSortCenter;
            int det = (p1.X - c.X) * (p2.Y - c.Y) - (p2.X - c.X) * (p1.Y - c.Y);
            if (det < 0)
                return -1;
            if (det > 0)
                return 1;
            return 0;
        }
    }
}
