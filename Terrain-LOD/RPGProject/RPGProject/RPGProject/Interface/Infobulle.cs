using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RPGProject
{
    class Infobulle : ClickableArea
    {

        List<string> info = new List<string>();

        Vector2 pos;

        InventoryStockableInterface parent;

        Texture2D backinfo;

        string t = "";

        Inventory.ObjQte cible;

        public Infobulle(List<string> linfos ,Texture2D back , Vector2 position , InventoryStockableInterface inv , Inventory.ObjQte cible):base("",Tools.Quick.dicoFont[Tools.TypeFont.Texte],position,inv.infobulleunshow)
        {

            pos = position;
            info = linfos;
            parent = inv;
            backinfo = back;
            this.cible = cible;
             t = "";
            foreach (string s in info)
            {
                t += s + Environment.NewLine;
            }
            this.area = new Rectangle((int)pos.X, (int)pos.Y, (int)font.MeasureString(t).X, (int)font.MeasureString(t).Y);

            this.color = Color.White;

            
         }
        public void unshow()
        {
           
        }
        public void Draw(SpriteBatch spb)
        {
           
            spb.Draw(backinfo, new Rectangle((int)pos.X, (int)pos.Y, (int)font.MeasureString(t).X, (int)font.MeasureString(t).Y), Color.White);
            spb.DrawString(this.font, t, position, Color.Black);
        }

    }
}
