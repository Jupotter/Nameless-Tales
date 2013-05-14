using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGProject
{
    public class Shortcut : InterfaceComponnent
    {

        public Rectangle datasizeposition;
        public Tools.onClickFunction action;
        public Tools.onClickFunction Action
        {
            get { return action; }
            set { action = value; }
        }

        public Shortcut(Rectangle datasizeposition)
            : base()
        {
            this.datasizeposition = datasizeposition;
        }

        public override void Draw(GameTime gametime)
        {
            Tools.Quick.spriteBatch.Begin();
            Tools.Quick.game.spriteBatch.Draw(Tools.Quick.elementInterface["Loupe"], datasizeposition, Color.White);
            Tools.Quick.spriteBatch.End();
        }
    }
}
