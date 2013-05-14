using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using drawing = System.Drawing;
using sColor = System.Drawing.Color;
using RPGProject.Affichage_Carte;

namespace RPGProject
{
    enum GroundType { Dirt, Grass, Stone, Water }

    public struct VertexPositionNormal4Texture : IVertexType
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector4 TextureWeight;
        public Vector4 TextureType;

        public static int SizeInBytes = (3 + 3 + 4 + 4) * sizeof(float);

        public VertexPositionNormal4Texture(Vector3 position, Vector3 normal, Vector4 textureWeight, Vector4 textureType)
        {
            Position = position;
            Normal = normal;
            TextureWeight = textureWeight;
            TextureType = textureType;
        }

        public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
            new VertexElement(sizeof(float) * 6, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 0),
            new VertexElement(sizeof(float) * 10, VertexElementFormat.Vector4, VertexElementUsage.TextureCoordinate, 1)
        );

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexDeclaration; }
        }
    }


    class Ground : MyDrawableGameComponent
    {
        const int TOP_LEFT = 0;
        const int TOP_RIGHT = 1;
        const int BOTTOM_RIGHT = 2;
        const int BOTTOM_LEFT = 3;

        const int VAL_DECOUPAGE = 600000; // multiple de 6 pour respecter l'ordre de dessin des primitives

        public List<Tile> tiles = new List<Tile>();

        public List<VertexPositionNormal4Texture[]> lvb = new List<VertexPositionNormal4Texture[]>();
        public List<VertexPositionNormal4Texture[]> lvwater = new List<VertexPositionNormal4Texture[]>();
        VertexBuffer GroundVertices;
        IndexBuffer GroundIndices;

        Vector2 ratiotex = new Vector2(2, 2);
        
        Random rnd = new Random();

        Effect effect;

        public BiomeType[,] biomeMap;
        Texture2D biomeTexture;

        VertexPositionNormal4Texture[] bigbuffer;

        public Map SourceMap;
        public int[,] source;
        public int waterlevel;
        Camera camera;

        public List<DrawabeElement> elements = new List<DrawabeElement>();

        public Ground(Game game, Map map)
            : base(game)
        {
            SourceMap = map;
            waterlevel = Convert.ToInt32(Map.WATER_LEVEL * LoadMap.taille / LoadMap.ajustZ);
            this.Visible = false;
        }

        public override void Initialize()
        {
            Console.WriteLine("{0}: Initialize", ID);

            LoadMap.loadground(SourceMap, this);
            biomeMap = SourceMap.BiomeMap;
            
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Console.WriteLine("{0}: LoadContent", ID);

            base.LoadContent();
        }

        public void SetGroundMap(List<Tile> groundMap, int[,] source)
        {
            tiles = groundMap;
            this.source = source;
        }

        public void SetCamera(Camera camera)
        {
            this.camera = camera;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice graphicsDevice = Tools.Quick.device;
            graphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.CullClockwiseFace, FillMode = FillMode.Solid };
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;

            effect.CurrentTechnique = effect.Techniques["MultiTextured"];
            //effect.CurrentTechnique = effect.Techniques["Textured"];

            effect.Parameters["xView"].SetValue(camera.getview());
            effect.Parameters["xProjection"].SetValue(camera.GetProjection());
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);
            //effect.Parameters["xEnableLighting"].SetValue(false);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.SetVertexBuffer(GroundVertices);
                graphicsDevice.Indices = GroundIndices;
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, GroundIndices.IndexCount, 0, GroundIndices.IndexCount / 3);
            }
        }

        public void DrawClippedGround(GraphicsDevice device, Effect effect, Camera camera)
        {
            effect.CurrentTechnique = effect.Techniques["MultiTextured"];
            effect.Parameters["xTexturesMap"].SetValue(biomeTexture);
            effect.Parameters["xView"].SetValue(camera.getview());
            effect.Parameters["xProjection"].SetValue(camera.GetProjection());
            effect.Parameters["xWorld"].SetValue(Matrix.Identity);

            device.RasterizerState = new RasterizerState() { CullMode = CullMode.CullClockwiseFace, FillMode = FillMode.Solid };
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;

            //effect.Parameters["xTexture"].SetValue(Tools.Quick.groundTexture[gt]);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(GroundVertices);
                device.Indices = GroundIndices;
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, GroundIndices.IndexCount, 0, GroundIndices.IndexCount / 3);
            }
        }

        public int DrawOrder
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        void makeCells()
        {
            int size = GroundCell.CELL_SIZE;
            int mapSize = Map.MAPSIZE - 1;

            for (int i = 0; i < mapSize/size; i++)
                for (int j = 0; j < mapSize/size; j++)
                {
                    
                }
        }

        public void makebigbuffer()
        {
            makeTextureMap(Game.GraphicsDevice);

            effect = Tools.Quick.content.Load<Effect>("Effects/MyEffect");

            effect.Parameters["xWorldScale"].SetValue(new Vector2(Map.MAPSIZE, Map.MAPSIZE));
            effect.Parameters["xTexturesMap"].SetValue(biomeTexture);

            effect.Parameters["xEnableLighting"].SetValue(true);
            Vector3 lightDirection = new Vector3(0.5f, -1f, 0);
            lightDirection.Normalize();
            effect.Parameters["xLightDirection"].SetValue(lightDirection);
            effect.Parameters["xClipping"].SetValue(false);
            effect.Parameters["xTextureAtlas"].SetValue(Tools.Quick.biome3D);


            bigbuffer = new VertexPositionNormal4Texture[tiles.Count * 6];
            //indexvertex = new int[tiles.Count * 6];

            Color color = Color.AliceBlue;
            int i = 0;
            int j = 0;
            int num = 0;

            foreach (Tile t in tiles)
            {
                if (t.v1.Y <= waterlevel)
                {
                    num++;
                }
            }

            //bigbufferWater = new VertexPositionNormal4Texture[num * 6];
            VertexPositionNormal4Texture[] buffer = new VertexPositionNormal4Texture[(Map.MAPSIZE+1) * (Map.MAPSIZE+1)];
            int[] iBuffer = new int[Map.MAPSIZE * Map.MAPSIZE * 6];

            for (int n = 0; n <= Map.MAPSIZE; n++)
            {
                buffer[n] = new VertexPositionNormal4Texture(
                    new Vector3(n, 0, 0), new Vector3(0, 1, 0), new Vector4(1,0,0,0), new Vector4((int)BiomeType.Water));
            }

            for (int n = 0; n < tiles.Count; n++)
            {
                Tile t = tiles[n];
                Vector3 normal = Vector3.Cross(t.v2 - t.v4, t.v1 - t.v4);
                normal.Normalize();
                normal = Vector3.Multiply(normal, -1);

                int y = n / (Map.MAPSIZE);
                int x = n % (Map.MAPSIZE);

                if (x == 0)
                    buffer[n + (Map.MAPSIZE) + y+1] = new VertexPositionNormal4Texture(
                        new Vector3(0, 0, y + 1), new Vector3(0, 1, 0), new Vector4(1,0,0,0), new Vector4((int)BiomeType.Water));

                buffer[n + Map.MAPSIZE + y + 2] = new VertexPositionNormal4Texture(new Vector3(x + 1, t.v1.Y, y + 1), t.n1, new Vector4(1,0,0,0), new Vector4((int)t.gp));

                buffer[n + y].TextureType.Y = (int)t.gp;
                buffer[n + y].TextureWeight.Y = 1;
                buffer[n + y].TextureWeight.Normalize();

                buffer[n + Map.MAPSIZE + y + 1].TextureType.Z = (int)t.gp;
                buffer[n + Map.MAPSIZE + y + 1].TextureWeight.Z = 1;
                buffer[n + Map.MAPSIZE + y + 1].TextureWeight.Normalize();

                buffer[n + Map.MAPSIZE + y + 2].TextureType.W = (int)t.gp;
                buffer[n + Map.MAPSIZE + y + 2].TextureWeight.W = 1;
                buffer[n + Map.MAPSIZE + y + 2].TextureWeight.Normalize();

                iBuffer[n * 6] = n + y;
                iBuffer[n * 6 + 1] = n + Map.MAPSIZE + y + 1;
                iBuffer[n * 6 + 2] = n + y + 1;
                iBuffer[n * 6 + 3] = n + y + 1;
                iBuffer[n * 6 + 4] = n + Map.MAPSIZE + y + 1;
                iBuffer[n * 6 + 5] = n + Map.MAPSIZE + y + 2;
            }

            GroundVertices = new VertexBuffer(Tools.Quick.device, VertexPositionNormal4Texture.VertexDeclaration, (Map.MAPSIZE + 1) * (Map.MAPSIZE + 1), BufferUsage.WriteOnly);
            GroundVertices.SetData(buffer);
            GroundIndices = new IndexBuffer(Tools.Quick.device, IndexElementSize.ThirtyTwoBits, (Map.MAPSIZE) * (Map.MAPSIZE) * 6, BufferUsage.WriteOnly);
            GroundIndices.SetData(iBuffer);
            // verifAppliTex(bigbufferWater);
        }

        public void initDraw()
        {
            //elements.Add(new DrawabeElement(bigbufferWater, GroundType.Water));
            if (bigbuffer.Count() > VAL_DECOUPAGE)
            {
                VertexPositionNormal4Texture[] intermediatebuffer;
                int i = 0;
                while (i < bigbuffer.Count())
                {

                    i += VAL_DECOUPAGE;
                    if (i < bigbuffer.Count())
                    {
                        intermediatebuffer = new VertexPositionNormal4Texture[VAL_DECOUPAGE];
                        Array.Copy(bigbuffer, i - VAL_DECOUPAGE, intermediatebuffer, 0, VAL_DECOUPAGE);
                    }
                    else
                    {
                        intermediatebuffer = new VertexPositionNormal4Texture[bigbuffer.Length - (i - VAL_DECOUPAGE)];
                        Array.Copy(bigbuffer, i - VAL_DECOUPAGE, intermediatebuffer, 0, bigbuffer.Length - (i - VAL_DECOUPAGE));
                    }

                    elements.Add(new DrawabeElement(intermediatebuffer, BiomeType.SubtropicalDesert));

                }
            }
            else
            {
                elements.Add(new DrawabeElement(bigbuffer, BiomeType.SubtropicalDesert));
            }

        }

        void makeTextureMap(GraphicsDevice device)
        {
            drawing.Bitmap img = new drawing.Bitmap(Map.MAPSIZE, Map.MAPSIZE);
            for (int i = 0; i < Map.MAPSIZE; i++)
                for (int j = 0; j < Map.MAPSIZE; j++)
                {
                    BiomeType h = biomeMap[i, j];
                    sColor c = sColor.FromArgb((int)h*255/11,0,0);
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

        public void setVisible(bool visible)
        {
            Visible = visible;
            
        }

    }
}
