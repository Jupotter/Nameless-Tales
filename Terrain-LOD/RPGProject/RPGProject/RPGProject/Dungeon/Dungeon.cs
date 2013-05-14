using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.DungeonNS;

namespace RPGProject
{
    class Dungeon
    {
        const int WIDTH = 79;
        const int HEIGHT = 79;

        Random rand;

        BSP rooms;
        bool[,] tiles;

        public Dungeon(int seed)
        {
            rand = new Random(seed);
            rooms = new BSP(0, 0, WIDTH, HEIGHT);
            tiles = new bool[WIDTH, HEIGHT];
        }

        public void Generate()
        {
            rooms.SplitRecursive(rand, 10, 4, 4, 1.5f, 1.5f);
            GenRoom(rooms);
        }

        private void GenRoom(BSP bsp)
        {
            if (bsp.Leaf)
            {
                int x = bsp.X+1;
                int y = bsp.Y+1;
                int w = bsp.Width-2;
                int h = bsp.Height-2;
                for (int i = x; i < x + w; i++)
                    for (int j = y; j < y + h; j++)
                    {
                        tiles[i, j] = true;
                    }
            }
            else
            {
                GenRoom(bsp.Left);
                GenRoom(bsp.Right);
            }
        }

        public void Print()
        {
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    Console.Write(tiles[i, j] ? '.' : '#');
                }
                Console.WriteLine();
            }
        }
    }
}

