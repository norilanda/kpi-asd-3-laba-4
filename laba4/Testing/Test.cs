using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4.Testing
{
    //Creature.allItems = Item.InitItems(new int[] { 22, 23, 11, 9 }, new int[] { 5, 22, 21, 1 });
    //Creature.allItems = Item.InitItems(new int[] { 24, 7, 17 }, new int[] { 2, 22, 10 });
    //Creature.allItems = Item.InitItems(new int[] { 18, 23 }, new int[] { 21, 9 });

    //n = 20;
    //Creature.allItems = Item.InitItems(new int[] { 13,7,29,24,25,29,3,15,19,21,24,3,15,24,23,7,21,8,17,21}, 
    //                                   new int[] { 3,21,15,17,4,21,2,18,16,18,6,13,8,18,23,20,24,2,25,6 });

    //n = 10;
    //Creature.allItems = Item.InitItems(new int[] { 8,8,7,6,3,4,18,2,20,2 },
    //                                   new int[] { 12,14,8,10,3,3,6,12,20,22 });
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
