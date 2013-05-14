using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGProject
{
    class Tile
    {
        public Vector3 v1;
        public Vector3 v2;
        public Vector3 v3;
        public Vector3 v4;
        public Vector3 normal1, normal2;
        public Vector3 n1, n2, n3, n4;
        public BiomeType gp;

        public Tile(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, BiomeType type)
        {
            // faire un <<c>> pour un affichage correcte
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
            gp = type;

            normal1 = Vector3.Cross(v3 - v2, v1 - v2);
            normal1.Normalize();
            normal2 = Vector3.Cross(v1 - v4, v3 - v4);
            normal2.Normalize();
        }
    }
}
