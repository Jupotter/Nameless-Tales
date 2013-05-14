using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RPGProject
{
   public class PlayerInterface:DrawableGameComponent
    {
        InterfaceBar vie;
        InterfaceBar mana;
        List<ClickableArea> lrac;
        ClickableArea targetobject;
        double ratiobarwidth = 0.0535;
        double ratiobarheight = 0.02;
        int sizeShortcut = 50;
        int width = 30;
        int height = 200;
        List<Shortcut> shortcuts = new List<Shortcut>();

        public PlayerInterface(int vie, int mana):base(Tools.Quick.game)
        {
            double screenW = Tools.Quick.graphics.PreferredBackBufferWidth;
            double screenH = Tools.Quick.graphics.PreferredBackBufferHeight;

            this.vie = new InterfaceBar(vie, vie, width, height, new Vector2(0, Convert.ToInt32(screenH - height)), TypeBar.Vertical, PlayType.Life); ;
            this.mana = new InterfaceBar(mana, mana, width, height, new Vector2(Convert.ToInt32(screenW - width), Convert.ToInt32(screenH - height)), TypeBar.Vertical, PlayType.Mana); ;
            
            shortcuts.Add(new Shortcut(new Rectangle(Convert.ToInt32(screenW/2),Convert.ToInt32(screenH -(sizeShortcut+10)),sizeShortcut,sizeShortcut)));
              shortcuts.Add(new Shortcut(new Rectangle(Convert.ToInt32(screenW/2)-(sizeShortcut + 20),Convert.ToInt32(screenH - (sizeShortcut+10)),sizeShortcut,sizeShortcut)));
            shortcuts.Add(new Shortcut(new Rectangle(Convert.ToInt32(screenW/2)-(sizeShortcut*2 + 20),Convert.ToInt32(screenH - (sizeShortcut+10)),sizeShortcut,sizeShortcut)));
            shortcuts.Add(new Shortcut(new Rectangle(Convert.ToInt32(screenW/2)+(sizeShortcut + 20),Convert.ToInt32(screenH - (sizeShortcut+10)),sizeShortcut,sizeShortcut)));
             shortcuts.Add(new Shortcut(new Rectangle(Convert.ToInt32(screenW/2)+(sizeShortcut*2 + 20),Convert.ToInt32(screenH - (sizeShortcut+10)),sizeShortcut,sizeShortcut)));
             targetobject = new ClickableArea("",Tools.Quick.dicoFont[Tools.TypeFont.Texte], new Vector2(Game.Window.ClientBounds.Width / 2, (Game.Window.ClientBounds.Height / 2)), voidfunc);
        
        
        }

        public void Draw(GameTime spb)
        {
            vie.Draw(spb);
            mana.Draw(spb);
            targetobject.Draw(spb);
            foreach (Shortcut s in shortcuts)
            {
                s.Draw(spb);
            }
        }

        public InterfaceBar getHealthBar()
        {
            return vie;
        }

        public InterfaceBar getManaBar()
        {
            return mana;
        }

        public void setVisibility(bool visible)
        {
            vie.Visible = visible;
            mana.Visible = visible;
            foreach (Shortcut s in shortcuts)
            {
                s.Visible=(visible);
            }
        }

        public void voidfunc() { }

        public void changetexte(string texte)
        {
            if (texte != targetobject.texte)
            {
                Console.WriteLine(texte);
                targetobject.texte = texte;
            }
        }

    }
}
