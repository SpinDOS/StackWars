using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars
{
    class Program
    {
        static void Main(string[] args)
        {
            var unitFabric = new RandomUnitFactory();
            GameEngine.StartNewGame(unitFabric, 300);
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
                        GameEngine.StartNewGame(unitFabric, 300);
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
                        Console.WriteLine(GameEngine.CurrentGame.Army1);
                        Console.WriteLine(GameEngine.CurrentGame.Army2);
                        break;
                    case 5:
                        return;
                }
            }
        }
    }
}
