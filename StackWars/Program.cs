using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecialUnits;
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
            int armyCost = 500;
            GameEngine.StartNewGame(unitFabric, armyCost);
            var gui = new ConsoleGUI();
            while (true)
            {
                gui.UndoAvailable = CommandsInvoker.CanUndo;
                gui.RedoAvailable = CommandsInvoker.CanRedo;
                gui.GameEnded = GameEngine.CurrentGame.GameEnded;
                UserInput input = gui.GetUserInput();
                switch (input)
                {
                case UserInput.CreateNewGame:
                    GameEngine.StartNewGame(unitFabric, armyCost);
                    break;
                case UserInput.MakeTurn:
                    GameEngine.CurrentGame.Turn();
                    break;
                case UserInput.PlayToEnd:
                    while (!GameEngine.CurrentGame.GameEnded)
                        GameEngine.CurrentGame.Turn();
                    break;
                case UserInput.ShowArmies:
                    Console.WriteLine(GameEngine.CurrentGame.Army1);
                    Console.WriteLine(GameEngine.CurrentGame.Army2);
                    break;
                case UserInput.SelectStrategy1vs1:
                case UserInput.SelectStrategy3vs3:
                case UserInput.SelectStrategyAllvsAll:
                    throw new NotImplementedException();
                case UserInput.Undo:
                    CommandsInvoker.Undo();
                    GameEngine.CurrentGame.GameEnded = false;
                    gui.ShowMessage("Undo done");
                    break;
                case UserInput.Redo:
                    CommandsInvoker.Redo();
                    gui.ShowMessage("Redo done");
                    break;
                case UserInput.Exit:
                    return;
                }
                var currentGame = GameEngine.CurrentGame;
                if (!currentGame.GameEnded)
                    continue;
                if (currentGame.Army1.Count == 0)
                    gui.ShowMessage(currentGame.Army2.Name + " wins");
                else if (currentGame.Army2.Count == 0)
                    gui.ShowMessage(currentGame.Army1.Name + " wins");
                else
                    gui.ShowMessage("Draw");

            }
        }
    }
}