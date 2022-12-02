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
        private int n;

        private int genNum1;
        private int genNum2;
        private int genNum3;

        public Generation(int n)
        {
            // initialization
            Item.vMin = 2; Item.vMax = 25;
            Item.wMax = 1; Item.wMax = 10;
            this.n = n;

            genNum1 = (int)(0.3 * n); //30%
            genNum2 = (int)(0.4 * n); //40%
            genNum3 = n - genNum1 - genNum2;    //30%

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
        public void GeneticAlgorithm()
        {
            const int iterationNumber = 100;
            for (int i=0; i<iterationNumber; i++)
            {
                Creature parent1, parent2;
                Selection(out parent1, out parent2);
                Creature child = Crossover(parent1, parent2);
            }
        }
        private void Selection(out Creature parent1, out Creature parent2)
        {
            Random rnd = new Random();
            parent1 = FindBestParent();
            do
            {
                parent2 = _currPopulation[rnd.Next(0, _currPopulation.Count)];
            } while (parent1 == parent2);
        }
        private Creature Crossover(Creature parent1, Creature parent2)
        {
            bool[] childChromosome = new bool[n];
            Array.Copy(parent1.Chromosome, 0, childChromosome, 0, genNum1);
            Array.Copy(parent2.Chromosome, genNum1, childChromosome, genNum1, genNum2);
            Array.Copy(parent1.Chromosome, genNum1 + genNum2, childChromosome, genNum1 + genNum2, genNum3);

            return new Creature(childChromosome);
        }

        private Creature FindBestParent()
        {
            int max = int.MinValue;
            Creature bestP = _currPopulation[0];
            for (int i = 0; i < _currPopulation.Count; i++)
            {
                if (_currPopulation[i].F > max)
                {
                    max = _currPopulation[i].F;
                    bestP = _currPopulation[i];
                }                    
            }
            return bestP;
        }
        private int FindBestF()
        {
            return FindBestParent().F;
        }
    }
}
