using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RPGProject
{
   public class Object
    {
        public int code;
        public Model apparance;
        public Vector3 position;
        public string nomaffichage;
        public int value;
        public float weight;
        public bool takeable;
        public Texture2D appInventory;
        //code|name|value|weight|takeable|modelname
        public Object(int code, string nom, int value, float weight, bool takeable, Model apparance,Texture2D appInventory)
        {
            this.code = code;
            this.nomaffichage = nom;
            this.value = value;
            this.weight = weight;
            this.takeable = takeable;
            this.apparance = apparance;
            this.appInventory = appInventory;

        }
   
  
   }
}
