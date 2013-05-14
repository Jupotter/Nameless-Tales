using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RPGProject
{
    class Batiment : IDrawable
    {

        public string name;
        public string Name
        {
            get { return name; }
            set { name = value;}
        }
        public bool passable;
        public bool Passable
        {
            get { return passable; }
            set { passable = value; }
        }
        public Vector3 position;
        public Vector3 Position
        {
            get{return position;}
            set{ position = value;}
        }
        public Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
             set { texture = value;}
        }
        public Model model;
        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        public void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
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
