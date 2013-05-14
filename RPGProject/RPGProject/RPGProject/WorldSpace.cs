using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGProject
{
    class WorldSpace : MyGameComponent
    {
        GameComponentCollection content;

        public WorldSpace(Game game)
            : base(game)
        {
            content = new GameComponentCollection();
        }

        public void Add(GameComponent item)
        {
            content.Add(item);
        }

        public void Initialize()
        {
            foreach (GameComponent item in content)
            {
                item.Initialize();
            }
        }

        public void Load()
        {
            foreach (GameComponent item in content)
            {
                Game.Components.Add(item);
            }
        }

        public void Unload()
        {
            foreach (GameComponent item in content)
            {
                Game.Components.Remove(item);
            }
        }
    }
}
