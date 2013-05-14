using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGProject
{
    class SkyBox : DrawableGameComponent
    {
        const int scale = 6;

        TextureCube cloudMap;
        Effect effect;
        GraphicsDevice device;

        Vector3 cameraPosition;
        Matrix viewMatrix, projectionMatrix;
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        public SkyBox(Game game)
            : base(game)
        {
            this.Visible = false;
        }

        protected override void LoadContent()
        {
            device = Game.GraphicsDevice;
            effect = Game.Content.Load<Effect>("Effects/MyEffect");

            cloudMap = Game.Content.Load<TextureCube>("Textures/Texture1");

            VertexPositionColor[] vertices = new VertexPositionColor[8];
            vertices[0] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.White);
            vertices[1] = new VertexPositionColor(new Vector3(1, -1, -1), Color.White);
            vertices[2] = new VertexPositionColor(new Vector3(1, 1, -1), Color.White);
            vertices[3] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.White);
            vertices[4] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.White);
            vertices[5] = new VertexPositionColor(new Vector3(1, -1, 1), Color.White);
            vertices[6] = new VertexPositionColor(new Vector3(1, 1, 1), Color.White);
            vertices[7] = new VertexPositionColor(new Vector3(-1, 1, 1), Color.White);

            int[] indices = new int[]{0, 1, 2, 0, 2, 3, 3, 2, 6, 6, 3, 7, 7, 5, 6, 7, 5, 4, 0, 4, 3, 3, 4, 7, 1, 5, 2, 2, 5, 6, 0, 5, 1, 0, 5, 4};
            vertexBuffer = new VertexBuffer(Game.GraphicsDevice, VertexPositionColor.VertexDeclaration, 8, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);
            indexBuffer = new IndexBuffer(Game.GraphicsDevice, IndexElementSize.ThirtyTwoBits, 36, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        public void SetCamera(Matrix view, Matrix projection, Vector3 position)
        {
            viewMatrix = view;
            projectionMatrix = projection;
            cameraPosition = position;
        }

        public void DrawClippedSky(GraphicsDevice device, Effect effect, Camera camera)
        {
            DepthStencilState s = new DepthStencilState();
            s.DepthBufferWriteEnable = false;
            DepthStencilState bak = device.DepthStencilState;
            device.DepthStencilState = s;
            device.RasterizerState = new RasterizerState() { CullMode = CullMode.None, FillMode = FillMode.Solid };

            Matrix wMatrix = Matrix.CreateScale(scale) * Matrix.CreateTranslation(camera.position);

            effect.CurrentTechnique = effect.Techniques["SkyBox"];
            effect.Parameters["xView"].SetValue(camera.getview());
            effect.Parameters["xProjection"].SetValue(camera.GetProjection());
            effect.Parameters["xWorld"].SetValue(wMatrix);
            effect.Parameters["xCamPos"].SetValue(camera.position);
            effect.Parameters["xSkyBoxTexture"].SetValue(cloudMap);
            effect.Parameters["xEnableLighting"].SetValue(false);
            //currentEffect.Parameters["xClipping"].SetValue(true);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(vertexBuffer);
                device.Indices = indexBuffer;
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indexBuffer.IndexCount, 0, indexBuffer.IndexCount / 3);
            }

            device.DepthStencilState = bak;
        }

        public override void Draw(GameTime gameTime)
        {
            DepthStencilState s = new DepthStencilState();
            s.DepthBufferWriteEnable = false;
            DepthStencilState bak = device.DepthStencilState;
            device.DepthStencilState = s;
            device.RasterizerState = new RasterizerState() { CullMode = CullMode.None, FillMode = FillMode.Solid };

            Matrix wMatrix = Matrix.CreateScale(scale) * Matrix.CreateTranslation(cameraPosition);
            effect.CurrentTechnique = effect.Techniques["SkyBox"];
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xWorld"].SetValue(wMatrix);
            effect.Parameters["xCamPos"].SetValue(cameraPosition);
            effect.Parameters["xSkyBoxTexture"].SetValue(cloudMap);
            effect.Parameters["xEnableLighting"].SetValue(false);
            effect.Parameters["xClipping"].SetValue(false);
            effect.Parameters["xTexture"].SetValue(Tools.Quick.biomeTextures[1]);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(vertexBuffer);
                device.Indices = indexBuffer;
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indexBuffer.IndexCount, 0, indexBuffer.IndexCount / 3);
            }

            device.DepthStencilState = bak;
            base.Draw(gameTime);
        }
    }
}
