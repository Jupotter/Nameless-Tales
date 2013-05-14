using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RPGProject
{
    public enum TypeSacoche { Parchemins=1 , Potion=2, Crochet=3, Carquois=4 }
    public enum Onglet { Armes, Armures, Parchemins, Potions, Crochets, Carquois }
    class InventoryStockableInterface
    {
        Onglet ongletActif = Onglet.Armes;
        Texture2D back = null;
        int nbemplacementArmures = 4;
        int nbemplacementArmes = 4;
        int nbemplacementPotions = 16;
        int nbemplacementCrochet = 16;
        int nbemplacementFléche = 0;
        int nbemplacementParchemin = 4;
        TypeSacoche sac1 = TypeSacoche.Potion;
        TypeSacoche sac2 = TypeSacoche.Crochet;
        public   List<ClickableArea> onglet = new List<ClickableArea>();
        bool visible = false;
        Infobulle info = null;
        int actualsize = 0;
        Rectangle position;
        Rectangle positionongl;
        InventoryInterface  parent;


        public InventoryStockableInterface(Texture2D back, TypeSacoche sacoche1, TypeSacoche sacoche2,Rectangle position , InventoryInterface invoc )
        {
            parent = invoc;
            this.position = positionongl = position;
            sac1 = sacoche1;
            sac2 = sacoche2;
            this.back = back;
            int ongX = positionongl.X+10;
            Onglet o = Onglet.Armes;
            onglet.Add(new ClickableArea(o.ToString(), Tools.Quick.dicoFont[Tools.TypeFont.Texte], new Vector2(ongX, positionongl.Y), () => changeOnglet(Onglet.Armes)));
            ongX += (int)Tools.Quick.dicoFont[Tools.TypeFont.Texte].MeasureString(o.ToString()).X + 10;

             o = Onglet.Armures;
             onglet.Add(new ClickableArea(o.ToString(), Tools.Quick.dicoFont[Tools.TypeFont.Texte], new Vector2(ongX, positionongl.Y), () => changeOnglet(Onglet.Armures)));
            ongX += (int)Tools.Quick.dicoFont[Tools.TypeFont.Texte].MeasureString(o.ToString()).X + 10;

            o = Onglet.Potions;
            onglet.Add(new ClickableArea(o.ToString(), Tools.Quick.dicoFont[Tools.TypeFont.Texte], new Vector2(ongX, positionongl.Y), () => changeOnglet( Onglet.Potions)));
            ongX += (int)Tools.Quick.dicoFont[Tools.TypeFont.Texte].MeasureString(o.ToString()).X + 10;

            o = Onglet.Crochets;
            onglet.Add(new ClickableArea(o.ToString(), Tools.Quick.dicoFont[Tools.TypeFont.Texte], new Vector2(ongX, positionongl.Y), () => changeOnglet(Onglet.Crochets)));
            ongX += (int)Tools.Quick.dicoFont[Tools.TypeFont.Texte].MeasureString(o.ToString()).X + 10;

            o = Onglet.Parchemins;
            onglet.Add(new ClickableArea(o.ToString(), Tools.Quick.dicoFont[Tools.TypeFont.Texte], new Vector2(ongX, positionongl.Y), () => changeOnglet(Onglet.Parchemins)));
            ongX += (int)Tools.Quick.dicoFont[Tools.TypeFont.Texte].MeasureString(o.ToString()).X + 10;

            o = Onglet.Carquois;
            onglet.Add(new ClickableArea(o.ToString(), Tools.Quick.dicoFont[Tools.TypeFont.Texte], new Vector2(ongX, positionongl.Y), () => changeOnglet(Onglet.Carquois)));
            ongX += (int)Tools.Quick.dicoFont[Tools.TypeFont.Texte].MeasureString(o.ToString()).X + 10;

            foreach (ClickableArea ca in onglet)
            {
                ca.Visible = false;
                ca.Visible = false ;
            }

     
        }

        public void Draw(SpriteBatch spb )
        {
            if (!visible) return;
            
            int qte = 0;
            switch (ongletActif)
            {
                    
                case Onglet.Armes:
                    qte = nbemplacementArmes;
                    
                    break;
                case Onglet.Armures:
                    qte = nbemplacementArmures;
                    break;
                case Onglet.Parchemins:
                    qte = nbemplacementParchemin;
                    break;
                case Onglet.Potions:
                    qte = nbemplacementPotions;
                    break;
                case Onglet.Crochets:
                    qte = nbemplacementCrochet;
                    break;
                case Onglet.Carquois:
                    qte = nbemplacementFléche;
                    break;
                default: qte = 0;
                    break;
            }

            position.Y = positionongl.Y+((int)Tools.Quick.dicoFont[Tools.TypeFont.Texte].MeasureString("Armes").Y + 5);
     
            int bar = 0;

            Vector2 pos =new Vector2( position.X,position.Y);
            
            int size = 0;

            if (qte != 0)

            {
                if (position.Height > position.Width)
                {
                    size = Convert.ToInt32((position.Width/2)/Math.Sqrt(qte));
                }
                else
                {
                    size = Convert.ToInt32((position.Width/2) / Math.Sqrt(qte));
                }

            }
            actualsize = size;
            List<Inventory.ObjQte> possess = Tools.Quick.player.getInventory().getObjects(ongletActif);
            for (int i = 0; i < qte; i++)
            {
                spb.Draw(back, new Rectangle((int)pos.X,(int) pos.Y, size, size), Color.White);

                if (possess.Count > 0 && possess.Count > i)
                {
                    spb.Draw(possess[i].obj.appInventory, new Rectangle((int)pos.X, (int)pos.Y, size, size), Color.White);
                }
                pos.X += size;
                if (pos.X >= (position.Location.X + position.Width))
                {
                    pos = new Vector2(position.X, pos.Y + size);
                }
            }

            if (info != null)
            {
                info.Draw(spb);
            }

        }

        public void changeOnglet(Onglet onglet)
        {
            ongletActif = onglet;
        }

        public void switchvisible()
        {
            foreach (ClickableArea ca in onglet)
            {
                ca.Visible = !ca.Visible;
                ca.visible = !ca.visible;
            }
            visible = !visible;
        }

        public void infobulleunshow()
        {
            info.unshow();
            info = null;
        }

       public void generateinfobulle(Vector2 pos)
    {
        if (position.Contains((int)pos.X,(int) pos.Y))
        {

            if (info != null)
            {
                infobulleunshow();
            }
            int x = (int)pos.X;
            int y = (int)pos.Y;
            
            int numelex =Convert.ToInt16( Math.Floor((((float)(x-position.X)) / ((float)actualsize))+1));
            int numeley = Convert.ToInt16( Math.Floor((((float)(y - position.Y)) / ((float)actualsize))));
            int nbele = (position.Location.X + position.Width) / actualsize;

            
            int num = nbele * numeley + numelex;

            List<Inventory.ObjQte> possess = Tools.Quick.player.getInventory().getObjects(ongletActif);

            if (possess.Count > num)
            {
                List<string> inf = new List<string>();
                inf.Add(possess[num].obj.nomaffichage);
                inf.Add(possess[num].obj.value.ToString() + " po");
                info = new Infobulle(inf,parent.backwind , pos, this, possess[num]);
                Console.WriteLine(num);
            }


        }
    }

    }
}
