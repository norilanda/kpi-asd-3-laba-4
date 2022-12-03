using laba4;

int P = 30;
Generation g = new Generation(10, P);
g.GeneticAlgorithm();

ConsoleDisplay.DisplayAllItems(Creature.allItems);
Console.WriteLine();
ConsoleDisplay.DisplaySolution(g.GetBest());