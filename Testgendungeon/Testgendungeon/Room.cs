using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Testgendungeon
{
    class Room
    {
        int[,] access;
        int sizemax = 500;
        int sizemin = 25;
        public Room(Random rnd)
        {
            access = new int[sizemax, sizemax];
            
            #region init
            for (int i = 0; i < sizemax; i++)
            {
                for (int j = 0; j < sizemax; j++)
                {
                    access[i, j] = 0;
                }
            }
            #endregion

            #region premier rectangle
            int sizerectangle = rnd.Next(sizemin, sizemax-4);
            int decal = (int)((sizemax - sizerectangle)/2);

            for (int i = decal; i < sizerectangle + decal; i++)
            {
                for (int j = decal; j < sizerectangle + decal; j++)
                {
                    access[i, j] = access[i, j] + 1;
                }
            }
            #endregion

           

            #region first circle

            int sizefirstCircle = (int)(rnd.Next(sizemin, sizemax));
            int espacement = (sizemax - sizefirstCircle)/2;
           
            for (int i = espacement; i < sizemax-espacement; i++)
            {
                for (int j = sizemax / 2 - (i * 2); j < sizemax / 2 + (i * 2); j++)
                {
                    if (!(i < 0 || j < 0 || i > sizemax - 1 || j > sizemax - 1 || i > sizefirstCircle || j > sizefirstCircle))
                    {
                        access[i, j] = access[i,j]+1;
                    }
                }
             
            }
           

            #endregion

           //#region ronge
           // float accelerateurderonge = 1f;
           // for (int i = 0; i < sizemax; i++)
           // {
           //     float txronge = 0;
           //     for (int j = 0; j < sizemax; j++)
           //     {
           //         if (access[i, j] > 0) txronge += accelerateurderonge;


           //         if (rnd.Next(0, 100) > txronge && access[i, j] > 0)
           //         {
           //             access[i, j]--;
           //         }

           //     }
           // }
           // #endregion

            #region ecriture
            int n = rnd.Next();
            if (File.Exists("test\\" + n + ".Bmp")) { File.Delete("test\\" + n + ".Bmp"); }
            
            Bitmap bmp = new Bitmap(sizemax,sizemax);
            for (int i = 0; i < sizemax; i++)
            {
                for (int j = 0; j < sizemax; j++)
                {
                    bmp.SetPixel(i, j, Color.FromArgb(0, access[i, j] * 50, 0));
                }
            }
            bmp.Save("test\\"+n+ ".Bmp");
            
            #endregion 
        }
    }
}
