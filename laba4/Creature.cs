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
        public Creature(bool[] chromosome)
        {
            this._chromosome = chromosome;
        }
    }
}
