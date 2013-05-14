using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGProject
{
   public class InventoryComponent :DrawableGameComponent
    {
        public string[] elements = new string[3];
        int number;
   public     bool visible;
        Vector2 position; 
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public InventoryComponent(string name, string value, string pods,int number ,bool visible):base(Tools.Quick.game)
        {
            elements[0] = name;
            elements[1] = value;
            elements[2] = pods;
            this.number = number;
            this.visible = visible;
       
        }

        public override void Draw(GameTime gameTime)
        {
            if (visible)
            {
                Tools.Quick.spriteBatch.DrawString(Tools.Quick.dicoFont[Tools.TypeFont.Texte], "[" + number + "]" + elements[0] + elements[1] + elements[2], position, Color.White);
            }
                base.Draw(gameTime);
        }

        public void switchVisibility()
        {
            visible = !visible; 
        }

        public string getAffichage()
        {
          return  "[" + number + "]" + elements[0] + elements[1] + elements[2];
        }
        public void delete()
        {
            base.Game.Components.Remove(this);
        }
    }
}
