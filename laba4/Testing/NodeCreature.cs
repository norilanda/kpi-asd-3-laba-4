using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4.Testing
{
    public class NodeCreature : Creature
    {
        private int _depth;
        public int Depth
        {
            get { return _depth; }
        }
        public NodeCreature(bool[] chromosome, int depth) : base (chromosome)
        {
            this._depth = depth;
        }
    }
}
