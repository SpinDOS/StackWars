using System;
using System.Linq;
using StackWars.Commands;
using StackWars.Logger;
using StackWars.UnitFactory;
using StackWars.GameEngine;
using GEngine = StackWars.GameEngine.GameEngine;

namespace StackWars
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CommandsInvoker.Logger = new ConsoleLogger();
            var unitFabric = new RandomUnitFactory();
            var armyCost = 500;
            GEngine.StartNewGame(unitFabric, armyCost);
            var gui = new ConsoleGUI();
            while (true)
            {
                gui.UndoAvailable = CommandsInvoker.CanUndo;
                gui.RedoAvailable = CommandsInvoker.CanRedo;
                gui.GameEnded = GEngine.CurrentGame.GameEnded;
                var input = gui.GetUserInput();
                switch (input)
                {
                case UserInput.CreateNewGame:
                    GEngine.StartNewGame(unitFabric, armyCost);
                    break;
                case UserInput.MakeTurn:
                    GEngine.CurrentGame.Turn();
                    break;
                case UserInput.PlayToEnd:
                    while (!GEngine.CurrentGame.GameEnded)
                        GEngine.CurrentGame.Turn();
                    break;
                case UserInput.ShowArmies:
                    Console.WriteLine(GEngine.CurrentGame.Army1);
                    Console.WriteLine(GEngine.CurrentGame.Army2);
                    break;
                case UserInput.SelectStrategy1vs1:
                    GEngine.CurrentGame.Strategy = FightStrategy1Vs1.Singleton;
                    gui.ShowMessage("Strategy changed to 1 vs 1");
                    break;
                case UserInput.SelectStrategy3vs3:
                    GEngine.CurrentGame.Strategy = FightStrategy3Vs3.Singleton;
                    gui.ShowMessage("Strategy changed to 3 vs 3");
                    break;
                case UserInput.SelectStrategyAllvsAll:
                    GEngine.CurrentGame.Strategy = FightStrategyAllVsAll.Singleton;
                    gui.ShowMessage("Strategy changed to all vs all");
                    break;
                case UserInput.Undo:
                    CommandsInvoker.Undo();
                    GEngine.CurrentGame.GameEnded = false;
                    gui.ShowMessage("Undo done");
                    break;
                case UserInput.Redo:
                    CommandsInvoker.Redo();
                    gui.ShowMessage("Redo done");
                    break;
                case UserInput.Exit:
                    return;
                }
                var currentGame = GEngine.CurrentGame;
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