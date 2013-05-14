using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace RPGProject
{
  public  class GameObject
    {
        
        ObjectModel objModel;
        Vector3 position;
        
        public GameObject(Vector3 position)
        {
            this.position = position;
        }

        public GameObject(ObjectModel objmodelbase)
        {
            objModel = objmodelbase;
        }

        public GameObject(Vector3 position,ObjectModel objmodelbase)
        {
            this.position = position;
            objModel = objmodelbase;
            objModel.pos = position;
           
         
        }

        public GameObject(GameObject source)
        {
            this.position = source.position;
            objModel = source.objModel;
           
        }

        public void setPosition(Vector3 position)
        {
            this.position = position;
            objModel.pos = position;
        }

        public bool OnView(Creature creat, int porte)
        {

            Plane view = new Plane(position, new Vector3(Convert.ToInt32(position.X + 90 * Math.Cos(MathHelper.ToRadians(-45))), Convert.ToInt32(position.Y), Convert.ToInt32(position.Z + 90 * Math.Cos(MathHelper.ToRadians(-45)))), new Vector3(Convert.ToInt32(position.X + 90 * Math.Cos(MathHelper.ToRadians(45))), Convert.ToInt32(position.Y), Convert.ToInt32(position.Z + 90 * Math.Cos(MathHelper.ToRadians(45)))));

            if (Vector3.Distance(creat.gameobject.position, position) < porte)
            {

                foreach (BoundingSphere bs in objModel.hitspheres)
                {
                    if (Convert.ToBoolean(bs.Intersects(view))) { return true; };
                }

            }
            return false;
           

        }

        public bool onRay(Creature creat, int porte)
        {
            Ray ray = new Ray(creat.gameobject.position, creat.lookat);
            if (Vector3.Distance(creat.gameobject.position, position) < porte)
            {
                foreach (BoundingSphere bs in objModel.hitspheres)
                {
                    if (Convert.ToBoolean(bs.Intersects(ray))) { return true; };
                }
            }
            return false;

        }

        public List<BoundingSphere> getHitSphere()
        {
            return objModel.hitspheres;
        }
        public void action()
        {
            Console.WriteLine("ok");
        }

        public Vector3 getposition()
        {
            return position;
        }
        public void Draw()
        {
            objModel.draw();
        }
    }
}
