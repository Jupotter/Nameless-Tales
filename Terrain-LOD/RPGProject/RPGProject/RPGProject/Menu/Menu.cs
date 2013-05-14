using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace RPGProject
{
    public enum TypeMenu { Principal,Inventaire,StartUp }

  public  class Menu : DrawableGameComponent
    {
       
     public   SpriteFont police ;
     public   List<ClickableArea> menuclick = new List<ClickableArea>();
     public int valMolette;
     public   TypeMenu type;
     public Inventory inventoryPlayer;
     public List<InventoryComponent> inventoryMenu = new List<InventoryComponent>();
     public Menu(SpriteFont police , TypeMenu tpm ):base(Tools.Quick.game)
        {
          
            this.police = police;
            type = tpm;
            init();
        }

        public void init()
        {
            menuclick = new List<ClickableArea>();
            switch (type)
            {
                case TypeMenu.Principal: initPrincipal();
                    break;

                case TypeMenu.Inventaire : menuclick=Tools.Quick.game.inf.getzoneclick();
                    break;

                default:
                    break;
            }

        }

        private void initPrincipal()
        {
            float espacement = police.MeasureString("A").Y;
            menuclick.Add(new ClickableArea("Annuler", police, new Vector2(Tools.Quick.bounds.X / 2 - police.MeasureString("Annuler").X / 2, Tools.Quick.bounds.Y / 2), Tools.Quick.onClick.annulerMenu));//3
            menuclick.Add(new ClickableArea("Menu Principal", police, new Vector2(Tools.Quick.bounds.X / 2 - police.MeasureString("Menu Principal").X / 2, Tools.Quick.bounds.Y / 2 + espacement), Tools.Quick.onClick.menuPrincipal));//4
            menuclick.Add(new ClickableArea("Quitter", police, new Vector2(Tools.Quick.bounds.X / 2 - police.MeasureString("Quitter").X / 2, Tools.Quick.bounds.Y / 2 + espacement * 2), Tools.Quick.onClick.quitter));//5
            menuclick.Add(new ClickableArea("Charger", police, new Vector2(Tools.Quick.bounds.X / 2 - police.MeasureString("Charger").X / 2, Tools.Quick.bounds.Y / 2 - espacement), Tools.Quick.onClick.load)); //2
            menuclick.Add(new ClickableArea("Sauvegarder", police, new Vector2(Tools.Quick.bounds.X / 2 - police.MeasureString("Sauvegarder").X / 2, Tools.Quick.bounds.Y / 2 - espacement * 2), Tools.Quick.onClick.save)); //1


        }

      

        public override void Draw(GameTime gametime)
        {
            if(Visible)
            {
            switch (type)
            {
                case TypeMenu.Principal: foreach (ClickableArea cla in menuclick)
                    {
                        cla.Draw(gametime);
                    }
                    break;
                case TypeMenu.Inventaire: Tools.Quick.game.inf.Draw(gametime) ;
                    break;
                default:
                    break;
            }
            }
            
        }

      
        public void setVisibility(bool visible)
        {
            Visible = visible;
            switch (type)
            {
                case TypeMenu.Inventaire: Tools.Quick.game.inf.switchvisible();
                    break;
                case TypeMenu.Principal: foreach (ClickableArea ivc in menuclick) { ivc.Visible = visible; }
                    break;
            }
            if (visible)
            {
                valMolette = Tools.Quick.actmouse.getMouseWheel();
            }
            
        }


        public void clean()
        {
            switch (type)
            {
                case TypeMenu.Inventaire: foreach (InventoryComponent ivc in inventoryMenu) { ivc.delete(); }
                    break;
                case TypeMenu.Principal: foreach (ClickableArea ivc in menuclick) { ivc.delete(); }
                    break;
            }
        }


        public void RightClick(Vector2 position)
        {
            if (type == TypeMenu.Inventaire)
            {
                Tools.Quick.game.inf.RightClick(position);
            }
        }
    }
}
