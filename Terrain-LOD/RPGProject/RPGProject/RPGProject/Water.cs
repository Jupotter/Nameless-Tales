using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGProject
{
    class Water : DrawableGameComponent
    {
        const float waterHeight = Map.WATER_LEVEL / 2+0.01f;
        const int WATERSIZE = 512;

        RenderTarget2D refractionRenderTarget;
        Texture2D refractionMap;

        RenderTarget2D reflectionRenderTarget;
        Texture2D reflectionMap;

        GraphicsDevice device;

        Ground ground;
        SpriteBatch sprite;

        Vector3 cameraPosition;
        Matrix viewMatrix, projectionMatrix;
        Matrix reflectionViewMatrix;
        Camera reflectionCamera = new Camera();

        Effect effect;

        VertexBuffer waterVertexBuffer;
        IndexBuffer waterIndexBuffer;
        VertexDeclaration waterVertexDeclaration;

        Texture2D waterBumpMap;

        Vector3 windDirection = new Vector3(1, 0, 1);

        public Water(Game game)
            : base(game)
        {
            this.DrawOrder = 0;
            //this.Visible = false;
        }

        protected override void LoadContent()
        {
            device = Game.GraphicsDevice;
            PresentationParameters pp = device.PresentationParameters;
            refractionRenderTarget = new RenderTarget2D(device, pp.BackBufferWidth, pp.BackBufferHeight, true, pp.BackBufferFormat, pp.DepthStencilFormat);
            reflectionRenderTarget = new RenderTarget2D(device, pp.BackBufferWidth, pp.BackBufferHeight, true, pp.BackBufferFormat, pp.DepthStencilFormat);

            effect = Game.Content.Load<Effect>("Effects/MyEffect");
            sprite = new SpriteBatch(device);

            waterBumpMap = Game.Content.Load<Texture2D>("waterbump");

            SetUpWaterVertices();
            waterVertexDeclaration = new VertexDeclaration(VertexPositionTexture.VertexDeclaration.GetVertexElements());

            base.LoadContent();
        }

        private void SetUpWaterVertices()
        {
            int terrainWidth, terrainLength;
            terrainWidth = terrainLength = WATERSIZE;

            /*VertexPositionTexture[] waterVertices = new VertexPositionTexture[6];

            waterVertices[0] = new VertexPositionTexture(new Vector3(0, waterHeight, 0), new Vector2(0, 1));
            waterVertices[2] = new VertexPositionTexture(new Vector3(terrainWidth, waterHeight, terrainLength), new Vector2(1, 0));
            waterVertices[1] = new VertexPositionTexture(new Vector3(0, waterHeight, terrainLength), new Vector2(0, 0));

            waterVertices[3] = new VertexPositionTexture(new Vector3(0, waterHeight, 0), new Vector2(0, 1));
            waterVertices[5] = new VertexPositionTexture(new Vector3(terrainWidth, waterHeight, 0), new Vector2(1, 1));
            waterVertices[4] = new VertexPositionTexture(new Vector3(terrainWidth, waterHeight, terrainLength), new Vector2(1, 0));

            waterVertexBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, 6, BufferUsage.WriteOnly);
            waterVertexBuffer.SetData(waterVertices);*/

            VertexPositionTexture[] buffer = new VertexPositionTexture[(WATERSIZE+1) * (WATERSIZE+1)];
            int[] iBuffer = new int[terrainWidth * terrainLength * 6];

            for (int n = 0; n <= WATERSIZE; n++)
            {
                buffer[n] = new VertexPositionTexture(
                    new Vector3(n, waterHeight, 0), new Vector2(0, n/(float)terrainWidth));
            }

            for (int n = 0; n < WATERSIZE * WATERSIZE; n++)
            {

                int y = n / (WATERSIZE);
                int x = n % (WATERSIZE);

                if (x == 0)
                    buffer[n + (WATERSIZE) + y + 1] = new VertexPositionTexture(
                        new Vector3(0, waterHeight, y + 1), new Vector2(0, (y + 1) / (float)WATERSIZE));

                buffer[n + WATERSIZE + y + 2] = new VertexPositionTexture(new Vector3(x + 1, waterHeight, y + 1), new Vector2((x + 1) / (float)WATERSIZE, (y + 1) / (float)WATERSIZE));

                iBuffer[n * 6] = n + y;
                iBuffer[n * 6 + 1] = n + WATERSIZE + y + 1;
                iBuffer[n * 6 + 2] = n + y + 1;
                iBuffer[n * 6 + 3] = n + y + 1;
                iBuffer[n * 6 + 4] = n + WATERSIZE + y + 1;
                iBuffer[n * 6 + 5] = n + WATERSIZE + y + 2;
            }

            waterVertexBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, (WATERSIZE + 1) * (WATERSIZE + 1), BufferUsage.WriteOnly);
            waterVertexBuffer.SetData(buffer);

            waterIndexBuffer = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, (WATERSIZE) * (WATERSIZE) * 6, BufferUsage.WriteOnly);
            waterIndexBuffer.SetData(iBuffer);
        }

        public void SetCamera(Matrix view, Matrix projection)
        {
            viewMatrix = view;
            projectionMatrix = projection;
        }

        public void SetGround(Ground ground)
        {
            this.ground = ground;
        }

        private Plane CreatePlane(float height, Vector3 planeNormalDirection, Matrix currentViewMatrix, Matrix currentProjectionMatrix, bool clipSide)
        {
            planeNormalDirection.Normalize();
            Vector4 planeCoeffs = new Vector4(planeNormalDirection, height);
            if (clipSide)
                planeCoeffs *= -1;

            Matrix worldViewProjection = currentViewMatrix * currentProjectionMatrix;
            Matrix inverseWorldViewProjection = Matrix.Invert(worldViewProjection);
            inverseWorldViewProjection = Matrix.Transpose(inverseWorldViewProjection);
            //planeCoeffs = Vector4.Transform(planeCoeffs, inverseWorldViewProjection);
            Plane finalPlane = new Plane(planeCoeffs);

            return finalPlane;
        }

        public void DrawRefractionMap(Camera c)
        {
            viewMatrix = c.getview();
            projectionMatrix = c.GetProjection();
            cameraPosition = c.position;

            Plane refractionPlane = CreatePlane(waterHeight + 50f, new Vector3(0, -1, 0), viewMatrix, projectionMatrix, false);
            effect.CurrentTechnique = effect.Techniques["Textured"];
            effect.Parameters["xClipPlane0"].SetValue(new Vector4(refractionPlane.Normal, refractionPlane.D));
            effect.Parameters["xClipping"].SetValue(true);

            device.SetRenderTarget(refractionRenderTarget);

            device.DepthStencilState = new DepthStencilState();
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 5.0f, 0);

            ground.DrawClippedGround(device, effect, c);


            effect.Parameters["xClipping"].SetValue(false);

            device.SetRenderTarget(null);

            refractionMap = (Texture2D)refractionRenderTarget;
        }

        public void DrawReflectionMap(Camera c, SkyBox d)
        {
            reflectionCamera.position = c.position;
            reflectionCamera.position.Y = -c.position.Y + waterHeight * 2;
            reflectionCamera.lookat = c.lookat;
            reflectionCamera.lookat.Y = -c.lookat.Y + waterHeight * 2;

            reflectionViewMatrix = reflectionCamera.getview();

            Plane reflectionPlane = CreatePlane(waterHeight, new Vector3(0, -1, 0), viewMatrix, reflectionViewMatrix, true);
            effect.CurrentTechnique = effect.Techniques["Textured"];
            effect.Parameters["xClipPlane0"].SetValue(new Vector4(reflectionPlane.Normal, reflectionPlane.D));
            effect.Parameters["xClipping"].SetValue(true);

            device.SetRenderTarget(reflectionRenderTarget);

            device.DepthStencilState = new DepthStencilState();
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 5.0f, 0);

            d.DrawClippedSky(device, effect, reflectionCamera);
            ground.DrawClippedGround(device, effect, reflectionCamera);

            effect.Parameters["xClipping"].SetValue(false);

            device.SetRenderTarget(null);

            reflectionMap = (Texture2D)reflectionRenderTarget;
        }

        public override void Draw(GameTime gameTime)
        {
            effect.CurrentTechnique = effect.Techniques["Water"];
            Matrix worldMatrix = Matrix.Identity;

            effect.Parameters["xWorld"].SetValue(worldMatrix);
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xReflectionView"].SetValue(reflectionViewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xEnableLighting"].SetValue(true);
            Vector3 lightDirection = new Vector3(2f, -1f, 0);
            lightDirection.Normalize();
            effect.Parameters["xLightDirection"].SetValue(lightDirection);
            effect.Parameters["xAmbient"].SetValue(0.1f);
            effect.Parameters["xReflectionMap"].SetValue(reflectionMap);
            effect.Parameters["xRefractionMap"].SetValue(refractionMap);
            effect.Parameters["xWaterBumpMap"].SetValue(waterBumpMap);
            effect.Parameters["xWaveLength"].SetValue(0.2f);
            effect.Parameters["xWaveHeight"].SetValue(0.2f);
            effect.Parameters["xCamPos"].SetValue(cameraPosition);
            effect.Parameters["xTime"].SetValue((float)gameTime.TotalGameTime.TotalMinutes);
            effect.Parameters["xWindForce"].SetValue(0.1f);
            effect.Parameters["xWindDirection"].SetValue(windDirection);


            device.RasterizerState = new RasterizerState() { CullMode = CullMode.None, FillMode = FillMode.Solid };
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                device.SetVertexBuffer(waterVertexBuffer);
                device.Indices = waterIndexBuffer;
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, waterIndexBuffer.IndexCount, 0, waterIndexBuffer.IndexCount / 3);
            }

            base.Draw(gameTime);
        }
    }
}
