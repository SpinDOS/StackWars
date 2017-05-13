using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars
{
    public enum UserInput
    {
        NotDefined,
        CreateNewGame,
        MakeTurn,
        PlayToEnd,
        ShowArmies,
        SelectStrategy1vs1,
        SelectStrategy3vs3,
        SelectStrategyAllvsAll,
        Undo,
        Redo,
        Exit,
    }

    public sealed class ConsoleGUI
    {
        public bool UndoAvailable { get; set; }
        public bool RedoAvailable { get; set; }
        public bool GameEnded { get; set; }

        public UserInput GetUserInput()
        {
            while (true)
            {
                Console.WriteLine("Main menu: ");
                Console.WriteLine("1. Start new game");
                Console.WriteLine("2. Make turn");
                Console.WriteLine("3. Play to end");
                Console.WriteLine("4. Show armies");
                Console.WriteLine("5. Select fight strategy");
                Console.WriteLine("6. Undo");
                Console.WriteLine("7. Redo");
                Console.WriteLine("8. Exit");
                Console.Write("Your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 8)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                if (GameEnded && choice != 1 && choice != 4 && choice != 6 && choice != 8)
                {
                    Console.WriteLine("Game ended");
                    continue;
                }
                switch (choice)
                {
                case 1:
                    return UserInput.CreateNewGame;
                case 2:
                    return UserInput.MakeTurn;
                case 3:
                    return UserInput.PlayToEnd;
                case 4:
                    return UserInput.ShowArmies;
                case 5:
                {
                    UserInput result = SelectStrategy();
                    if (result != UserInput.NotDefined)
                        return result;
                    break;
                }
                case 6:
                    if (UndoAvailable)
                        return UserInput.Undo;
                    Console.WriteLine("Undo is not available now");
                    break;
                case 7:
                    if (RedoAvailable)
                        return UserInput.Redo;
                    Console.WriteLine("Redo is not available now");
                    break;
                case 8:
                    return UserInput.Exit;
                }
            }
            
        }

        public void ShowMessage(string message) => Console.WriteLine(message);

        private static UserInput SelectStrategy()
        {
            while (true)
            {
                Console.WriteLine("Select strategy");
                Console.WriteLine("1. 1 vs 1");
                Console.WriteLine("2. 3 vs 3");
                Console.WriteLine("3. All vs all");
                Console.WriteLine("4. Cancel");
                Console.Write("Your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                switch (choice)
                {
                case 1:
                    return UserInput.SelectStrategy1vs1;
                case 2:
                    return UserInput.SelectStrategy3vs3;
                case 3:
                    return UserInput.SelectStrategyAllvsAll;
                case 4:
                    return UserInput.NotDefined;
                }
            }

        }
    }
        
}
