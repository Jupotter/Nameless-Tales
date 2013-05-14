using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGProject.Dungeons
{
    class DungeonRoomDrawable : IDrawable
    {
        Model model;

        protected Camera camera;
        protected Matrix world;

        public DungeonRoomDrawable()
        {
        }

        public void SetCamera(Camera camera)
        {
            this.camera = camera;
        }

        public void SetPosition(Vector3 position)
        {
            world = Matrix.CreateTranslation(position);
        }

        public virtual void Draw(GameTime gameTime)
        {
            model.Draw(world, camera.getview(), camera.GetProjection());
        }

        public int DrawOrder
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;

        public bool Visible
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> VisibleChanged;
    }
}
