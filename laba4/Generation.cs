using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace laba4
{
    
    public class Generation
    {
        public enum SelectionMethod
        {
            BestAndRandom,
            Tournament
        }
        public enum LocalImprovementMethod
        {
            Superset,
            Subtitute,
            Hybrid //(theory)
        }
        private SortedList<int, Creature> _currPopulation;
        private Creature bestCreature;//, worstCreature;
        private int n;
        private int MaxWeight;
        private int iterationNumber;
        public List<int> F_ValuesAfter20Iterations;

        private int genNum1;
        private int genNum2;
        private int genNum3;

        private double mutationPossibility;
        SelectionMethod selectMethod;
        LocalImprovementMethod imprMethod;
        int setNumber;//var for tournament selection

        public Generation(int n, int P, int iterations, int selectMethod, int imprMethod=2)
        {
            // initialization
            this.selectMethod = (SelectionMethod)selectMethod;
            this.imprMethod = (LocalImprovementMethod)imprMethod;
            this.n = n;
            this.MaxWeight = P;
            this.iterationNumber = iterations;
            F_ValuesAfter20Iterations = new List<int>();

            genNum1 = (int)(0.3 * n); //30%
            genNum2 = (int)(0.4 * n); //40%
            genNum3 = n - genNum1 - genNum2;    //30%
            mutationPossibility = 0.1;  //10%

            _currPopulation = new SortedList<int, Creature>(new DuplicateKeyComparer<int>());
            CreateInitialPopulation(n);
            bestCreature = _currPopulation.Last().Value;
            //worstCreature = FindWorstCreature();

            if (this.selectMethod == SelectionMethod.Tournament)
            {
                switch (n)
                {
                    case < 5: setNumber = 2; break;
                    case < 20: setNumber = (int)(n * 0.4); break;
                    default: setNumber = (int)(n * 0.2); break;
                }               
            }          

        }
        private void CreateInitialPopulation(int n)
        {
            bool[] chromosome;
            for (int i=0; i<n; i++)
            {
                chromosome = new bool[n];
                chromosome[i] = true;
                Creature creature = new Creature(chromosome);
                _currPopulation.Add(creature.F, creature);
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
                    child1 = LocalImprovement(child1);
                    AddChildToPopulation(child1);
                }

                // for the second child
                if (child2.P <= MaxWeight)//check if alive
                {
                    child2 = Mutation(child2);
                    child2 = LocalImprovement(child2);
                    AddChildToPopulation(child2);
                }
            }
        }
        private void Selection(out Creature parent1, out Creature parent2)
        {
            switch (selectMethod)
            {
                case SelectionMethod.Tournament:
                    {
                        S_Tournament(out parent1, out parent2);
                        break;
                    }
                case SelectionMethod.BestAndRandom:
                    {
                        S_BestAndRandom(out parent1, out parent2);
                        break;
                    }  
                default:
                    {
                        parent1 = _currPopulation.ElementAt(0).Value;
                        parent2 = _currPopulation.ElementAt(1).Value;
                        break;
                    }
            }
        }
        private void S_BestAndRandom(out Creature parent1, out Creature parent2)
        {
            Random rnd = new Random();
            parent1 = bestCreature;
            do
            {
                parent2 = _currPopulation.ElementAt(rnd.Next(0, _currPopulation.Count)).Value;
            } while (parent1 == parent2);
        }
        private void S_Tournament(out Creature parent1, out Creature parent2)
        {
           
            SortedList<int, Creature> subList = ChooseSublist(setNumber);
            parent1 = subList.Last().Value;
            subList = ChooseSublist(setNumber);
            int indexOfParent1 = subList.IndexOfValue(parent1);
            if (indexOfParent1 != -1)
                subList.RemoveAt(indexOfParent1);
           //subList.Remove(parent1);//avoiding choosing the same parent twice
            parent2 = subList.Last().Value;
        }
        private SortedList<int, Creature> ChooseSublist(int setNumber)
        {
            Random rnd = new Random();
            SortedList<int, Creature> subList = new SortedList<int, Creature>(new DuplicateKeyComparer<int>());
            for (int i = 0; i < setNumber; i++)
            {
                int randNumb = rnd.Next();
                if (!subList.ContainsValue(_currPopulation.ElementAt(randNumb).Value))
                    subList.Add(_currPopulation.ElementAt(randNumb).Value.F, _currPopulation.ElementAt(randNumb).Value);
                else
                    i--;
            }
            return subList;
        }
        //private void S_Proportional(out Creature parent1, out Creature parent2)
        //{
        //    Random rnd = new Random();
        //    while (true)
        //    {
        //        break;
        //        /// Finish!!!!!!!!!!!!!!!!!!!!
        //    }
        //}

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
                newChromosome[gen2] = child.Chromosome[gen1];//
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
           switch (imprMethod)
            {
                case LocalImprovementMethod.Superset:
                    return LI_Superset(child);
                case LocalImprovementMethod.Subtitute:
                    return LI_Subtitute(child);
                case LocalImprovementMethod.Hybrid:
                    return LI_Hybrid(child);
                default:
                    return child;
            }
        }
        private Creature LI_Superset(Creature child)    //local improvement method
        {
            bool[] newChromosome = new bool[n];
            int currF = child.F;
            int currP = child.P;
            Array.Copy(child.Chromosome, newChromosome, n);
            Random rnd = new Random();
            if (rnd.Next(0,1) == 0)
            {
                for (int i = 0; i < n; i++)
                {
                    if (newChromosome[i] == false)
                    {
                        if (currP + Creature.allItems[i].Weight <= MaxWeight && currF + Creature.allItems[i].Value > currF)// if alive and have better F
                        {
                            newChromosome[i] = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = n-1; i >=0; i--)
                {
                    if (newChromosome[i] == false)
                    {
                        if (currP + Creature.allItems[i].Weight <= MaxWeight && currF + Creature.allItems[i].Value > currF)// if alive and have better F
                        {
                            newChromosome[i] = true;
                            break;
                        }
                    }
                }
            }
            return new Creature(newChromosome);
        }
        private Creature LI_Subtitute(Creature child)   //local improvement method
        {            
            bool[] newChromosome = new bool[n];
            int currF = child.F;
            int currP = child.P;
            Array.Copy(child.Chromosome, newChromosome, n);
            for (int i = 0; i < n; i++)
            {
                if (child.Chromosome[i] == false)
                {
                    for (int j=0; j < n; j++)
                    {
                        if (child.Chromosome[j] == true)
                        {
                            int tempP = currP + Creature.allItems[i].Weight - Creature.allItems[j].Weight;
                            int tempF = currF + Creature.allItems[i].Value - Creature.allItems[j].Value;
                            if (tempP <= MaxWeight && tempF > currF)
                            {
                                newChromosome[i] = true;
                                newChromosome[j] = false;
                                Creature ch = new Creature(newChromosome);
                                if (ch.P > MaxWeight)
                                {
                                    return child;
                                    //foreach (bool b in child.Chromosome)
                                    //    Console.Write(b +" ");
                                    //Console.WriteLine("Substitute" + " currP=" + currP + " W+= " + Creature.allItems[i].Weight + " W-= " + Creature.allItems[j].Weight + " ch.P " + ch.P + " i= " + i + " j= " + j + " tempP= " + tempP);
                                    //Creature ch1 = new Creature(newChromosome);
                                    //foreach (bool b in newChromosome)
                                    //    Console.Write(b + " ");
                                    //Console.WriteLine("\n");
                                }
                                   
                                break;
                            }
                        }
                    }
                }
            }
           
            return new Creature(newChromosome);
        }

        private Creature LI_Hybrid(Creature child)
        {
            const double LIMIT = 0.4;
            Random rnd= new Random();
            if (rnd.NextDouble() < LIMIT)
                return LI_Superset(child);
            else return LI_Subtitute(child);
        }
        private void AddChildToPopulation(Creature child)   //add child and remove the worst
        {           
            if (bestCreature.F < child.F)
                bestCreature = child;
            //if (worstCreature.F > child.F)
            //    worstCreature = child;
            _currPopulation.Add(child.F, child);
            _currPopulation.RemoveAt(0);
            //worstCreature = FindWorstCreature();    //finding a new worst creature
        }
        //private Creature FindBestCreature(List<Creature> population)
        //{
        //    Creature best = new Creature(new bool[n]);
        //    for (int i = 0; i < population.Count; i++)
        //    {
        //        if (population[i].F > best.F && population[i].P <= MaxWeight)
        //        {
        //            best = population[i];
        //        }                    
        //    }
        //    return best;
        //}    
        //private Creature FindWorstCreature()
        //{
        //    Creature worst = _currPopulation[0];
        //    for (int i = 0; i < _currPopulation.Count; i++)
        //    {
        //        if (_currPopulation[i].F < worst.F)
        //        {
        //            worst = _currPopulation[i];
        //        }
        //    }
        //    return worst;
        //}
        public Creature GetBest() => bestCreature;
    }
}
