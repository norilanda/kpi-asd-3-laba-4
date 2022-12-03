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
            for (int i = 0; i < items.Length; i++)
            {
                itemNumbers += Convert.ToString(i).PadLeft(4) + "|";
                itemValues += Convert.ToString(items[i].Value).PadLeft(4) + "|";
                itemWeight += Convert.ToString(items[i].Weight).PadLeft(4) + "|";
            }
            Console.WriteLine("Number" + itemNumbers);
            Console.WriteLine("Value " + itemValues);
            Console.WriteLine("Weight" + itemWeight);
        }
    }
}
