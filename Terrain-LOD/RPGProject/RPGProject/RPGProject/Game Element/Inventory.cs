using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject
{
    public class Inventory
    {
        Dictionary <Onglet, List<ObjQte>> possess = new Dictionary<Onglet,List<ObjQte>>();
        TypeSacoche sac1 ;
        TypeSacoche sac2 ;
        
        public Inventory()
        {
         possess.Add(Onglet.Armes,new List<ObjQte>());
         possess.Add(Onglet.Armures, new List<ObjQte>());
         possess.Add(Onglet.Carquois, new List<ObjQte>());
         possess.Add(Onglet.Crochets, new List<ObjQte>());
         possess.Add(Onglet.Parchemins, new List<ObjQte>());
         possess.Add(Onglet.Potions, new List<ObjQte>());
        }
       
       


        public  Dictionary <Onglet, List<ObjQte>> getObjects()
        {
            return possess;
        }
        public List<ObjQte> getObjects(Onglet type)
        {
            return possess[type];
        }
        public void addObject(Onglet type,int id , int qte)
        {
            possess[type].Add(new ObjQte(Tools.Quick.objectlist[id],qte));
        }
        public void setsacoche(TypeSacoche tsac1, TypeSacoche tsac2)
        {
            sac1 = tsac1;
            sac2 = tsac2;
        }
      public  struct ObjQte
        {
            public Object obj;
            public int qte;
            public ObjQte(Object obj, int qte)
            {
                this.obj = obj;
                this.qte = qte;
            }
        }

    
    }
}
