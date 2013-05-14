using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RPGProject
{
    enum BiomeType
    {
        None = 0,
        Water = 1,
        SubtropicalDesert = 2,
        GrasslandDesert = 3,
        Savanna = 4,
        Shrubland = 5,
        Taiga = 6,
        Tundra = 7,
        TemperateForest = 8,
        TropicalRainforest = 9,
        TemperateRainforest = 10
    }

}

namespace RPGProject.MapGen
{
    class BiomeGen
    {
        List<BiomeStruct> BiomeList;

        Map map;

        int tempAltMax = 20;
        int tempAltMin = -30;
        int tempLatMax = 30;
        int tempLatMin = -30;

        public BiomeGen(Map map)
        {
            this.map = map;
            FillBiomeList();
        }

        int calcTemp(int i, int j)
        {
            float tempAlt, tempLat, temp;
            int alt;

            alt = map.HeightMap[i, j];
            tempAlt = ((tempAltMin - tempAltMax) / (float)255 * alt + tempAltMax);
            tempLat = ((tempLatMax - tempLatMin) / (float)Map.MAPSIZE * j + tempLatMin);
            temp = tempLat + tempAlt;

            map.tempMap[i, j] = Math.Max(0, Math.Min((int)((temp + 10) * 250 / (float)50), 255));
            return (int)temp;
        }

        public void GenBiomes()
        {
            int temp;
            int rain;

            for (int i = 0; i < Map.MAPSIZE - 1; i++)
                for (int j = 0; j < Map.MAPSIZE - 1; j++)
                {
                    if (map.HeightMap[i, j] > Map.WATER_LEVEL)
                    {
                        temp = calcTemp(i, j);
                        rain = map.RainMap[i, j];
                        map.SetBiome(i, j, FindBiome(temp, rain));
                    }
                    else
                        map.SetBiome(i, j, BiomeType.Water);
                }
        }

        struct BiomeStruct
        {
            public BiomeType Name;
            public int StartTemp, EndTemp;
            public int StartRain, EndRain;
        }

        public void FillBiomeList()
        {
            BiomeList = new List<BiomeStruct>();
            BiomeStruct b = new BiomeStruct();

            b.Name = BiomeType.SubtropicalDesert;
            b.StartRain = -1;
            b.EndRain = 50 * 256 / 400;
            b.StartTemp = 15;
            b.EndTemp = 50;

            BiomeList.Add(b);

            b.Name = BiomeType.GrasslandDesert;
            b.StartRain = -1;
            b.EndRain = 50 * 256 / 400;
            b.StartTemp = -10;
            b.EndTemp = 15;

            BiomeList.Add(b);

            b.Name = BiomeType.Tundra;
            b.StartRain = -1;
            b.EndRain = 256;
            b.StartTemp = -50;
            b.EndTemp = -10;

            BiomeList.Add(b);

            b.Name = BiomeType.Savanna;
            b.StartRain = 50 * 256 / 400;
            b.EndRain = 250 * 256 / 400;
            b.StartTemp = 20;
            b.EndTemp = 50;

            BiomeList.Add(b);

            b.Name = BiomeType.TropicalRainforest;
            b.StartRain = 250 * 256 / 400;
            b.EndRain = 256;
            b.StartTemp = 20;
            b.EndTemp = 50;

            BiomeList.Add(b);

            b.Name = BiomeType.Shrubland;
            b.StartRain = 50 * 256 / 400;
            b.EndRain = 100 * 256 / 400;
            b.StartTemp = -10;
            b.EndTemp = 20;

            BiomeList.Add(b);

            b.Name = BiomeType.TemperateForest;
            b.StartRain = 100 * 256 / 400;
            b.EndRain = 200 * 256 / 400;
            b.StartTemp = 5;
            b.EndTemp = 20;

            BiomeList.Add(b);

            b.Name = BiomeType.TemperateRainforest;
            b.StartRain = 200 * 256 / 400;
            b.EndRain = 256;
            b.StartTemp = 5;
            b.EndTemp = 20;

            BiomeList.Add(b);

            b.Name = BiomeType.Taiga;
            b.StartRain = 100 * 256 / 400;
            b.EndRain = 256;
            b.StartTemp = -10;
            b.EndTemp = 5;

            BiomeList.Add(b);
        }

        public BiomeType FindBiome(int temp, int rain)
        {
            try
            {
                BiomeStruct s = BiomeList.Find(
                     b => (b.EndRain >= rain && b.StartRain < rain)
                            && (b.EndTemp >= temp && b.StartTemp < temp));
                return s.Name;
            }
            catch
            {
                return BiomeType.None;
            }
        }
    }
}