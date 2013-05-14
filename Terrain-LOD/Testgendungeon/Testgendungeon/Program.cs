using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Testgendungeon
{
    class Program
    {

        static void Main(string[] args)
        {
            foreach (string f in Directory.GetFiles("test"))
            {
                File.Delete(f);
            }
            Random rnd = new Random();
            Room r = new Room(rnd);
            for (int i = 0; i < 50; i++)
            {
                 rnd = new Random(rnd.Next());
                 r = new Room(rnd);
            }


            #region old
            //    string historique = ""; ;
            //    string[,] dungeon;
            //    int freqrooom = 7;

            //    Console.WriteLine("Taille : ");
            //    int taille = Convert.ToInt32(Console.ReadLine());
            //    Console.WriteLine("Initialisation");
            //    dungeon = new string[taille, taille];

            //    int graine = new Random().Next();
            //    Random rnd = new Random(graine);
            //    historique += "graine : " + graine.ToString() + Environment.NewLine + "taille :" + taille.ToString() + Environment.NewLine;

            //    for (int j = 0; j < taille; j++)
            //    {
            //        for (int i = 0; i < taille; i++)
            //        {
            //            if (rnd.Next(0, 100) <= freqrooom)
            //            {
            //                dungeon[i, j] = "#";
            //            }
            //            else
            //            {
            //                dungeon[i, j] = "'";
            //            }
            //        }
            //    }

            //    historique += Environment.NewLine;

            //    for (int j = 0; j < taille; j++)
            //    {
            //        for (int i = 0; i < taille; i++)
            //        {
            //            historique += dungeon[i, j];
            //        }
            //        historique += Environment.NewLine;
            //    }

            //    Console.Write(historique);


            //    Console.WriteLine("Generation des couloirs");

            //    for (int j = 0; j < taille; j++)
            //    {
            //        for (int i = 0; i < taille; i++)
            //        {
            //            switch (dungeon[i, j])
            //            {
            //                case "#": traceroute(i, j, taille, dungeon);
            //                    break;

            //                case "-":
            //                    break;

            //                case "^":
            //                    break;

            //                case "v":
            //                    break;

            //                case "+":
            //                    break;
            //            }
            //        }
            //    }



            //    for (int j = 0; j < taille; j++)
            //    {
            //        for (int i = 0; i < taille; i++)
            //        {
            //            historique += dungeon[i, j];
            //        }
            //        historique += Environment.NewLine;
            //    }

            //    Console.Write(historique);
            //    historique += Environment.NewLine + "Amelioration : " + Environment.NewLine;

            //    verification(dungeon, taille-1);

            //    for (int j = 0; j < taille; j++)
            //    {
            //        for (int i = 0; i < taille; i++)
            //        {
            //            historique += dungeon[i, j];
            //        }
            //        historique += Environment.NewLine;
            //    }

            //    Console.Write(historique);
            //    historique += Environment.NewLine + "Elargissement : " + Environment.NewLine;
            //    elargissement(dungeon, taille-1, rnd);

            //    for (int j = 0; j < taille; j++)
            //    {
            //        for (int i = 0; i < taille; i++)
            //        {
            //            historique += dungeon[i, j];
            //        }
            //        historique += Environment.NewLine;
            //    }

            //    Console.Write(historique);

            //    if (File.Exists("test.txt")) { File.Delete("test.txt"); }
            //    File.Create("test.txt").Close();
            //    StreamWriter sw = new StreamWriter("test.txt");
            //    sw.WriteLine(historique);
            //    sw.Close();
            //    Console.Read();

            //}

            //static void traceroute(int i, int j, int size, string[,] dungeon)
            //{

            //    for (int k = 0; k < size; k++)
            //    {
            //        if (dungeon[k, j] == "#")
            //        {
            //            makeroute(i, j, k, j, dungeon);
            //        }

            //        if (dungeon[i, k] == "#")
            //        {
            //            makeroute(i, j, i, k, dungeon);
            //        }
            //    }



            //}

            //static void makeroute(int i, int j, int toi, int toj, string[,] dungeon)
            //{
            //    int incr = 1;
            //    if (i != toi)
            //    {
            //        if (i > toi)
            //        {
            //            incr = -1;  
            //        }

            //        for (i = i; i <= toi; i += incr)
            //        {
            //            if (dungeon[i, j] != "#" && dungeon[i, j] != "i" && dungeon[i, j] != "-")
            //            {
            //                dungeon[i, j] = "-";
            //            }else if (dungeon[i, j] == "i" )
            //            {
            //                dungeon[i, j] = "+";
            //            }
            //        }
            //        return;
            //    }

            //    if (j != toj)
            //    {
            //        if (j> toj)
            //        {
            //            incr = -1;
            //        }

            //        for (j = j; j <= toj; j += incr)
            //        {
            //            if (dungeon[i, j] != "#" && dungeon[i, j] != "-" && dungeon[i, j] != "i")
            //            {
            //                dungeon[i, j] = "i";
            //            }else if (dungeon[i, j] == "-")
            //            {
            //                dungeon[i, j] = "+";
            //            }

            //        }

            //        return;
            //    }

            //}

            //static void verification(string[,] dungeon , int size)
            //{

            //    for (int i = 1; i < size ; i++)
            //    {

            //        for (int j = 1; j < size; j++)
            //        {
            //            //if (i != 0) { if ((dungeon[j, i] == "i" || dungeon[j, i] == "-" || dungeon[j, i] == "#") && (dungeon[j, i - 1] == "-" || dungeon[j, i - 1] == "i")) { dungeon[j, i - 1] = ","; } }
            //            //if (i != size) { if ((dungeon[j, i] == "i" || dungeon[j, i] == "-" || dungeon[j, i] == "#") && (dungeon[j, i + 1] == "-" || dungeon[j, i + 1] == "i")) { dungeon[j, i + 1] = ","; } }
            //            //if (j != 0) { if ((dungeon[j, i] == "i" || dungeon[j, i] == "-" || dungeon[j, i] == "#") && (dungeon[j, i - 1] == "-" || dungeon[j - 1, i] == "i")) { dungeon[j - 1, i ] = ","; } }
            //            if ((dungeon[j, i] == "-") && (dungeon[j, i - 1] == "-" || dungeon[j, i - 1] == "i")) { dungeon[j, i - 1] = ","; } 
            //            if ((dungeon[j, i] == "-") && (dungeon[j, i + 1] == "-" || dungeon[j, i + 1] == "i")) { dungeon[j, i + 1] = ","; } 
            //            if ((dungeon[j, i] == "i") && (dungeon[j, i - 1] == "-" || dungeon[j - 1, i] == "i")) { dungeon[j - 1, i] = ","; }
            //            if ((dungeon[j, i] == "I") && (dungeon[j + 1, i] == "-" || dungeon[j + 1, i] == "i")) { dungeon[j + 1, i] = ","; } 

            //        }

            //    }

            //}

            //static void elargissement(string[,] dungeon, int size , Random rnd)
            //{
            //    int fac = 20;

            //    List<int[]> lint = new List<int[]>(); // i > 0 , j > 1
            //    int[] inter = new int[2];
            //    for (int i = 1; i < size; i++)
            //    {

            //        for (int j = 1; j < size; j++)
            //        {

            //            if (dungeon[j, i] == "#")
            //            {
            //                inter = new int[2];
            //                inter[0] = i;
            //                inter[1] = j;
            //                lint.Add(inter);
            //            }

            //        }

            //    }

            //    foreach (int[] inti in lint)
            //    {

            //        int rnda = rnd.Next(0, 100);
            //        for (int k = 0; k < ((rnda - (rnda % fac)) / fac); k++)
            //        {
            //            if (inti[0] != 0) { dungeon[inti[1] - 1, inti[0]] = "#"; }
            //            if (inti[1] != 0) { dungeon[inti[1], inti[0] - 1] = "#"; }
            //            if (inti[0] != size) { dungeon[inti[1] + 1, inti[0]] = "#"; }
            //            if (inti[1] != size) { dungeon[inti[1], inti[0] + 1] = "#"; }
            //        }
            //    }





            //}
            #endregion


        } // le sup




    }



}
