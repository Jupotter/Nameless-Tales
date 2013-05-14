using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGProject
{
    class Trees : DrawableGameComponent
    {
        Ground ground;
        BiomeType[,] biomeMap;
        GraphicsDevice device;

        Vector3 cameraPosition;
        Matrix viewMatrix, projectionMatrix;

        Texture2D treeTexture;
        Effect effect;
        VertexBuffer vertexBuffer;
        List<Vector3> treeList;

        public Trees(Game game)
            : base(game)
        {
            treeList = new List<Vector3>();

            this.DrawOrder = 2;
        }

        protected override void LoadContent()
        {
            device = Game.GraphicsDevice;

            treeTexture = Game.Content.Load<Texture2D>("Textures/tree");
            effect = Game.Content.Load<Effect>("Effects/Billboard");

            base.LoadContent();
        }

        public void SetGround(Ground ground)
        {
            this.ground = ground;
            biomeMap = ground.biomeMap;
        }

        public void CreateTrees()
        {
            GenerateTreeList();
        }

        private void GenerateTreeList()
        {
            for (int i = 0; i < Map.MAPSIZE; i++)
            {
                for (int j = 0; j < Map.MAPSIZE; j++)
                {
                    switch (biomeMap[i,j])
                    {

                        case BiomeType.TemperateForest:
                            treeList.Add(new Vector3(j, ground.source[i, j]/2, i));
                            break;
                        case BiomeType.TropicalRainforest:
                            treeList.Add(new Vector3(j, ground.source[i, j]/2, i));
                            break;
                        case BiomeType.TemperateRainforest:
                            treeList.Add(new Vector3(j, ground.source[i, j]/2, i));
                            break;
                        default:
                            break;
                    }
                }
            }
            CreateVertices();
        }

        private void CreateVertices()
        {
            VertexPositionTexture[] billboardVertices = new VertexPositionTexture[treeList.Count * 6];
            int i = 0;
            foreach (Vector3 currentV3 in treeList)
            {
                billboardVertices[i++] = new VertexPositionTexture(currentV3, new Vector2(0, 0));
                billboardVertices[i++] = new VertexPositionTexture(currentV3, new Vector2(1, 0));
                billboardVertices[i++] = new VertexPositionTexture(currentV3, new Vector2(1, 1));

                billboardVertices[i++] = new VertexPositionTexture(currentV3, new Vector2(0, 0));
                billboardVertices[i++] = new VertexPositionTexture(currentV3, new Vector2(1, 1));
                billboardVertices[i++] = new VertexPositionTexture(currentV3, new Vector2(0, 1));
            }

            vertexBuffer = new VertexBuffer(device, VertexPositionTexture.VertexDeclaration, billboardVertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(billboardVertices);
        }

        public void SetCamera(Matrix view, Matrix projection, Vector3 position)
        {
            viewMatrix = view;
            projectionMatrix = projection;
            cameraPosition = position;
        }

        public override void Draw(GameTime gameTime)
        {
            

            effect.CurrentTechnique = effect.Techniques["CylBillboard"];
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);
            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
            effect.Parameters["xCamPos"].SetValue(cameraPosition);
            effect.Parameters["xAllowedRotDir"].SetValue(new Vector3(0, 1, 0));
            effect.Parameters["xBillboardTexture"].SetValue(treeTexture);

            BlendState bl = BlendState.AlphaBlend;

            //bl.AlphaSourceBlend = Blend.SourceAlpha;
            //bl.AlphaDestinationBlend = Blend.InverseSourceAlpha;
            device.BlendState = bl;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(vertexBuffer);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexBuffer.VertexCount/3);
            }

            effect.CurrentTechnique = effect.Techniques["CylBillboardAlpha"];
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(vertexBuffer);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexBuffer.VertexCount / 3);
            }

            device.BlendState = BlendState.Opaque;
        }
    }
}
