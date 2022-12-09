using laba4;
using laba4.Testing;

int n = 100;
int P = 250;
int iterations = 1000;
Item.vMin = 2; Item.vMax = 30;
Item.wMin = 1; Item.wMax = 25;
int selectMethod = 2; // 0 - BestAndRandom, 1 - Tournament, 2 - Proportionate
int imprMethod = 2; // 0 - Superset, 1 - Subtitute, 2 - Hybrid
ConsoleDisplay.InputFromUser(ref n, ref P, ref iterations, 
    ref Item.vMin, ref Item.vMax, ref Item.wMin, ref Item.wMax, ref selectMethod, ref imprMethod);
Creature.allItems = Item.GenerateItems(n);


Generation g = new Generation(n, P, iterations, selectMethod, imprMethod);
g.GeneticAlgorithm();

Console.WriteLine("Knapsack items:");
ConsoleDisplay.DisplayAllItems(Creature.allItems);
Console.WriteLine("\n");
Console.WriteLine("Solution:");
ConsoleDisplay.DisplaySolution(g.GetBest());

List<int> Fs = g.F_ValuesAfter20Iterations;
Console.WriteLine("F changes: ");
for(int i=0; i< Fs.Count; i++)
    Console.Write(Fs[i] + " ");
Console.WriteLine("\n");

if (n <= 20)
{
    Test test = new Test(n, P);
    Creature testedCreatures = test.StartTest();
    Console.WriteLine("Tested wiht viewing all the creatures: ");
    ConsoleDisplay.DisplaySolution(testedCreatures);
}
