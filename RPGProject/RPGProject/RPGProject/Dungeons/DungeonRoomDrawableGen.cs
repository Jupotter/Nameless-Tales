using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGProject.Dungeons
{
    class DungeonRoomDrawableGen : DungeonRoomDrawable
    {
        BasicEffect effect;
        GraphicsDevice device;
        VertexBuffer shape;
        IndexBuffer indices;

        public DungeonRoomDrawableGen(Vector3 size, List<Tuple<int, int>> entries)
        {
            device = Tools.Quick.device;
            effect = new BasicEffect(device);
            generate(size, entries);
        }

        void generate(Vector3 size, List<Tuple<int, int>> entries)
        {
            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            //int[] indicesA = { 1, 2, 4, 1, 3, 4 };
            int[] indicesA = { 0, 3, 1, 0, 2, 3 };
            vertices.Add(new VertexPositionColor(Vector3.Zero, Color.Black));
            vertices.Add(new VertexPositionColor(new Vector3(size.X, 0,0), Color.Red));
            vertices.Add(new VertexPositionColor(new Vector3(0, 0, size.Z), Color.Blue));
            vertices.Add(new VertexPositionColor(new Vector3(size.X, 0, size.Z), Color.Green));

            shape = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, 4, BufferUsage.WriteOnly);
            shape.SetData(vertices.ToArray());
            indices = new IndexBuffer(device, IndexElementSize.ThirtyTwoBits, 6, BufferUsage.WriteOnly);
            indices.SetData(indicesA);
        }

        public override void Draw(GameTime gameTime)
        {
            device.RasterizerState = new RasterizerState() { CullMode = CullMode.CullClockwiseFace, FillMode = FillMode.Solid};
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            effect.View = camera.getview();
            effect.Projection = camera.GetProjection();
            effect.World = world;
            effect.VertexColorEnabled = true;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(shape);
                device.Indices = indices;
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, indices.IndexCount, 0, indices.IndexCount / 3);
            }
        }

    }
}
