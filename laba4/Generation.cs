namespace laba4
{
    public class Generation
    {
        private List<Creature> _currPopulation;
        private Creature bestCreature, worstCreature;
        private int n;
        private int MaxWeight;

        private int genNum1;
        private int genNum2;
        private int genNum3;

        private double mutationPossibility;

        public Generation(int n, int P)
        {
            // initialization
            Item.vMin = 2; Item.vMax = 25;
            Item.wMax = 1; Item.wMax = 10;
            this.n = n;
            this.MaxWeight = P;

            genNum1 = (int)(0.3 * n); //30%
            genNum2 = (int)(0.4 * n); //40%
            genNum3 = n - genNum1 - genNum2;    //30%
            mutationPossibility = 0.1;  //10%

            Creature.allItems = Item.GenerateItems(n);
            _currPopulation = new List<Creature>();
            CreateInitialPopulation(n);
            bestCreature = FindBestCreature();
            worstCreature = FindWorstCreature();
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
                if (child.P > MaxWeight)//check if alive
                    continue;
                child = Mutation(child);
                // local improvements !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                if (bestCreature.F < child.F)
                    bestCreature = child;
                if (worstCreature.F > child.F)
                    worstCreature = child;
                _currPopulation.Add(child);
                _currPopulation.Remove(worstCreature);
            }
        }
        private void Selection(out Creature parent1, out Creature parent2)
        {
            Random rnd = new Random();
            parent1 = bestCreature;
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
        private Creature Mutation(Creature child)
        {
            Random rnd = new Random();
            if (rnd.NextDouble() <= mutationPossibility)
            {                
                bool[] child1Chromosome = new bool[n];
                Array.Copy(child.Chromosome, child1Chromosome, n);
                int gen1, gen2;
                gen1 = rnd.Next(0, n);
                do
                {
                    gen2 = rnd.Next(0, n);
                } while (gen1 == gen2);
                child1Chromosome[gen1] = child.Chromosome[gen2];
                child1Chromosome[gen2] = child1Chromosome[gen1]; ;
                Creature child1 = new Creature(child1Chromosome);
                if (child1.P > MaxWeight) //if child1 is dead
                    return child;
                else 
                    return child1;
            }
            return child;
        }
        private Creature LocalImprovement(Creature child)
        {
            return child;
        }
        private Creature FindBestCreature()
        {
            Creature best = _currPopulation[0];
            for (int i = 0; i < _currPopulation.Count; i++)
            {
                if (_currPopulation[i].F > best.F)
                {
                    best = _currPopulation[i];
                }                    
            }
            return best;
        }    
        private Creature FindWorstCreature()
        {
            Creature worst = _currPopulation[0];
            for (int i = 0; i < _currPopulation.Count; i++)
            {
                if (_currPopulation[i].F < worst.F)
                {
                    worst = _currPopulation[i];
                }
            }
            return worst;
        }
    }
}
