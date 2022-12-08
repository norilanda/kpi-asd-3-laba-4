using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4.Testing
{
    public class Test
    {
        // class for testing the solution with a little number of items by using ...
        NodeCreature bestCreature;
        private int n, P;
        public Test(int n, int P)
        {
            this.n = n;
            this.P = P;
        }
        public NodeCreature StartTest()
        {
            List<NodeCreature> successors;
            Queue<NodeCreature> openList = new Queue<NodeCreature>();
            bestCreature = new NodeCreature(new bool[n], 0);
            openList.Enqueue(bestCreature);
            while (openList.Count > 0)
            {
                NodeCreature curr = openList.Dequeue();
                if (curr.P <= P && curr.F > bestCreature.F)
                    bestCreature = curr;
                successors = Expand(curr);
                foreach (NodeCreature successor in successors)
                    openList.Enqueue(successor);
            }
            return bestCreature;
        }

        private List<NodeCreature> Expand(NodeCreature curr)
        {
            int depth = curr.Depth;
            List<NodeCreature> successors = new List<NodeCreature>();
            if (depth < n)
            {
                bool[] chromosome1 = new bool[n];
                bool[] chromosome2 = new bool[n];
                Array.Copy(curr.Chromosome, chromosome1, n);
                chromosome1[depth] = false; 
                successors.Add(new NodeCreature(chromosome1, curr.Depth + 1));

                Array.Copy(curr.Chromosome, chromosome2, n);
                chromosome2[depth] = true;
                successors.Add(new NodeCreature(chromosome2, curr.Depth + 1));
            }
            return successors;
        }
    }
}
