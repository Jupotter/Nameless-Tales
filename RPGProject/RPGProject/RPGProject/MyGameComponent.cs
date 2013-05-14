using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGProject
{
    public abstract class MyGameComponent : GameComponent
    {
        static int currentID = 0;

        int id;
        public int ID
        { get { return id; } }

        public MyGameComponent(Game game)
            : base(game)
        {
            id = currentID;
            currentID++;
        }

        static public int GetNewID()
        {
            return currentID++;
        }
    }

    public abstract class MyDrawableGameComponent : DrawableGameComponent
    {
        int id;
        public int ID
        { get { return id; } }

        public MyDrawableGameComponent(Game game)
            : base(game)
        {
            id = MyGameComponent.GetNewID();
        }
    }
}
