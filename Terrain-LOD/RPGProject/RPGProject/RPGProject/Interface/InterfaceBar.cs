using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RPGProject
{
  public  enum TypeBar { Vertical, Horizontal }
   public enum PlayType { Mana, Life , Null }
   public class InterfaceBar:InterfaceComponnent
    {
        double percent;
        double max;
        int value;
        Texture2D apparance;
        double width;
        int height;
        double initwidth;
        double initheight;
        TypeBar type;
        PlayType gametype;
        public InterfaceBar(int max, int value, int width, int height, Vector2 position,TypeBar tb , PlayType pt):base()
        {
            percent = 1;
            this.type = tb;
            this.max = max;
            this.value = value;
            this.width = width;
            this.initwidth = width;
            this.initheight = height;
            this.height = height;
            this.position = position;
            this.gametype = pt;
            area = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), width, height);
            
            actualise();
        }

        public override void Draw(GameTime gametimer)
        {
            Tools.Quick.spriteBatch.Begin();
            Tools.Quick.game.spriteBatch.Draw(Tools.Quick.elementInterface["Back"], new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), Convert.ToInt32(initwidth), Convert.ToInt32(initheight)), Color.White);
                switch (gametype)
                {
                    case PlayType.Life: Tools.Quick.game.spriteBatch.Draw(Tools.Quick.elementInterface["Life"], new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y + (initheight - height)), Convert.ToInt32(width), Convert.ToInt32(height)), Color.White);
                        break;

                    case PlayType.Mana: Tools.Quick.game.spriteBatch.Draw(Tools.Quick.elementInterface["Mana"], new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y + (initheight - height)), Convert.ToInt32(width), Convert.ToInt32(height)), Color.White);
                        break;
                }
                Tools.Quick.game.spriteBatch.Draw(Tools.Quick.elementInterface["Surface"], new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), Convert.ToInt32(initwidth), Convert.ToInt32(initheight)), Color.White);
                       
            // spritebatch.Draw(Tools.Quick.groundTexture[GroundType.Dirt],position, new Rectangle(0, 0, Convert.ToInt32(width), Convert.ToInt32(height)), Color.Red);
                Tools.Quick.spriteBatch.End();
        }

        public void actualise()
        {

            percent = (value / max);
            if (type == TypeBar.Horizontal)
            {
                width = Convert.ToInt32(initwidth * percent);
            }
            else
            {
                height = Convert.ToInt32(initheight - (initheight * (1 - percent)));
            }
        }
        public void Add(int value)
    {
            
        this.value += value;
        if (this.value > max)
        {
            this.value = Convert.ToInt32(max);
        }
        actualise();
    }

        public void Substract(int value)
        {
            
                this.value -= value;
                if (this.value < 0)
                {
                    this.value = 0;
                }
            actualise();
        }


        public bool onItem(Vector2 pos, bool click)
        {
            //Console.WriteLine(pos);
            bool b = area.Contains(new Point(Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y)));
            
            if (b)
            {
               if (click)
                {
                    onClick();
                }
            }
            
            return b;
        }


        public void onClick()
        {
            Substract(1);
        }

    }
}
