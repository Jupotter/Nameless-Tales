using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace RPGProject
{
  public  class ObjectOnMap : MyDrawableGameComponent
    {

        #region mapObject

      public  List<GameObject> onMapObject = new List<GameObject>();

        #endregion

        public ObjectOnMap()
            : base(Tools.Quick.game)
        {
            Visible = true;
            DrawOrder = 1000;
        }

        public bool addObjectOnMap(string name, Vector3 position)
        {
            try
            {
                GameObject go = new GameObject(Tools.Quick.gameobject[name]);
                go.setPosition(position);
                onMapObject.Add(go);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameObject go in onMapObject)
            {
                go.Draw();
            }
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            if (!addObjectOnMap("sapin", Vector3.Zero)) { throw new Exception("Object load error 1"); };
            base.LoadContent();
        }
    }
}
