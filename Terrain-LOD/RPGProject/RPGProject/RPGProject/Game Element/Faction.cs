using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
namespace RPGProject
{
    class Faction
    {
        List<Personnage> peopleList = new List<Personnage>();
        List<Faction> enemies = new List<Faction>();
        List<Faction> friends = new List<Faction>();
        List<Faction> neutral = new List<Faction>();
        Texture2D emblem;
        string name;

    }
}
