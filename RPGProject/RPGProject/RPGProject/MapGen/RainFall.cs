using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.MapGen
{
    class RainFall
    {
        Map map;
        int waterGain = 100;
        int waterBase = 1000;
        float slopeMult = 0.75f;
        int depositMult = 10;

        public RainFall(Map map)
        {
            this.map = map;
        }

        void blurMap()
        {
            //double[] kernel = { 0.006, 0.061, 0.242, 0.383, 0.242, 0.061, 0.006 };
            //int[] vals = new int[3];
            int[,] rainMap = map.RainMap;
            for (int i = 0; i < Map.MAPSIZE - 1; i++)
                for (int j = 0; j < Map.MAPSIZE - 1; j++)
                {
                    int size = 5;
                    int s = 0;
                    int c = 0;
                    int sx, sy, ex, ey;
                    sx = (int)Math.Max(-i, -size);
                    sy = (int)Math.Max(-j, -size);
                    ex = (int)Math.Min(Map.MAPSIZE - 3 - i, size);
                    ey = (int)Math.Min(Map.MAPSIZE - 3 - j, size);
                    for (int x = sx; x < ex; x++)
                        for (int y = sy; y < ey; y++)
                        {
                            s += (int)(rainMap[i + x, j+y]);
                            c++;
                        }
                    map.SetRain(i, j, (s/(10*(c==0?1:c)))*10);
                }
        }

        void rainPass(int n)
        {
            bool hv;
            bool sb;

            switch (n)
            {
                case 1:
                    hv = false;
                    sb = false;
                    break;
                case 2:
                    hv = false;
                    sb = true;
                    break;
                case 3:
                    hv = true;
                    sb = false;
                    break;
                case 4:
                    hv = true;
                    sb = true;
                    break;
                default:
                    return;
            }

            int starti, endi;
            starti = 0;
            endi = Map.MAPSIZE - 1;

            int waterRes;
            for (int i = starti; i < endi; i++)
            {
                waterRes = waterBase;
                for (int j = !sb?1:Map.MAPSIZE-2; !sb?j < Map.MAPSIZE - 2:j>=1; j = !sb?j+1:j-1)
                {
                    if (map.HeightMap[hv ? j : i, hv ? i : j] <= Map.WATER_LEVEL)
                    {
                        int waterAdd = waterGain;
                        waterRes += waterAdd;
                        if (waterRes > 1000)
                            waterRes = 1000;
                        map.AddRain(hv ? j : i, hv ? i : j, waterAdd);
                    }
                    else
                    {
                        float slope = (map.HeightMap[hv ? j + 1 : i, hv ? i + 1 : j + 1] -
                            map.HeightMap[hv ? j - 1 : i, hv ? i : j - 1] + 1) * slopeMult * (sb ? -1 : 1);

                        if (slope > 0 && waterRes > 0)
                        {
                            float slopeP = slope / 255f;
                            map.AddRain(hv ? j : i, hv ? i : j, (int)(waterRes * slopeP) * depositMult);
                            waterRes -= (int)(waterRes * slopeP);
                        }
                    }
                }
            }
        }

        public void RainSim()
        {
            for (int i = 1; i <= 4; i++)
            {
                rainPass(i);
            }

            blurMap();
        }
    }
}
