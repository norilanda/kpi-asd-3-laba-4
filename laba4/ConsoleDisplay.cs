using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
    public static class ConsoleDisplay
    {
        public static void InputFromUser(ref int n, ref int P, ref int iterations,  
            ref int vMin, ref int vMax, ref int wMin, ref int wMax, ref int selectMethod, ref int imprMethod)
        {
            Console.Write("\nItem number = " + n + ";   knapsack max weight = " + P + ";  number of iterations to terminate: "+ iterations + 
                ";\nitems values in range (" + vMin +", " + vMax + ");   items weights in range (" + wMin + ", " + wMax + ";    Selection method: ");
            switch (imprMethod)
            {
                case 0: Console.WriteLine("Superset\n"); break;
                case 1: Console.WriteLine("Subtitute\n"); break;
                case 2: Console.WriteLine("Hybrid\n"); break;
            }
            
            Console.WriteLine("Choose selection method: 0 - BestAndRandom, 1 - Tournament, 2 - Proportionate:");
            selectMethod = Convert.ToInt32(Console.ReadLine());
        }
        public static void DisplayAllItems(Item[] items)
        {
            string itemNumbers, itemValues, itemWeight;
            itemNumbers = itemValues = itemWeight = "";
            int itemsAdded = 0;
            for (int i = 0; i < items.Length; i++)
            {
                itemNumbers += Convert.ToString(i).PadLeft(4) + "|";
                itemValues += Convert.ToString(items[i].Value).PadLeft(4) + "|";
                itemWeight += Convert.ToString(items[i].Weight).PadLeft(4) + "|";
                itemsAdded++;
                if (itemsAdded == 30)
                {
                    Console.WriteLine("Number" + itemNumbers);
                    Console.WriteLine("Value " + itemValues);
                    Console.WriteLine("Weight" + itemWeight);
                    Console.WriteLine("---------------");
                    itemsAdded = 0;
                    itemNumbers = itemValues = itemWeight = "";
                }
            }
            if (itemsAdded > 0)
            {
                Console.WriteLine("Number" + itemNumbers);
                Console.WriteLine("Value " + itemValues);
                Console.WriteLine("Weight" + itemWeight);
            }
        }

        public static void DisplaySolution(Creature bestCreature)
        {
            string itemNumbers, itemValues;
            itemNumbers = itemValues = "";
            int itemsAdded = 0;
            for (int i = 0; i < bestCreature.Chromosome.Length; i++)
            {
                itemNumbers += Convert.ToString(i).PadLeft(4) + "|";
                itemValues += Convert.ToString(Convert.ToInt32(bestCreature.Chromosome[i])).PadLeft(4) + "|";
                itemsAdded++;
                if (itemsAdded == 30)
                {
                    Console.WriteLine("Number" + itemNumbers);
                    Console.WriteLine("Value " + itemValues);
                    Console.WriteLine("---------------");
                    itemsAdded = 0;
                    itemNumbers = itemValues = "";
                }
            }
            if (itemsAdded > 0)
            {
                Console.WriteLine("Number" + itemNumbers);
                Console.WriteLine("Value " + itemValues);
            }

            Console.WriteLine("F = " + bestCreature.F);
            Console.WriteLine("P = " + bestCreature.P);
        }
    }
}
