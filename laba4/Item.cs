using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
    public class Item
    {
        private int _value;
        private int _weight;

        public int Value => _value;
        public int Weight => _weight;
        public Item(int v, int w) 
        {
            this._value = v;
            this._weight = w;
        }

        public static Item Generate(int vMin, int vMax, int wMin, int wMax)
        {
            Random rnd = new Random();
            int v = rnd.Next(vMin, vMax + 1);
            int w = rnd.Next(wMin, wMax + 1);
            return new Item(v, w);
        }
    }
}
