using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGProject.Dungeons;
using Microsoft.Xna.Framework;

namespace RPGProject
{
    class Dungeon : MyDrawableGameComponent
    {
        const int WIDTH = 100;
        const int HEIGHT = 100;

        Random rand;

        RoomGraph rooms;
        bool[,] tiles;
        Camera camera;

        public Dungeon(int seed, Game game)
            : base(game)
        {
            rand = new Random(seed);
            rooms = new RoomGraph();
            tiles = new bool[WIDTH, HEIGHT];
        }

        public void Generate()
        {
            BSP rooms = new BSP(0, 0, WIDTH, HEIGHT);
            rooms.SplitRecursive(rand, 5, 4, 4, 1.3f, 1.3f);

            Console.WriteLine(rooms.ToString());
            GenRoom(rooms);
        }

        public void SetCamera(Camera cam)
        {
            camera = cam;
            
        }

        private void GenRoom(BSP bsp)
        {
            rooms.GenFromBSP(bsp);

            //if (bsp.Leaf)
            //{
            //    int x = bsp.X+1;
            //    int y = bsp.Y+1;
            //    int w = bsp.Width-2;
            //    int h = bsp.Height-2;
            //    for (int i = x; i < x + w; i++)
            //        for (int j = y; j < y + h; j++)
            //        {
            //            tiles[i, j] = true;
            //        }
            //}
            //else
            //{
            //    GenRoom(bsp.Left);
            //    GenRoom(bsp.Right);
            //}
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

        public override void Draw(GameTime gameTime)
        {
            rooms.Draw(gameTime);
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            
            this.Generate();
            rooms.SetCamera(camera);

            base.LoadContent();
        }
    }
}

