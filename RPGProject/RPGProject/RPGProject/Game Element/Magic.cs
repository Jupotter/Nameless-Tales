using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGProject
{
    enum MagicElement {neutre , feu , glace}
    class Magic
    {

        public int[] numberofdices;
        public int[] dicenumbers;
        public int cost;
        public int[] bonus;
        public string name;
        public MagicElement[] elements;
        public Magic(int[] numberofdices, int[] dicenumbers, int[] bonus, MagicElement[] elements, int manacost, string name)
        {

            this.bonus = bonus;
            this.dicenumbers = dicenumbers;
            this.elements = elements;
            this.name = name;
            this.numberofdices = numberofdices;

        }

        public int calculatecost()
        {
            int ccost = 0;
            int n = 0;
            foreach (int i in dicenumbers)
            {
                ccost += i * n;
                n++;
            }

                return ccost;
        }
    }
}
