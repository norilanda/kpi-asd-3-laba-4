namespace laba4
{
    public class Generation
    {
        private List<Creature> _currPopulation;
        private Creature bestCreature, worstCreature;
        private int n;
        private int MaxWeight;
        private int iterationNumber;
        public List<int> F_ValuesAfter20Iterations;

        private int genNum1;
        private int genNum2;
        private int genNum3;

        private double mutationPossibility;

        public Generation(int n, int P, int iterations)
        {
            // initialization            
            this.n = n;
            this.MaxWeight = P;
            this.iterationNumber = iterations;
            F_ValuesAfter20Iterations = new List<int>();

            genNum1 = (int)(0.3 * n); //30%
            genNum2 = (int)(0.4 * n); //40%
            genNum3 = n - genNum1 - genNum2;    //30%
            mutationPossibility = 0.1;  //10%

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
            int count20Iterations = 0;
            for (int i=0; i< this.iterationNumber; i++)
            {
                if (count20Iterations == 20)
                {
                    F_ValuesAfter20Iterations.Add(bestCreature.F);
                    count20Iterations = 0;
                }
                count20Iterations++;
                Creature parent1, parent2;
                Creature child1, child2;
                Selection(out parent1, out parent2);
                Crossover(parent1, parent2, out child1, out child2);

                // for the first child
                if (child1.P <= MaxWeight)//check if alive
                {
                    child1 = Mutation(child1);
                    child1 = LI_Subtitute(child1);
                    AddChildToPopulation(child1);
                }

                // for the second child
                if (child2.P <= MaxWeight)//check if alive
                {
                    child2 = Mutation(child2);
                    child2 = LI_Subtitute(child2);
                    AddChildToPopulation(child2);
                }
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
        private void Crossover(Creature parent1, Creature parent2, out Creature child1, out Creature child2)
        {
            //  creating 2 childrens
            bool[] childChromosome1 = new bool[n];
            bool[] childChromosome2 = new bool[n];
            Array.Copy(parent1.Chromosome, 0, childChromosome1, 0, genNum1);
            Array.Copy(parent2.Chromosome, genNum1, childChromosome1, genNum1, genNum2);
            Array.Copy(parent1.Chromosome, genNum1 + genNum2, childChromosome1, genNum1 + genNum2, genNum3);

            Array.Copy(parent2.Chromosome, 0, childChromosome2, 0, genNum1);
            Array.Copy(parent1.Chromosome, genNum1, childChromosome2, genNum1, genNum2);
            Array.Copy(parent2.Chromosome, genNum1 + genNum2, childChromosome2, genNum1 + genNum2, genNum3);

            child1 = new Creature(childChromosome1);
            child2 = new Creature(childChromosome2);
        }
        private Creature Mutation(Creature child)
        {
            Random rnd = new Random();
            if (rnd.NextDouble() <= mutationPossibility)
            {                
                bool[] newChromosome = new bool[n];
                Array.Copy(child.Chromosome, newChromosome, n);
                int gen1, gen2;
                gen1 = rnd.Next(0, n);
                do
                {
                    gen2 = rnd.Next(0, n);
                } while (gen1 == gen2);
                newChromosome[gen1] = child.Chromosome[gen2];
                newChromosome[gen2] = newChromosome[gen1]; ;
                Creature mutatedChild = new Creature(newChromosome);
                if (mutatedChild.P > MaxWeight) //if mutatedChild is dead
                    return child;
                else 
                    return mutatedChild;
            }
            return child;
        }
        private Creature LocalImprovement(Creature child)
        {
            bool[] newChromosome = new bool[n];
            int currF = child.F;
            int currP = child.P;
            Array.Copy(child.Chromosome, newChromosome, n);
            for (int i = 0; i < n; i++)
            {
                if (newChromosome[i] == false)
                {
                    if(currP + Creature.allItems[i].Weight <= MaxWeight && currF + Creature.allItems[i].Value > currF)// if alive and have better F
                    {
                        newChromosome[i] = true;
                        //currF += Creature.allItems[i].Value;
                        //currP += Creature.allItems[i].Weight;
                        break;
                    }
                }
            }
            return new Creature(newChromosome);
        }
        private Creature LI_Subtitute(Creature child)
        {
            bool[] newChromosome = new bool[n];
            int currF = child.F;
            int currP = child.P;
            Array.Copy(child.Chromosome, newChromosome, n);
            for (int i = 0; i < n; i++)
            {
                if (newChromosome[i] == false)
                {
                    for (int j=0; j < n; j++)
                    {
                        if (newChromosome[j] == true)
                        {
                            int tempP = currP + Creature.allItems[i].Weight - Creature.allItems[j].Weight;
                            int tempF = currF + Creature.allItems[i].Value - Creature.allItems[j].Value;
                            if (tempP <= MaxWeight && tempF > currF)
                            {
                                newChromosome[i] = true;
                                newChromosome[j] = false;
                                break;
                            }
                        }
                    }
                }
            }
            return new Creature(newChromosome);
        }
        private void AddChildToPopulation(Creature child)   //add child and remove the worst
        {           
            if (bestCreature.F < child.F)
                bestCreature = child;
            if (worstCreature.F > child.F)
                worstCreature = child;
            _currPopulation.Add(child);
            _currPopulation.Remove(worstCreature);
            worstCreature = FindWorstCreature();    //finding a new worst creature
        }
        private Creature FindBestCreature()
        {
            Creature best = new Creature(new bool[n]);
            for (int i = 0; i < _currPopulation.Count; i++)
            {
                if (_currPopulation[i].F > best.F && _currPopulation[i].P <= MaxWeight)
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
        public Creature GetBest() => bestCreature;
    }
}
