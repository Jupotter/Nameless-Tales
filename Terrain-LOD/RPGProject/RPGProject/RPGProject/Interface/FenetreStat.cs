using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace RPGProject
{
    class FenetreStat
    {
        Texture2D back;
        Personnage player;
        bool visible = false;
        public FenetreStat(Personnage playerA,Texture2D background)
        {

            back = background;
            player = playerA;
        }

        public void Draw(SpriteBatch sp, Rectangle taille)
        {

            if (!visible) return;

            sp.Draw(back, taille, Color.White);

            float pHeight = 20 ;
            

            foreach(var v in player.stats)
            {
                Vector2 taillestr = Tools.Quick.dicoFont[Tools.TypeFont.Texte].MeasureString(v.Key.ToString());
                Vector2 pos = new Vector2(taille.Width+(taille.Width / 5) * 2 - (taillestr.X / 2), pHeight);
                sp.DrawString(Tools.Quick.dicoFont[Tools.TypeFont.Texte], v.Key.ToString(), pos, Color.Black);
                taillestr = Tools.Quick.dicoFont[Tools.TypeFont.Texte].MeasureString(v.Value.ToString());
                Color cls = Color.Black;
                if (v.Value.bonus > 0) { cls = Color.Green; }
                else if (v.Value.bonus < 0) { cls = Color.Red; }
                pos = new Vector2(taille.Width+(taille.Width / 5) * 4 - (taillestr.X / 2), pHeight);
                sp.DrawString(Tools.Quick.dicoFont[Tools.TypeFont.Texte], v.Value.realvalue.ToString(), pos, cls);

                pHeight += taillestr.Y + 5;
            }
          

        }

        public void switchvisible()
        {
            visible = !visible;
        }
    }
}
