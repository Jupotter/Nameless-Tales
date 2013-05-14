using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace RPGProject
{
   public class InterfaceComponnent:DrawableGameComponent
    {
        public string name;
        public bool visible;
        public bool enable;
        public Vector2 position;
        public Rectangle area;

        public InterfaceComponnent()
            : base(Tools.Quick.game)
        {
            this.DrawOrder = 10000;
            base.Game.Components.Add(this);
        }
       
        public override void Draw(GameTime t)
        {
        }
        public void delete()
        {
            base.Game.Components.Remove(this);
        }

    }
}
