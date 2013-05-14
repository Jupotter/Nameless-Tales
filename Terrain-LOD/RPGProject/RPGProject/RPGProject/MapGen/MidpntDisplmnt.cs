using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject.MapGen
{
    public delegate int BorderAction(int x, int y);

    class MidPntDisplmnt
    {
        int[,] map;
        float smooth = 1f;

        int[,] seedVal;
        int seed;

        BorderAction borderAction;

        static int zeroBorder(int x, int y)
        {
            return 0;
        }

        public MidPntDisplmnt(ref int[,] map, float smooth, int[,] seedVal, int randSeed)
        {
            this.smooth = smooth;
            this.map = map;
            if (randSeed == -1)
            {
                Random r = new Random();
                seed = r.Next();
            }
            else
                seed = randSeed;
            this.seedVal = seedVal;
            borderAction = zeroBorder;
        }

        public MidPntDisplmnt(ref int[,] map, float smooth, int[,] seedVal)
            : this(ref map, smooth, seedVal, 0)
        {
            Random r = new Random();
            seed = r.Next();
        }

        public MidPntDisplmnt(ref int[,] map, float smooth, int seed)
            : this(ref map, smooth, new int[3, 3], seed)
        { }

        public MidPntDisplmnt(ref int[,] map, float smooth)
            : this(ref map, smooth, new int[3, 3])
        { }

        public void SetBorderAction(BorderAction action)
        {
            borderAction = action;
        }

        //void blurMap()
        //{
        //    double[] kernel = {0.006,0.061,0.242,0.383,0.242,0.061,0.006};
        //    //double[] kernel = { 0.2, 0.2, 0.2, 0.2, 0.2, 0.2, 0.2 };
        //    //int[] vals = new int[3];

        //    for (int i = 3; i < Map.MAPSIZE-4; i++)
        //        for (int j = 0; j < Map.MAPSIZE - 1; j++)
        //        {
        //            int s = 0;
        //            for (int x = -3; x < 3; x++)
        //            {
        //                s += (int)(map.HeightMap[i + x, j] * kernel[x + 3]);
        //            }
        //            map.SetHeight(i, j, s);
        //        }
        //    for (int i = 0; i < Map.MAPSIZE - 1; i++)
        //        for (int j = 3; j < Map.MAPSIZE - 4; j++)
        //        {
        //            int s = 0;
        //            for (int x = -3; x < 3; x++)
        //            {
        //                s += (int)(map.HeightMap[i, j + x] * kernel[x + 3]);
        //            }
        //            map.SetHeight(i, j, s);
        //        }
        //}

        void blurMap()
        {
            //double[] kernel = { 0.006, 0.061, 0.242, 0.383, 0.242, 0.061, 0.006 };
            //int[] vals = new int[3];
            int[,] heightMap = map;
            for (int i = 0; i < Map.MAPSIZE - 1; i++)
                for (int j = 0; j < Map.MAPSIZE - 1; j++)
                {
                    int size = 2;
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
                    map[i, j] = s / c;
                }
        }

        public void Generate()
        {
            Random rand = new Random(seed);

            int s = Map.MAPSIZE;
            int n = 0;
            while (s > 1)
            {
                s = s >> 1; n++;
            }

            map[0, 0] = seedVal[0, 0];
            map[0, Map.MAPSIZE - 1] = seedVal[0, 2];
            map[Map.MAPSIZE - 1, 0] = seedVal[2, 0];
            map[Map.MAPSIZE - 1, Map.MAPSIZE - 1] = seedVal[2, 2];
            map[0, (Map.MAPSIZE - 1) / 2] = seedVal[0, 1];
            map[(Map.MAPSIZE - 1) / 2, Map.MAPSIZE - 1] = seedVal[1, 2];
            map[Map.MAPSIZE - 1, (Map.MAPSIZE - 1) / 2] = seedVal[2, 1];
            map[(Map.MAPSIZE - 1) / 2, 0] = seedVal[1, 0];
            map[(Map.MAPSIZE - 1) / 2, (Map.MAPSIZE - 1) / 2] = seedVal[1, 1];

            for (int c = 1; c < n; c++)
            {
                int num = 1 << c;
                int mid = (Map.MAPSIZE - 1) / num;

                for (int i = 0; i < num; i++)
                    for (int j = 0; j < num; j++)
                    {
                        int x1 = (i * mid + (i + 1) * mid) / 2;
                        int x2 = i * mid;
                        int x3 = (i + 1) * mid;
                        int y1 = (j * mid + (j + 1) * mid) / 2;
                        int y2 = j * mid;
                        int y3 = (j + 1) * mid;

                        int diamrand = rand.Next(-255, 255);
                        diamrand = (int)(diamrand / ((num) * smooth));
                        int diam = Math.Max(0, (int)Math.Min(255,
                            (map[x2, y2] + map[x2, y3] + map[x3, y2] + map[x3, y3]) / 4
                            + diamrand));

                        map[x1, y1] = diam;

                    }
                for (int i = 0; i < num; i++)
                    for (int j = 0; j < num; j++)
                    {
                        int x1 = (i * mid + (i + 1) * mid) / 2;
                        int x2 = i * mid;
                        int x3 = (i + 1) * mid;
                        int y1 = (j * mid + (j + 1) * mid) / 2;
                        int y2 = j * mid;
                        int y3 = (j + 1) * mid;

                        int x0 = ((i - 1) * mid + i * mid) / 2;
                        int x4 = ((i + 1) * mid + (i + 2) * mid) / 2;
                        int y0 = ((j - 1) * mid + j * mid) / 2;
                        int y4 = ((j + 1) * mid + (j + 2) * mid) / 2;

                        int diam = map[x1, y1];

                        int sq1, sq2, sq3, sq4;

                        int r = rand.Next(-255, 255);
                        sq1 = x2 == 0 ?
                            borderAction(x2, y1) : Math.Max(0, (int)Math.Min(255,
                                (map[x2, y2] + diam + map[x2, y3] + map[x0, y1]) / 4
                                + r / ((num) * smooth)));
                        if (sq1 < 0)
                            sq1 = Math.Max(0, (int)Math.Min(255,
                                (map[x2, y2] + diam + map[x2, y3]) / 3
                                + r / ((num) * smooth)));

                        r = rand.Next(-255, 255);
                        sq2 = y2 == 0 ?

                            borderAction(x1, y2) : Math.Max(0, (int)Math.Min(255,
                                (map[x2, y2] + diam + map[x3, y2] + map[x1, y0]) / 4
                                + r / ((num) * smooth)));
                        if (sq2 < 0)
                            sq2 = Math.Max(0, (int)Math.Min(255,
                            (map[x2, y2] + diam + map[x3, y2]) / 3
                            + r / ((num) * smooth)));

                        r = rand.Next(-255, 255);
                        sq3 = x3 == Map.MAPSIZE - 1 ?
                            borderAction(x3, y1) : Math.Max(0, (int)Math.Min(255,
                                (map[x3, y3] + diam + map[x2, y3] + map[x4, y1]) / 4
                                + r / ((num) * smooth)));
                        if (sq3 < 0)
                            sq3 = Math.Max(0, (int)Math.Min(255,
                                (map[x3, y3] + diam + map[x2, y3]) / 3
                                + r / ((num) * smooth)));

                        r = rand.Next(-255, 255);
                        sq4 = y3 == Map.MAPSIZE - 1 ?
                            borderAction(x1, y3) : Math.Max(0, (int)Math.Min(255,
                                (map[x3, y3] + diam + map[x3, y2] + map[x1, y4]) / 4
                                + r / ((num) * smooth)));
                        if (sq4 < 0)
                            sq4 = Math.Max(0, (int)Math.Min(255,
                                (map[x3, y3] + diam + map[x3, y2]) / 3
                                + r / ((num) * smooth)));

                        //int deb = 0;
                        //Console.WriteLine("c:{5} i:{6} j:{7}\n{0} : {1} : {2} : {3} : {4}", diam, sq1, sq2, sq3, sq4, c, i, j);
                        //if (diam < 0 || sq1 < 0 || sq2 < 0 || sq3 < 0 || sq4 < 0)
                          //  deb = 1 / deb;

                        map[x2, y1] = sq1;
                        map[x1, y2] = sq2;
                        map[x3, y1] = sq3;
                        map[x1, y3] = sq4;
                    }
            }

            blurMap();
        }


    }
}
