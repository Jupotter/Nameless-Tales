using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RPGProject
{
    class DrawThings
    {

        Matrix viewMatrix;
        Matrix projectionMatrix;

        RasterizerState WIREFRAME_RASTERIZER_STATE = new RasterizerState() { CullMode = CullMode.CullClockwiseFace, FillMode = FillMode.Solid };

        public void init()
        {
            viewMatrix = Matrix.Identity;
            //  projectionMatrix = Matrix.CreateOrthographic(Tools.Quick.graphics.GraphicsDevice.Viewport.Width,(float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height, 1f, 100000.0f);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                    Tools.Quick.graphics.GraphicsDevice.Viewport.Width / (float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height, 1.0f, 10000.0f);

        }

        public void reinitcounttex()
        {

        }

        public void drawSquarre(VertexPositionNormalTexture[] vertexData, int[] indexData, Camera came, BasicEffect effect, GraphicsDevice graphicsDevice)
        {
            Texture2D texture = Tools.Quick.groundTexture[BiomeType.SubtropicalDesert];
            effect.Projection = projectionMatrix;
            effect.Texture = texture;
            effect.TextureEnabled = true; ;


            graphicsDevice.RasterizerState = WIREFRAME_RASTERIZER_STATE;    // draw in wireframe
            graphicsDevice.BlendState = BlendState.Opaque;                  // no alpha this time


            //   effect.DiffuseColor = Color.Red.ToVector3();
            effect.CurrentTechnique.Passes[0].Apply();

            effect.View = came.getview();
            effect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertexData, 0, 4, indexData, 0, 2);

            /*  // Draw wireframe box
              graphicsDevice.RasterizerState = WIREFRAME_RASTERIZER_STATE;    // draw in wireframe
              graphicsDevice.BlendState = BlendState.Opaque;                  // no alpha this time

              effect.TextureEnabled = false;
             // effect.DiffuseColor = Color.Black.ToVector3();
              effect.CurrentTechnique.Passes[0].Apply();

              graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertexData, 0, 4, indexData, 0, 2);
                 */


        }

        public void drawMesh(Model myModel, Vector3 modelPosition, float modelRotation, Camera camera)
        {

            float aspectRatio = (float)Tools.Quick.graphics.GraphicsDevice.Viewport.Width /
               (float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height;
            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(transforms);
            Tools.Quick.device.RasterizerState = RasterizerState.CullNone;  // vertex order doesn't matter
            Tools.Quick.device.BlendState = BlendState.Opaque;    // use alpha blending
            Tools.Quick.device.DepthStencilState = DepthStencilState.Default;  // don't bother with the depth/stencil buffer

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in myModel.Meshes)
            {
                // This is where the mesh orientation is set, as well as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(modelRotation) * Matrix.CreateTranslation(modelPosition); //modelPosition
                    effect.View = camera.getview();
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                
                        aspectRatio, 1.0f, 1000000.0f);
                  
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }

        }

        public void drawBigbuffer(DrawabeElement element, int[] index, Camera came, GraphicsDevice graphicsDevice, bool trans)
        {
            WIREFRAME_RASTERIZER_STATE = new RasterizerState() { CullMode = CullMode.CullClockwiseFace, FillMode = FillMode.Solid };


            graphicsDevice.RasterizerState = WIREFRAME_RASTERIZER_STATE;

            // draw in wireframe

            if (trans)
            {
                graphicsDevice.BlendState = BlendState.AlphaBlend;

            }
            else
            {
                graphicsDevice.BlendState = BlendState.Opaque;
            }

            graphicsDevice.DepthStencilState = DepthStencilState.Default;

            //   effect.DiffuseColor = Color.Red.ToVector3();
            element.effect.View = came.getview();
            element.effect.CurrentTechnique.Passes[0].Apply();
            // vb.GetData<VertexPositionNormalTexture>(vertexData);

            graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, element.vpc, 0, element.vpc.Count() / 3);
            // graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertexData, 0, vertexData.Count(), index, 0, index.Count() / 3);
        }

    }
}
