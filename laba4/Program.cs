using laba4;

int n = 50;
int P = 250;
int iterations = 100;
Item.vMin = 2; Item.vMax = 30;
Item.wMin = 1; Item.wMax = 25;
Creature.allItems = Item.GenerateItems(n);

Generation g = new Generation(n, P, iterations);
g.GeneticAlgorithm();

ConsoleDisplay.DisplayAllItems(Creature.allItems);
Console.WriteLine("\n");
Console.WriteLine("Solution:");
ConsoleDisplay.DisplaySolution(g.GetBest());

List<int> Fs = g.F_ValuesAfter20Iterations;
Console.WriteLine("F changes: ");
for(int i=0; i< Fs.Count; i++)
    Console.Write(Fs[i] + " ");
