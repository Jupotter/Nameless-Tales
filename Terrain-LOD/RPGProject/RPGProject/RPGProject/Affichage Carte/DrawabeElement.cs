using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace RPGProject
{
    class DrawabeElement
    {

        public VertexPositionNormal4Texture[] vpc;
        public BasicEffect effect;
        public VertexBuffer vb;

        public DrawabeElement(VertexPositionNormal4Texture[] vpc, BiomeType gt)
        {
            this.vpc = vpc;
            effect = new BasicEffect(Tools.Quick.device);
            setEffect(gt);

            vb = new VertexBuffer(Tools.Quick.device, VertexPositionNormal4Texture.VertexDeclaration, vpc.Count(), BufferUsage.WriteOnly);
            vb.SetData(vpc);
            //effect.Parameters["xTexture"].SetValue(effect.Texture);
        }

        private void setEffect(BiomeType gt)
        {
            Texture2D texture = Tools.Quick.groundTexture[gt];

            effect.World = Matrix.Identity;
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                Tools.Quick.graphics.GraphicsDevice.Viewport.Width / (float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height, 1.0f, 10000.0f);

            effect.Texture = texture;
            effect.TextureEnabled = true;
            //effect.World = Matrix.CreateTranslation(Vector3.Zero); //modelPosition

            effect.LightingEnabled = true;
            //effect.Parameters["xEnableLighting"].SetValue(true);
            Vector3 lightDirection = new Vector3(0.5f, -1f, 0);
            lightDirection.Normalize();
            //effect.Parameters["xLightDirection"].SetValue(lightDirection);

            effect.DirectionalLight0.Direction = lightDirection;
            effect.DirectionalLight0.Enabled = true;

            effect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
            //effect.DiffuseColor = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}
