using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathHelper = Microsoft.Xna.Framework.MathHelper;

namespace RPGProject.MapGen
{
    class Erosion
    {
        Map map;
        const float slopeMax = 5;

        public Erosion(Map map)
        {
            this.map = map;
        }

        void blurMap()
        {
            //double[] kernel = { 0.006, 0.061, 0.242, 0.383, 0.242, 0.061, 0.006 };
            //int[] vals = new int[3];
            int[,] heightMap = map.HeightMap;
            for (int i = 0; i < Map.MAPSIZE - 1; i++)
                for (int j = 0; j < Map.MAPSIZE - 1; j++)
                {
                    int size = 1;
                    int s = 0;
                    int c = 0;
                    int sx, sy, ex, ey;
                    sx = (int)Math.Max(-i, -size);
                    sy = (int)Math.Max(-j, -size);
                    ex = (int)Math.Min(Map.MAPSIZE - 2 - i, size);
                    ey = (int)Math.Min(Map.MAPSIZE - 2 - j, size);
                    for (int x = sx; x < ex; x++)
                        for (int y = sy; y < ey; y++)
                        {
                            s += (int)(heightMap[i + x, j + y]);
                            c++;
                        }
                    map.SetHeight(i, j, s/c);
                }
        }

        void erode(int n)
        {
            int[,] heightMap = map.HeightMap;
            int di, dmax = 0;
            int ti = 0;
            int tj = 0;

            for (int i = 0; i < Map.MAPSIZE - 1; i++)
                for (int j = 0; j < Map.MAPSIZE - 1; j++)
                {
                    dmax = 0;
                    int size = 1;
                    int sx, sy, ex, ey;
                    sx = (int)Math.Max(-i, -size);
                    sy = (int)Math.Max(-j, -size);
                    ex = (int)Math.Min(Map.MAPSIZE - 1 - i, size);
                    ey = (int)Math.Min(Map.MAPSIZE - 1 - j, size);

                    if (n % 2 == 0)
                    {
                        for (int x = sx; x <= ex; x++)
                        {
                            di = heightMap[i, j] - heightMap[i + x, j];
                            if (di > dmax)
                            {
                                dmax = di;
                                ti = i + x; tj = j;
                            }
                        }

                        for (int y = sy; y <= ey; y++)
                        {
                            di = heightMap[i, j] - heightMap[i, j + y];
                            if (di > dmax)
                            {
                                dmax = di;
                                ti = i; tj = j + y;
                            }
                        }
                    }
                    else
                    {
                        for (int x = sx; x <= ex; x++)
                        {
                            for (int y = sy; y <= ey; y++)
                            {
                                if (x != 1 && y != 1)
                                {
                                    di = heightMap[i, j] - heightMap[i + x, j + y];
                                    if (di > dmax)
                                    {
                                        dmax = di;
                                        ti = i + x; tj = j + y;
                                    }
                                }
                            }
                        }
                    }//*/

                    /*for (int x = sx; x <= ex; x++)
                    {
                        for (int y = sy; y <= ey; y++)
                        {

                            di = heightMap[i, j] - heightMap[i + x, j + y];
                            if (di > dmax)
                            {
                                dmax = di;
                                ti = i + x; tj = j + y;
                            }

                        }
                    }//*/

                    if (dmax > 1 && dmax <= slopeMax)
                    {
                        int diffH = (int)((1 / 2f) * dmax);

                        map.SetHeight(i, j, (int)MathHelper.Clamp(heightMap[i, j] - diffH, 0f, 255f));
                        map.SetHeight(ti, tj, (int)MathHelper.Clamp(heightMap[ti, tj] + diffH, 0f, 255f));
                    }
                }
        }

        public void Erode()
        {
            for (int i = 0; i < 100; i++)
            {
                erode(i);
            }
            blurMap();
        }
    }
}
