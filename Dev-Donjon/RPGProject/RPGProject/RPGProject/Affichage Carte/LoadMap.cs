using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace RPGProject
{
    static  class LoadMap
    {
        static public int taille =1; //32
        static public float ajustZ = 2;

        static public void loadground(string file, Ground source)
        {
            List<Tile> lt = new List<Tile>();
            if (!File.Exists(file)) { throw new Exception("Fichier introuvable"); }
            StreamReader sr = new StreamReader(file);
            int ligne = 0;
            while (!sr.EndOfStream)
            {
                lt.AddRange(stringtolistvec(sr.ReadLine(), ligne));
                ligne++;
            }
            source.SetGroundMap(lt, new int[2, 2]);
        }

        static public void loadground(Map map, Ground source)
        {
            Ground gnd;
            List<Tile> lts = new List<Tile>();
            Tile t;

            for (int i = 0; i < Map.MAPSIZE; i++)
            {
                for (int j = 0; j < Map.MAPSIZE; j++)
                {
                    //int suivi = x + 1;
                    //int suivj = y + 1;
                    //if (suivi <= Map.MAPSIZE)
                    //{
                    //    if (suivj <= Map.MAPSIZE)
                    //    {
                    int h1, h2, h3, h4;
                    h1 = map.HeightMap[i, j];
                    h2 = j + 1 == Map.MAPSIZE ? 0 : map.HeightMap[i, j + 1];
                    h3 = (i + 1 == Map.MAPSIZE) || (j + 1 == Map.MAPSIZE) ? 0 : map.HeightMap[i + 1, j + 1];
                    h4 = i + 1 == Map.MAPSIZE ? 0 : map.HeightMap[i + 1, j];
                    t = new Tile(new Vector3(i * taille, h1 * taille / ajustZ, j * taille),
                        new Vector3(i * taille, h2 * taille / ajustZ, (j + 1) * taille),
                        new Vector3((i + 1) * taille, h3 * taille / ajustZ, (j + 1) * taille),
                        new Vector3((i + 1) * taille, h4 * taille / ajustZ, j * taille),
                        map.BiomeMap[i,j]);
                    lts.Add(t);
                    //    }
                    //}

                }
            }

            for (int i = 0; i < lts.Count; i++)
            {
                Tile[,] calc = new Tile[3, 3];
                Tile tmp = lts[i];
                Tile ttmp;
                int msize = Map.MAPSIZE;

                tmp.n3 = tmp.normal1 + tmp.normal2;
                tmp.n1 = tmp.normal1 + tmp.normal2;
                tmp.n4 = tmp.normal1;
                tmp.n2 = tmp.normal2;

                if (i >= msize)
                {
                    ttmp = lts[i - (msize - 2)];
                    tmp.n1 += ttmp.normal1;
                    tmp.n2 += ttmp.normal1 + ttmp.normal2;
                    if (i % (msize) != 0)
                    {
                        ttmp = lts[i - msize];
                        tmp.n1 += ttmp.normal1 + ttmp.normal2;
                    }
                    if (i % (msize - 1) != 0)
                        tmp.n2 += lts[i - msize + 2].normal1;
                }
                if (i < (msize - 1) * (msize - 1) - msize)
                {
                    ttmp = lts[i + (msize - 2)];
                    tmp.n3 += ttmp.normal2;
                    tmp.n4 += ttmp.normal1 + ttmp.normal2;
                    if (i % (msize) != 0)
                        tmp.n4 += lts[i + msize - 2].normal2;
                    if (i % (msize - 1) != 0)
                    {
                        ttmp = lts[i + msize];
                        tmp.n3 += ttmp.normal1 + ttmp.normal2;
                    }
                }
                if (i % (msize) != 0)
                {
                    ttmp = lts[i - 1];
                    tmp.n4 += ttmp.normal2 + ttmp.normal1;
                    tmp.n1 += ttmp.normal2;
                }
                if (i % (msize - 1) != 0)
                {
                    ttmp = lts[i + 1];
                    tmp.n2 += ttmp.normal2 + ttmp.normal1;
                    tmp.n3 += ttmp.normal1;
                }
                tmp.n1.Normalize();
                tmp.n2.Normalize();
                tmp.n3.Normalize();
                tmp.n4.Normalize();
            }

            //lts.Reverse();
            source.SetGroundMap(lts, map.HeightMap);
        }

        static private List<Tile> stringtolistvec(string s, int ligne)
        {
            List<Tile> lt = new List<Tile>();
            Vector3 v1 = new Vector3();
            Vector3 v2 = new Vector3();
            Vector3 v3 = new Vector3();
            Vector3 v4 = new Vector3();
            float x = 0;
            int prec = 0;
            int valeloignement = (0);
            foreach (char c in s)
            {
                int suiv = Convert.ToInt32(c);
                //v1 = new Vector3(x - 64, -(float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height , (ligne * 64) + valeloignement);
                //v2 = new Vector3(x + 64, -(float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height , (ligne * 64) + valeloignement);
                //v3 = new Vector3(x + 64, -(float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height , ligne * 64 - 128 + valeloignement);
                //v4 = new Vector3(x - 64, -(float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height , ligne * 64 - 128 + valeloignement);

                v1 = new Vector3(x - 64, -(float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height + prec * 62, (ligne * 64) + valeloignement);
                v2 = new Vector3(x + 64, -(float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height, (ligne * 64) + valeloignement);
                v3 = new Vector3(x + 64, -(float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height, ligne * 64 - 128 + valeloignement);
                v4 = new Vector3(x - 64, -(float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height + suiv * 62, ligne * 64 - 128 + valeloignement);


                prec = suiv;


                x += 128;
                lt.Add(new Tile(v1, v2, v3, v4, BiomeType.None));
            }
            return lt;
        }
    }
}
