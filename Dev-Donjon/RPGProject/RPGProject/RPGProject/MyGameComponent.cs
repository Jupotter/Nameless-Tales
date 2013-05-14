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

        public override void Initialize()
        {
            Console.WriteLine("{0} ({1:X}): Initialize",this.ToString() , ID);
            base.Initialize();
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

        public override void Initialize()
        {
            Console.WriteLine("{0} ({1:X8}): Initialize", this.ToString(), ID);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("{0} ({1:X8}): LoadContent", this.ToString(), ID);
            base.LoadContent();
        }
    }
}
