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

        public GroundCell(ref int[,] tiles, BiomeType[,] biomeMap)
        {
            makeBiomeMap(Tools.Quick.device, biomeMap);
        }

        public void makeBuffers(ref int[,] tile, BiomeType[,] biomeMap)
        {

            //effect = Tools.Quick.content.Load<Effect>("Effects/MyEffect");

            //effect.Parameters["xWorldScale"].SetValue(new Vector2(Map.MAPSIZE, Map.MAPSIZE));
            //effect.Parameters["xTexturesMap"].SetValue(biomeTexture);

            //effect.Parameters["xEnableLighting"].SetValue(true);
            //Vector3 lightDirection = new Vector3(0.5f, -1f, 0);
            //lightDirection.Normalize();
            //effect.Parameters["xLightDirection"].SetValue(lightDirection);
            //effect.Parameters["xClipping"].SetValue(false);
            //effect.Parameters["xTextureAtlas"].SetValue(Tools.Quick.biome3D);


            //bigbuffer = new VertexPositionNormal4Texture[tiles.Count * 6];
            //indexvertex = new int[tiles.Count * 6];

            Color color = Color.AliceBlue;
            
            //int num = 0;

            //foreach (Tile t in tiles)
            //{
            //    if (t.v1.Y <= waterlevel)
            //    {
            //        num++;
            //    }
            //}

            //bigbufferWater = new VertexPositionNormal4Texture[num * 6];

            VertexPositionNormal4Texture[] buffer = new VertexPositionNormal4Texture[(CELL_SIZE + 1) * (CELL_SIZE + 1)];
            int[] iBuffer1 = new int[CELL_SIZE * CELL_SIZE * 6];

            for (int n = 0; n <= CELL_SIZE; n++)
            {
                buffer[n] = new VertexPositionNormal4Texture(
                    new Vector3(n, 0, 0), new Vector3(0, 1, 0), new Vector4(1, 0, 0, 0), new Vector4((int)BiomeType.Water));
            }

            for (int x = 0; x < CELL_SIZE; x++)
                for (int y = 0; y < CELL_SIZE; y++)
                {
                    int n = CELL_SIZE * y + x;
                    int h = tile[x, y];
                    if (x == 0)
                        buffer[n + CELL_SIZE + y + 1] = new VertexPositionNormal4Texture(
                            new Vector3(0, 0, y + 1), new Vector3(0, 1, 0), new Vector4(1, 0, 0, 0), new Vector4((int)BiomeType.Water));
                    buffer[n + CELL_SIZE + y + 2] = new VertexPositionNormal4Texture(new Vector3(x + 1, h, y + 1), new Vector3(1, 0, 0), new Vector4(1, 0, 0, 0), new Vector4((int)biomeMap[x, y]));

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

            vertices = new VertexBuffer(Tools.Quick.device, VertexPositionNormal4Texture.VertexDeclaration, (Map.MAPSIZE + 1) * (Map.MAPSIZE + 1), BufferUsage.WriteOnly);
            vertices.SetData(buffer);
            IndexBuffer indices = new IndexBuffer(Tools.Quick.device, IndexElementSize.ThirtyTwoBits, (Map.MAPSIZE) * (Map.MAPSIZE) * 6, BufferUsage.WriteOnly);
            indices.SetData(iBuffer1);

            LODIndices.Add(1, indices);

        //    for (int n = 0; n < tiles.Count; n++)
        //    {
        //        Tile t = tiles[n];
        //        Vector3 normal = Vector3.Cross(t.v2 - t.v4, t.v1 - t.v4);
        //        normal.Normalize();
        //        normal = Vector3.Multiply(normal, -1);

        //        int y = n / (Map.MAPSIZE);
        //        int x = n % (Map.MAPSIZE);

        //        if (x == 0)
        //            buffer[n + (Map.MAPSIZE) + y + 1] = new VertexPositionNormal4Texture(
        //                new Vector3(0, 0, y + 1), new Vector3(0, 1, 0), new Vector4(1, 0, 0, 0), new Vector4((int)BiomeType.Water));

        //        buffer[n + Map.MAPSIZE + y + 2] = new VertexPositionNormal4Texture(new Vector3(x + 1, t.v1.Y, y + 1), t.n1, new Vector4(1, 0, 0, 0), new Vector4((int)t.gp));

        //        buffer[n + y].TextureType.Y = (int)t.gp;
        //        buffer[n + y].TextureWeight.Y = 1;
        //        buffer[n + y].TextureWeight.Normalize();

        //        buffer[n + Map.MAPSIZE + y + 1].TextureType.Z = (int)t.gp;
        //        buffer[n + Map.MAPSIZE + y + 1].TextureWeight.Z = 1;
        //        buffer[n + Map.MAPSIZE + y + 1].TextureWeight.Normalize();

        //        buffer[n + Map.MAPSIZE + y + 2].TextureType.W = (int)t.gp;
        //        buffer[n + Map.MAPSIZE + y + 2].TextureWeight.W = 1;
        //        buffer[n + Map.MAPSIZE + y + 2].TextureWeight.Normalize();

        //        iBuffer[n * 6] = n + y;
        //        iBuffer[n * 6 + 1] = n + Map.MAPSIZE + y + 1;
        //        iBuffer[n * 6 + 2] = n + y + 1;
        //        iBuffer[n * 6 + 3] = n + y + 1;
        //        iBuffer[n * 6 + 4] = n + Map.MAPSIZE + y + 1;
        //        iBuffer[n * 6 + 5] = n + Map.MAPSIZE + y + 2;
        //    }

        //    GroundVertices = new VertexBuffer(Tools.Quick.device, VertexPositionNormal4Texture.VertexDeclaration, (Map.MAPSIZE + 1) * (Map.MAPSIZE + 1), BufferUsage.WriteOnly);
        //    GroundVertices.SetData(buffer);
        //    GroundIndices = new IndexBuffer(Tools.Quick.device, IndexElementSize.ThirtyTwoBits, (Map.MAPSIZE) * (Map.MAPSIZE) * 6, BufferUsage.WriteOnly);
        //    GroundIndices.SetData(iBuffer);
        //    // verifAppliTex(bigbufferWater);
        }

        void makeBiomeMap(GraphicsDevice device, BiomeType[,] biomeMap)
        {
            drawing.Bitmap img = new drawing.Bitmap(Map.MAPSIZE, Map.MAPSIZE);
            for (int i = 0; i < Map.MAPSIZE; i++)
                for (int j = 0; j < Map.MAPSIZE; j++)
                {
                    BiomeType h = biomeMap[i, j];
                    sColor c = sColor.FromArgb((int)h * 255 / 11, 0, 0);
                    img.SetPixel(i, j, c);
                }
            biomeTexture = new Texture2D(device, Map.MAPSIZE, Map.MAPSIZE);

            drawing.Imaging.BitmapData data = img.LockBits(new drawing.Rectangle(0, 0, img.Width, img.Height), drawing.Imaging.ImageLockMode.ReadOnly, img.PixelFormat);

            int bufferSize = data.Height * data.Stride;
            byte[] bytes = new byte[bufferSize];
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
            biomeTexture.SetData(bytes);
            img.UnlockBits(data);
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
