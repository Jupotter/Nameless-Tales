using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGProject.MapGen
{
    static class MapConverter
    {
        static Effect effect;

        static GraphicsDevice device;
        static RenderTarget2D rd;
        static MapConverter()
        {
        }

        static public void Init()
        {
            effect = Tools.Quick.content.Load<Effect>("Effects/MapConvert");
            device = Tools.Quick.device;
            //device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters());
            rd = new RenderTarget2D(device, Map.MAPSIZE, Map.MAPSIZE);

        }

        public static Map Convert(Voronoi v)
        {
            rd = new RenderTarget2D(device, Map.MAPSIZE, Map.MAPSIZE);

            List<VertexPositionColor> lv = new List<VertexPositionColor>();

            foreach (Voronoi.Edge edge in v.Edges.Values)
            {
                lv.Add(new VertexPositionColor(new Vector3(edge.d0.position, (float)((double)edge.d0.Properties["Height"])), Color.Black));
                lv.Add(new VertexPositionColor(new Vector3(edge.v0.position, (float)((double)edge.v0.Properties["Height"])), Color.Black));
                lv.Add(new VertexPositionColor(new Vector3(edge.v1.position, (float)((double)edge.v1.Properties["Height"])), Color.Black));

                lv.Add(new VertexPositionColor(new Vector3(edge.d1.position, (float)((double)edge.d1.Properties["Height"])), Color.Black));
                lv.Add(new VertexPositionColor(new Vector3(edge.v0.position, (float)((double)edge.v0.Properties["Height"])), Color.Black));
                lv.Add(new VertexPositionColor(new Vector3(edge.v1.position, (float)((double)edge.v1.Properties["Height"])), Color.Black));
            }
            device.SetRenderTarget(rd);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, lv.ToArray(), 0, lv.Count / 3);
                //device.DrawPrimitives(PrimitiveType.TriangleList, 0, lv.Count / 3);
            }
            device.SetRenderTarget(null);
            ((Texture2D)rd).SaveAsJpeg(new FileStream("Map.jpg", FileMode.OpenOrCreate), 1025, 1025);

            return new Map();
        }

    }
}
