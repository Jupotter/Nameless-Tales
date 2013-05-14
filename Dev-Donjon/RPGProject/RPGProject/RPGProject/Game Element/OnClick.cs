using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject
{
    class OnClick
    {

        
        public void save()
        {
            Map map = new Map();
            map.GenMap();
            map.SaveImage("map");
        }

        public void load()
        {

        }

        public void annulerMenu()
        {
            Tools.Quick.game.switchMenu(TypeMenu.Principal);
        }

        public void quitter()
        {
            Tools.Quick.game.Exit();
        }

        public void menuPrincipal()
        {

        }




    }
}
