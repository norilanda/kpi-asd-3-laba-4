using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba4
{
    public class ConsoleDisplay
    {
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
