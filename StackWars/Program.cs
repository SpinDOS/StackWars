using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Commands;
using StackWars.Logger;
using StackWars.UnitFactory;

namespace StackWars
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandsInvoker.Logger = new ConsoleLogger();
            var unitFabric = new RandomUnitFactory();
            int maxCost = 500;
            GameEngine.StartNewGame(unitFabric, maxCost);
            while (true)
            {
                Console.WriteLine("1. Start new game");
                Console.WriteLine("2. Make turn");
                Console.WriteLine("3. Play to end");
                Console.WriteLine("4. Show armies");
                Console.WriteLine("5. Exit");
                Console.Write("Your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                    continue;
                switch (choice)
                {
                    case 1:
                        GameEngine.StartNewGame(unitFabric, maxCost);
                        Console.WriteLine("Created!");
                        break;
                    case 2:
                        GameEngine.CurrentGame.Turn();
                        break;
                    case 3:
                        while (!GameEngine.CurrentGame.GameEnded)
                            GameEngine.CurrentGame.Turn();
                        break;
                    case 4:
                        Console.WriteLine(CommandsInvoker.Army1);
                        Console.WriteLine(CommandsInvoker.Army2);
                        break;
                    case 5:
                        return;
                }
            }
        }
    }
}
