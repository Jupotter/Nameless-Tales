using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RPGProject.MapGen;
using Microsoft.Xna.Framework;
using sColor = System.Drawing.Color;


namespace RPGProject
{
    class Map
    {
        public const int MAPSIZE = 513;
        public const int WATER_LEVEL = 25;
        const bool SHOW_WATER = true;

        int[,] heightMap;
        int[,] rainMap;
        BiomeType[,] biomeMap;
        public int[,] tempMap;

        Dictionary<Vector2, int> localMapSeeds;
        Dictionary<Vector2, int[,]> localMapBorder;
        int[,] currentLocalMap;
        int currentLocalMapX;
        int currentLocalMapY;

        public int[,] HeightMap
        { get { return heightMap; } }

        public int[,] RainMap
        { get { return rainMap; } }

        public BiomeType[,] BiomeMap
        { get { return biomeMap; } }

        public Map()
        {
            heightMap = new int[MAPSIZE, MAPSIZE];
            rainMap = new int[MAPSIZE, MAPSIZE];

            biomeMap = new BiomeType[MAPSIZE, MAPSIZE];
            tempMap = new int[MAPSIZE, MAPSIZE];

            currentLocalMap = new int[MAPSIZE, MAPSIZE];

            localMapSeeds = new Dictionary<Vector2, int>();
            localMapBorder = new Dictionary<Vector2, int[,]>();
        }

        public void AddRain(int x, int y, int rain)
        {
            rainMap[x,y] = (int)Math.Max(0, Math.Min(255, rainMap[x, y] + rain));
        }

        public bool SetRain(int x, int y, int rain)
        {
            if (rain >= 0 && rain < 256)
                rainMap[x, y] = rain;
            else
                return false;
            return true;
        }

        public void SetBiome(int x, int y, BiomeType b)
        {
            biomeMap[x, y] = b;
        }

        public bool SetHeight(int x, int y, int height)
        {
            if (height >= 0 && height < 256)
                heightMap[x, y] = height;
            else
                return false;
            return true;
        }

        public bool SetHeight(int[,] height)
        {
            heightMap = height;
            return true;
        }

        public void GenLocalMap(int x, int y, bool sides, bool useSeed)
        {
            currentLocalMapX = x;
            currentLocalMapY = y;

            int[,] mapSeeds = new int[3, 3];


            mapSeeds[1, 1] = heightMap[x, y];
            mapSeeds[0, 0] = x == 0 || y == 0 ? 0 : heightMap[x, y];
            mapSeeds[1, 0] = y == 0 ? 0 : heightMap[x, y];
            mapSeeds[0, 1] = x == 0 ? 0 : heightMap[x, y];
            mapSeeds[2, 0] = x == MAPSIZE - 1 || y == 0 ? 0 : heightMap[x, y];
            mapSeeds[0, 2] = y == MAPSIZE - 1 || x == 0 ? 0 : heightMap[x, y];
            mapSeeds[2, 1] = x == MAPSIZE - 1 ? 0 : heightMap[x, y];
            mapSeeds[1, 2] = y == MAPSIZE - 1 ? 0 : heightMap[x, y];
            mapSeeds[2, 2] = x == MAPSIZE - 1 || y == MAPSIZE - 1 ? 0 : heightMap[x, y];


            int seed = -1;
            if (useSeed && !localMapSeeds.TryGetValue(new Vector2(x, y), out seed))
            {
                Random r = new Random();
                seed = r.Next();
                localMapSeeds.Add(new Vector2(x, y), seed);
            }


            MidPntDisplmnt disp = new MidPntDisplmnt(ref currentLocalMap, 1f, mapSeeds, seed);

            if (sides)
                disp.SetBorderAction(localBorderAction);

            disp.Generate();

            int[,] border = new int[4, MAPSIZE];

            for (int i = 0; i < MAPSIZE; i++)
            {
                border[0, i] = currentLocalMap[0, i];
                border[1, i] = currentLocalMap[i, 0];
                border[2, i] = currentLocalMap[MAPSIZE - 1, i];
                border[3, i] = currentLocalMap[i, MAPSIZE - 1];
            }

            if (!localMapBorder.ContainsKey(new Vector2(x, y)))
                localMapBorder.Add(new Vector2(x, y), border);
        }

        int localBorderAction(int x, int y)
        {
            int[,] border;
            if (localMapBorder.TryGetValue(new Vector2(currentLocalMapX, currentLocalMapY), out border))
            {
                int res;
                if (x == 0)
                    res = border[0, y];
                else if (y == 0)
                    res = border[1, x];
                else if (x == MAPSIZE - 1)
                    res = border[2, y];
                else
                    res = border[3, x];
                return res;
            }
            else
            {
                int dx = x == 0 ? -1 : x == MAPSIZE - 1 ? 1 : 0;
                int dy = y == 0 ? -1 : y == MAPSIZE - 1 ? 1 : 0;
                if (localMapBorder.TryGetValue(new Vector2(currentLocalMapX + dx, currentLocalMapY + dy), out border))
                {
                    int res;
                    if (x == 0)
                        res = border[2, y];
                    else if (y == 0)
                        res = border[3, x];
                    else if (x == MAPSIZE - 1)
                        res = border[0, y];
                    else
                        res = border[1, x];
                    return res;
                }
            }
            return -1;
        }

        public void GenMap()
        {
            MidPntDisplmnt HeightGen = new MidPntDisplmnt(ref heightMap, 0.750f, -1);
            HeightGen.Generate();

            Erosion erosion = new Erosion(this);
            erosion.Erode();
           
            RainFall rain = new RainFall(this);
            rain.RainSim();

            BiomeGen biome = new BiomeGen(this);
            biome.GenBiomes();

            //Random r = new Random();
            //int x, y;
            //do
            //{
            //    x = r.Next(0, MAPSIZE);
            //    y = r.Next(0, MAPSIZE);
            //} while (heightMap[x,y] < 25);

            ////localMapSeeds[x, y] = 5;
            ////GenLocalMap(x, y, true, true);
            ////Console.WriteLine("{0}:{1}", x, y);

        }

        public void SaveImage(string path)
        {
            

            Bitmap img = new Bitmap(MAPSIZE, MAPSIZE);
            for (int i = 0; i < MAPSIZE; i++)
                for (int j = 0; j < MAPSIZE; j++)
                {
                    int h = heightMap[i, j];
                    if (!SHOW_WATER)
                        img.SetPixel(i, j, sColor.FromArgb(h, h, h));
                    else
                    {
                        img.SetPixel(i, j, h <= WATER_LEVEL ? sColor.FromArgb(0, 0, h * 255 / WATER_LEVEL) : sColor.FromArgb(0, h, 0));
                    }
                }
            img.Save(path + "-height.bmp");

            img = new Bitmap(MAPSIZE, MAPSIZE);
            for (int i = 0; i < MAPSIZE; i++)
                for (int j = 0; j < MAPSIZE; j++)
                {
                    int h = currentLocalMap[i, j];
                    if (!SHOW_WATER)
                        img.SetPixel(i, j, sColor.FromArgb(h, h, h));
                    else
                    {
                        img.SetPixel(i, j, h <= WATER_LEVEL ? sColor.FromArgb(0, 0, h * 255 / WATER_LEVEL) : sColor.FromArgb(0, h, 0));
                    }
                }
            img.Save(path + "-local.bmp");

            img = new Bitmap(MAPSIZE, MAPSIZE);
            for (int i = 0; i < MAPSIZE; i++)
                for (int j = 0; j < MAPSIZE; j++)
                {
                    int h = rainMap[i, j];
                    if (heightMap[i, j] == WATER_LEVEL)
                        img.SetPixel(i, j, sColor.Black);
                    else
                        img.SetPixel(i, j, sColor.FromArgb(h, h, h));
                }
            img.Save(path + "-rain.bmp");

            img = new Bitmap(MAPSIZE, MAPSIZE);
            for (int i = 0; i < MAPSIZE; i++)
                for (int j = 0; j < MAPSIZE; j++)
                {
                    int h = tempMap[i, j];
                    if (heightMap[i, j] <= WATER_LEVEL)
                        img.SetPixel(i, j, sColor.Black);
                    else
                        img.SetPixel(i, j, sColor.FromArgb(h, h, h));
                }
            img.Save(path + "-temp.bmp");

            img = new Bitmap(MAPSIZE, MAPSIZE);
            for (int i = 0; i < MAPSIZE; i++)
                for (int j = 0; j < MAPSIZE; j++)
                {
                    //sColor c = sColor.FromArgb(255,tempMap[i,j], 0, rainMap[i,j]);
                    BiomeType h = biomeMap[i, j];
                    sColor c = sColor.Black;
                    switch (h)
                    {
                        case BiomeType.SubtropicalDesert:
                            c = sColor.Yellow;
                            break;
                        case BiomeType.GrasslandDesert:
                            c = sColor.YellowGreen;
                            break;
                        case BiomeType.Savanna:
                            c = sColor.Orange;
                            break;
                        case BiomeType.Shrubland:
                            c = sColor.LightGreen;
                            break;
                        case BiomeType.Taiga:
                            c = sColor.LightBlue;
                            break;
                        case BiomeType.Tundra:
                            c = sColor.White;
                            break;
                        case BiomeType.TemperateForest:
                            c = sColor.Green;
                            break;
                        case BiomeType.TropicalRainforest:
                            c = sColor.DarkGreen;
                            break;
                        case BiomeType.TemperateRainforest:
                            c = sColor.Teal;
                            break;
                        case BiomeType.Water:
                            c = sColor.DarkBlue;
                            break;
                        case BiomeType.None:
                            c = sColor.Black;
                            break;
                        default:
                            break;
                    }
                    img.SetPixel(i, j, c);

                }
            img.Save(path + "-biome.bmp");//*/

            img = new Bitmap(MAPSIZE, MAPSIZE);
            for (int i = 0; i < MAPSIZE; i++)
                for (int j = 0; j < MAPSIZE; j++)
                {
                    sColor c = sColor.FromArgb(255,tempMap[i,j], 0, rainMap[i,j]);
                    img.SetPixel(i, j, c);

                }
            img.Save(path + "-biome2.bmp");//*/
        }
    }
}
