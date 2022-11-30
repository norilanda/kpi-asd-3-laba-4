using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
    public class Generation
    {
        private List<Creature> _currPopulation;
        private int bestF;

        public Generation(int n)
        {
            Item.vMin = 2; Item.vMax = 25;
            Item.wMax = 1; Item.wMax = 10;
            Creature.allItems = Item.GenerateItems(n);
            _currPopulation = new List<Creature>();
            CreateInitialPopulation(n);
            bestF = FindBestF();
        }
        private void CreateInitialPopulation(int n)
        {
            bool[] chromosome;
            for (int i=0; i<n; i++)
            {
                chromosome = new bool[n];
                chromosome[i] = true;
                _currPopulation.Add(new Creature(chromosome));
            }
        }
        private int FindBestF()
        {
            int max = int.MinValue;
            for (int i = 0; i < _currPopulation.Count; i++)
            {
                if (_currPopulation[i].F > max)
                    max = _currPopulation[i].F;
            }
            return max;
        }
    }
}
