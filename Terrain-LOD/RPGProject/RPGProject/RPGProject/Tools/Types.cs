using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;

namespace RPGProject.Tools
{
    enum TypeFont{Texte,Titre};
    public enum Armorplace { Head = 1, Gear = 2, Belt = 3 , RArm = 4, LArm = 5 , RLeg = 6, LLeg = 7 , RHand = 8 , LHand = 9 , RFoot = 10 ,LFoot = 11 };
    public delegate void onClickFunction();

    struct Couple
    {
        public object val1;
       public object val2;

    }
}
