using laba4;
using laba4.Testing;

int n = 20;
int P = 10;
int iterations = 600;
Item.vMin = 2; Item.vMax = 30;
Item.wMin = 1; Item.wMax = 25;
int selectMethod = 1; // 0 - BestAndRandom, 1 - Tournament
int imprMethod = 2; // 0 - Superset, 1 - Subtitute, 2 - LI_Hybrid
Creature.allItems = Item.GenerateItems(n);

//Creature.allItems = Item.InitItems(new int[] { 22, 23, 11, 9 }, new int[] { 5, 22, 21, 1 });
//Creature.allItems = Item.InitItems(new int[] { 24, 7, 17 }, new int[] { 2, 22, 10 });
//Creature.allItems = Item.InitItems(new int[] { 18, 23 }, new int[] { 21, 9 });

//n = 20;
//Creature.allItems = Item.InitItems(new int[] { 13,7,29,24,25,29,3,15,19,21,24,3,15,24,23,7,21,8,17,21}, 
//                                   new int[] { 3,21,15,17,4,21,2,18,16,18,6,13,8,18,23,20,24,2,25,6 });

Generation g = new Generation(n, P, iterations, selectMethod, imprMethod);
g.GeneticAlgorithm();

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
