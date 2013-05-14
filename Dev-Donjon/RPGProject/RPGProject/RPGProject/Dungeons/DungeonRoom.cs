using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using GameTime = Microsoft.Xna.Framework.GameTime;

namespace RPGProject.Dungeons
{
    class DungeonRoom
    {
        List<Tuple<int, int>> entries;
        Vector3 size;
        Vector3 position;

        DungeonRoomDrawable drawRoom;
        bool loaded;

        public DungeonRoom(Vector3 size, Vector3 position)
        {
            this.size = size;
            this.position = position;
            entries = new List<Tuple<int, int>>();
            loaded = false;
        }

        public void LoadRoom()
        {
            drawRoom = new DungeonRoomDrawableGen(size, entries);
            drawRoom.SetPosition(position);
        }

        public void SetCamera(Camera cam)
        {
            drawRoom.SetCamera(cam);
        }

        public void Draw(GameTime gametime)
        {
            drawRoom.Draw(gametime);
        }
    }
}
