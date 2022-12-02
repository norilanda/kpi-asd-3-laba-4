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

        private bool[] _chromosome;
        private int _F; //value
        private int _P; //weight

        public int F => _F;
        public int P => _P;
        public bool[] Chromosome => _chromosome;
        public Creature(bool[] chromosome)
        {
            this._chromosome = chromosome;
            for (int i=0; i<_chromosome.Length; i++)
            {
                if (_chromosome[i])
                {
                    _F += allItems[i].Value;
                    _P += allItems[i].Weight;
                }                
            }
        }
        
    }
}
