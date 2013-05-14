using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using drawing = System.Drawing;
using sColor = System.Drawing.Color;

namespace RPGProject.Affichage_Carte
{
    class GroundCell : IDrawable
    {
        const int LOD_NUM = 4;

        public const int CELL_SIZE = 32;

        VertexBuffer vertices;
        SortedDictionary<int, IndexBuffer> LODIndices;
        Texture2D biomeTexture;
        Effect effect;
        int xPos, yPos;

        public GroundCell(int[,] tiles, BiomeType[,] biomeMap, int xPos, int yPos)
        {
            this.xPos = xPos; this.yPos = yPos;
            LODIndices = new SortedDictionary<int, IndexBuffer>();
            makeBiomeMap(Tools.Quick.device, biomeMap);
            makeBuffers(tiles, biomeMap);
            

            effect = Tools.Quick.content.Load<Effect>("Effects/MyEffect");

            effect.Parameters["xWorldScale"].SetValue(new Vector2(Map.MAPSIZE, Map.MAPSIZE));
            effect.Parameters["xTexturesMap"].SetValue(biomeTexture);

            effect.Parameters["xEnableLighting"].SetValue(true);
            Vector3 lightDirection = new Vector3(0.5f, -1f, 0);
            lightDirection.Normalize();
            effect.Parameters["xLightDirection"].SetValue(lightDirection);
            effect.Parameters["xClipping"].SetValue(false);
            effect.Parameters["xTextureAtlas"].SetValue(Tools.Quick.biome3D);
        }

        public void makeBuffers(int[,] tile, BiomeType[,] biomeMap)
        {
            VertexPositionNormal4Texture[] buffer = new VertexPositionNormal4Texture[(CELL_SIZE + 1) * (CELL_SIZE + 1)];
            int[] iBuffer1 = new int[CELL_SIZE * CELL_SIZE * 6];

            for (int n = 0; n <= CELL_SIZE; n++)
            {
                buffer[n] = new VertexPositionNormal4Texture(
                    new Vector3(n, tile[n,0]/2, -1), new Vector3(0, 1, 0), new Vector4(1, 0, 0, 0), new Vector4((int)BiomeType.Water));
            }

            for (int y = 0; y < CELL_SIZE; y++)
                for (int x = 0; x < CELL_SIZE; x++)
                {
                    int n = CELL_SIZE * y + x;
                    int h = tile[x, y]/2;
                    if (x == 0)
                        buffer[n + CELL_SIZE + y + 1] = new VertexPositionNormal4Texture(
                            new Vector3(-1, tile[0,y]/2, y), new Vector3(0, 1, 0), new Vector4(1, 0, 0, 0), new Vector4((int)BiomeType.Water));
                    buffer[n + CELL_SIZE + y + 2] = new VertexPositionNormal4Texture(new Vector3(x, h, y), new Vector3(1, 0, 0), new Vector4(1, 0, 0, 0), new Vector4((int)biomeMap[x, y]));

                    buffer[n + y].TextureType.Y = (int)biomeMap[x, y];
                    buffer[n + y].TextureWeight.Y = 1;
                    buffer[n + y].TextureWeight.Normalize();

                    buffer[n + CELL_SIZE + y + 1].TextureType.Z = (int)biomeMap[x, y];
                    buffer[n + CELL_SIZE + y + 1].TextureWeight.Z = 1;
                    buffer[n + CELL_SIZE + y + 1].TextureWeight.Normalize();

                    buffer[n + CELL_SIZE + y + 2].TextureType.W = (int)biomeMap[x, y];
                    buffer[n + CELL_SIZE + y + 2].TextureWeight.W = 1;
                    buffer[n + CELL_SIZE + y + 2].TextureWeight.Normalize();

                    iBuffer1[n * 6] = n + y;
                    iBuffer1[n * 6 + 1] = n + CELL_SIZE + y + 1;
                    iBuffer1[n * 6 + 2] = n + y + 1;
                    iBuffer1[n * 6 + 3] = n + y + 1;
                    iBuffer1[n * 6 + 4] = n + CELL_SIZE + y + 1;
                    iBuffer1[n * 6 + 5] = n + CELL_SIZE + y + 2;
                }

            vertices = new VertexBuffer(Tools.Quick.device, VertexPositionNormal4Texture.VertexDeclaration, (CELL_SIZE + 1) * (CELL_SIZE + 1), BufferUsage.WriteOnly);
            vertices.SetData(buffer);
            IndexBuffer indices = new IndexBuffer(Tools.Quick.device, IndexElementSize.ThirtyTwoBits, (CELL_SIZE) * (CELL_SIZE) * 6, BufferUsage.WriteOnly);
            indices.SetData(iBuffer1);

            LODIndices.Add(1, indices);
        }

        void makeBiomeMap(GraphicsDevice device, BiomeType[,] biomeMap)
        {
            drawing.Bitmap img = new drawing.Bitmap(CELL_SIZE, CELL_SIZE);
            for (int i = 0; i < CELL_SIZE; i++)
                for (int j = 0; j < CELL_SIZE; j++)
                {
                    BiomeType h = biomeMap[i, j];
                    sColor c = sColor.FromArgb((int)h * 255 / 11, 0, 0);
                    img.SetPixel(i, j, c);
                }
            biomeTexture = new Texture2D(device, CELL_SIZE, CELL_SIZE);

            drawing.Imaging.BitmapData data = img.LockBits(new drawing.Rectangle(0, 0, img.Width, img.Height), drawing.Imaging.ImageLockMode.ReadOnly, img.PixelFormat);

            int bufferSize = data.Height * data.Stride;
            byte[] bytes = new byte[bufferSize];
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
            biomeTexture.SetData(bytes);
            img.UnlockBits(data);
        }

        public void DrawGround(Vector3 lookat, Vector3 cameraPosition, DrawThings display, Camera camera)
        {
            GraphicsDevice graphicsDevice = Tools.Quick.device;
            graphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.CullClockwiseFace, FillMode = FillMode.Solid };
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;

            effect.CurrentTechnique = effect.Techniques["MultiTextured"];
            //effect.CurrentTechnique = effect.Techniques["Textured"];

            effect.Parameters["xView"].SetValue(camera.getview());
            effect.Parameters["xProjection"].SetValue(camera.GetProjection());
            effect.Parameters["xWorld"].SetValue(Matrix.CreateTranslation(new Vector3(xPos, 0, yPos)));
            //effect.Parameters["xEnableLighting"].SetValue(false);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.SetVertexBuffer(vertices);
                graphicsDevice.Indices = LODIndices[1];
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, LODIndices[1].IndexCount, 0, LODIndices[1].IndexCount / 3);
            }
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
