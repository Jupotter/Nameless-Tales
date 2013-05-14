using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace RPGProject
{
    public class ObjectModel
    {
       public List<BoundingSphere> hitspheres = new List<BoundingSphere>();
        Model apparance;
        public Vector3 pos;
        public string name = "";

        public ObjectModel(Model view , Vector3 position , string nameori)
        {
            pos = position;
            apparance = view;
            foreach(ModelMesh mm  in view.Meshes)
            {
                hitspheres.Add(mm.BoundingSphere);
               // Console.WriteLine(hitspheres.Last().Center + " : " + hitspheres.Last().Radius);
            }
            Matrix[] transforms = new Matrix[apparance.Bones.Count];
            apparance.CopyAbsoluteBoneTransformsTo(transforms);
            name = nameori;

        }

        public void draw()
        {
           
            apparance.Draw(Matrix.CreateTranslation(pos), Tools.Quick.game.camera.getview(), Tools.Quick.game.camera.GetProjection());
            
        }
    }

   
}
