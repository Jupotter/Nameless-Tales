using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject
{
    public enum Stat { Force = 0, Endurance = 1, Dexterite = 2, Intelligence = 3, Sagesse = 4, Chance = 5 }

    public class Characteristic
    {

        public int value;
        public int realvalue;
        public int bonus;
        public Stat type;

        public Characteristic(Stat type, int value, int bonus)
        {

            this.type = type;
            this.value = value;
            this.bonus = bonus;
            realvalue = this.value + bonus;
        
        }

        public void recalculatestat(int bonus)
        {
            realvalue = value + bonus;
        }


    }
}
