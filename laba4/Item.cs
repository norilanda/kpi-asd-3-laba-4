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

        public static int vMin, vMax, wMin, wMax;
        private static Item Generate()
        {
            Random rnd = new Random();
            int v = rnd.Next(vMin, vMax + 1);
            int w = rnd.Next(wMin, wMax + 1);
            return new Item(v, w);
        }
        public static Item[] GenerateItems(int n)
        {
            Item[] items = new Item[n];
            for (int i=0; i<n; i++)            
                items[i] = Generate();
            return items;
        }
        public static Item[] InitItems(int[] values, int[] weights)
        {
            int n = values.Length;
            Item[] items = new Item[n];
            for (int i = 0; i < n; i++)
                items[i] = new Item(values[i], weights[i]);                
            return items;
        }
    }
}
