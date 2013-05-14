using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
namespace RPGProject
{
    class Personnage 
    {
        Model model;
        Texture2D texture;
        Vector3 position;
        int argent;
        Inventory inventory;
      public  Dictionary<Stat, Characteristic> stats = new Dictionary<Stat, Characteristic>(); 
        string name;
        Faction faction;
        bool player;
        PlayerInterface interfacePlayer;
      

        public Personnage(string name, bool player, int argent, Inventory inventory, Model model)
        {
            this.name = name;
            this.player = player;
            this.argent = argent;
            this.model = model;
            this.inventory = inventory;
            init();
            Load("01");
        }

        public void Load(string fname)
        {
            stats.Clear();
            string file = fname;
            if (name.Split('.').Count() == 1)
            {
                file += ".pl";  
            }

           
                StreamReader sr = new StreamReader("Data\\" + file);
                this.name = sr.ReadLine();
                string statsi = sr.ReadLine();
                string statbonus = sr.ReadLine();
                int i = 0;
                foreach (string s in statsi.Split(','))
                {
                    int val = Convert.ToInt16(s);
                    int valbon = Convert.ToInt16(statbonus.Split(',')[i]);
                    stats.Add((Stat)i,new Characteristic((Stat)i,val,valbon));
                    i++;
                }

                string typesac = sr.ReadLine();
                inventory.setsacoche((TypeSacoche)Convert.ToInt16(typesac.Split(',')[0]), (TypeSacoche)Convert.ToInt16(typesac.Split(',')[1]));

                string inventaire = sr.ReadLine();
                foreach (string s in inventaire.Split(','))
                {
                    int code = Convert.ToInt16(s.Split('\'')[0]);
                    int qte = Convert.ToInt16(s.Split('\'')[1]);
                    if(qte>0)
                    {
                    inventory.addObject(Onglet.Armes, code, qte);
                        }

                }

                inventaire = sr.ReadLine();
                foreach (string s in inventaire.Split(','))
                {
                    int code = Convert.ToInt16(s.Split('\'')[0]);
                    int qte = Convert.ToInt16(s.Split('\'')[1]);
                    if (qte > 0)
                    {
                        inventory.addObject((Onglet.Armures), code, qte);
                    }

                }
                 inventaire = sr.ReadLine();
                foreach (string s in inventaire.Split(','))
                {
                    int code = Convert.ToInt16(s.Split('\'')[0]);
                    int qte = Convert.ToInt16(s.Split('\'')[1]);
                    if (qte > 0)
                    {
                        inventory.addObject((Onglet.Carquois), code, qte);
                    }
                }
                 inventaire = sr.ReadLine();
                foreach (string s in inventaire.Split(','))
                {
                    int code = Convert.ToInt16(s.Split('\'')[0]);
                    int qte = Convert.ToInt16(s.Split('\'')[1]);
                    if (qte > 0)
                    {
                        inventory.addObject((Onglet.Crochets), code, qte);
                    }
                }
                 inventaire = sr.ReadLine();
                foreach (string s in inventaire.Split(','))
                {
                    int code = Convert.ToInt16(s.Split('\'')[0]);
                    int qte = Convert.ToInt16(s.Split('\'')[1]);
                    if (qte > 0)
                    {
                        inventory.addObject((Onglet.Parchemins), code, qte);
                    }
                }
                 inventaire = sr.ReadLine();
                foreach (string s in inventaire.Split(','))
                {
                    int code = Convert.ToInt16(s.Split('\'')[0]);
                    int qte = Convert.ToInt16(s.Split('\'')[1]);
                    if (qte > 0)
                    {
                    inventory.addObject(Onglet.Potions, code, qte);
                }
                }
              
            


            
        }
        public void init()
        {
            if (player)
            {
                interfacePlayer = new PlayerInterface(100, 100);
            }
            foreach (ModelMesh mm in model.Meshes)
            {
          
            }
            
            

        }


        
        public void setDegat(int degat)
        {
            interfacePlayer.getHealthBar().Substract(degat);
        }
        
        public void setHeal(int heal)
        {
            interfacePlayer.getHealthBar().Add(heal);
       
        }

        public PlayerInterface getInterface()
        {
            return interfacePlayer;
    }

        public Model getModel()
        {
            return model;
        }

        public Inventory getInventory()
        {
            return (inventory);
        }
    }
}
