using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace RPGProject
{
    public class ClickableArea : InterfaceComponnent
    {
        public string texte;
        public SpriteFont font;
        public Rectangle area;
        public Color color;
        public Tools.onClickFunction action;
        public Tools.onClickFunction Action
        {
            get { return action; }
            set { action = value; }
        }
        public ClickableArea(string texte, SpriteFont font, Vector2 position, Tools.onClickFunction action)
            : base()
        {
            color = Color.Black;
            this.texte = texte;
            this.font = font;
            this.position = position;
            area = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), Convert.ToInt32(font.MeasureString(texte).X), Convert.ToInt32(font.MeasureString(texte).Y));
            this.action = action;
        }

        public override void Draw(GameTime gt)
        {
            Tools.Quick.spriteBatch.Begin();
            Tools.Quick.game.spriteBatch.DrawString(font, texte, position, color);
            Tools.Quick.spriteBatch.End();

        }
        public bool onItem(Vector2 pos, bool click)
        {
            //Console.WriteLine(pos);
            bool b = area.Contains(new Point(Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y)));
            //   Console.WriteLine(b);
            //bool b;
            //pos.X>=area.X &&pos.X<=area.X+area.Width &&pos.Y>=area.Y &&pos.Y<=area.Y+area.Height
            if (b)
            {
                //    b=true;
                color = Color.Red;
                if (click)
                {
                    onClick();
                }
            }
            else
            { //b = false; 
                color = Color.Black;
            }
            return b;
        }


        public void onClick()
        {
            action();
        }

    }
}
