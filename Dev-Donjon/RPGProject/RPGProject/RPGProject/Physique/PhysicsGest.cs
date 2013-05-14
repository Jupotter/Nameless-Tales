using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace RPGProject
{
    class PhysicsGest
    {
        public bool activate;
        Ground gnd;
        List<Object> lo = new List<Object>();
        int tailleAvat = 2;
        public PhysicsGest(Ground actualground, List<Object> listofobjectinthemap)
        {
            activate = true;
            gnd = actualground;
            lo = listofobjectinthemap;
        }

        public bool canmoveto(Vector3 vec)
        {

            foreach (Object o in lo)
            {
                if (o.position == vec)
                {
                    return false;
                }
            }
            return true;
        }

        public int makegravity(Vector3 position)
        {
            int force = 0;

          

            
            return force;
        }
        public void onGround(Vector3 cameraposition,Camera came)
        {
            int y = searchPosY(cameraposition);

            Vector3 newpos = new Vector3(came.position.X,y+tailleAvat, came.position.Z);
            if (activate) { came.modiposition(newpos, true); }

        }
      


        public int searchPosY(Vector3 position)
        {
            int z = 0;

            int x = 0;
            int y = 0;


            x = Convert.ToInt32(position.X / (LoadMap.taille ));
            y = (Convert.ToInt32(position.Z / (LoadMap.taille)));
            
            if (x < 0 || y < 0 || x > (Math.Sqrt(gnd.source.Length)-1) || y > (Math.Sqrt(gnd.source.Length)-1))
            {
                return 0;
            }

            z = gnd.source[y, x];
            z = Convert.ToInt32(z * LoadMap.taille / LoadMap.ajustZ);
          //  Console.WriteLine(z);

            return z;
        }
        public void switchactivate()
        {
            activate = !activate;
        }
    }
  
}
