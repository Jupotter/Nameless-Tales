using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using GameTime = Microsoft.Xna.Framework.GameTime;

namespace RPGProject.Dungeons
{
    class RoomGraph
    {
        static int ID = 0;

        enum NeighbourgPos
        {
            Up, Down, Left, Right
        }

        DungeonRoom room;
        Dictionary<NeighbourgPos,RoomGraph> neighbourg;
        BSP pos;
        int id;
        bool drawn = false;

        public RoomGraph()
        {
            neighbourg = new Dictionary<NeighbourgPos, RoomGraph>(4);
            id = ID++;
        }

        void addNeighbourg(RoomGraph value, NeighbourgPos pos)
        {
            neighbourg.Add(pos, value);
        }

        void changeNeighbourg(RoomGraph value, NeighbourgPos pos)
        {
            neighbourg.Remove(pos);
            neighbourg.Add(pos, value);
        }

        public void GenFromBSP(BSP bsp)
        {
            RoomGraph next;

            while (!bsp.Leaf)
            {
                next = new RoomGraph();
                if (bsp.Horizontal)
                {
                    if (neighbourg.ContainsKey(NeighbourgPos.Right))
                    {
                        next.addNeighbourg(neighbourg[NeighbourgPos.Right], NeighbourgPos.Right);
                        neighbourg[NeighbourgPos.Right].changeNeighbourg(next, NeighbourgPos.Left);
                        this.changeNeighbourg(next, NeighbourgPos.Right);
                    }
                    else
                    {
                        this.addNeighbourg(next, NeighbourgPos.Right);
                    }
                    next.addNeighbourg(this, NeighbourgPos.Left);
                }
                else
                {
                    if (neighbourg.ContainsKey(NeighbourgPos.Down))
                    {
                        next.addNeighbourg(neighbourg[NeighbourgPos.Down], NeighbourgPos.Down);
                        neighbourg[NeighbourgPos.Down].changeNeighbourg(next, NeighbourgPos.Up);
                        this.changeNeighbourg(next, NeighbourgPos.Down);
                    }
                    else
                    {
                        this.addNeighbourg(next, NeighbourgPos.Down);
                    }
                    next.addNeighbourg(this, NeighbourgPos.Up);
                }
                next.GenFromBSP(bsp.Right);
                bsp = bsp.Left;
            }
            pos = bsp;

            room = new DungeonRoom(new Vector3(bsp.Width, 1, bsp.Height), new Vector3(bsp.X, 0, bsp.Y));
            room.LoadRoom();
        }

        public override string ToString()
        {
            return String.Format("RoomGraph: {0}", id); 
            //return base.ToString();
        }

        public void SetCamera(Camera cam)
        {
            if (!drawn)
            {
                drawn = true;
                room.SetCamera(cam);
                foreach (RoomGraph n in neighbourg.Values)
                {
                    if (!n.drawn)
                        n.SetCamera(cam);
                }
                drawn = false;
            }
        }
        public void Draw(GameTime gametime)
        {
            if (!drawn)
            {
                drawn = true;
                room.Draw(gametime);
                foreach (RoomGraph n in neighbourg.Values)
                {
                    if (!n.drawn)
                        n.Draw(gametime);
                }
                drawn = false;
            }
        }
    }
}
