using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
    public class Creature
    {
        public static Item[] allItems;

        protected bool[] _chromosome;
        protected int _F; //value
        protected int _P; //weight
        private int _itemNumber;    // var to decide which local improvment method to use

        public int F => _F;
        public int P => _P;
        public int ItemNumber => _itemNumber;
        public bool[] Chromosome => _chromosome;
        public Creature(bool[] chromosome)
        {
            this._chromosome = new bool[chromosome.Length];
            Array.Copy(chromosome, this._chromosome, this._chromosome.Length);
            for (int i=0; i<_chromosome.Length; i++)
            {
                if (_chromosome[i])
                {
                    _F += allItems[i].Value;
                    _P += allItems[i].Weight;
                    _itemNumber++;
                }                
            }
        }  
    }
}
