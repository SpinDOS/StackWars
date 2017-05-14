using System;
using System.Linq;
using StackWars.Commands;
using StackWars.Logger;
using StackWars.UnitFactory;
using StackWars.GameEngine;
using StackWars.GUI;
using GEngine = StackWars.GameEngine.GameEngine;

namespace StackWars
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CommandsInvoker.Logger = new ConsoleLogger();
            var unitFabric = new RandomUnitFactory();
            var gui = StackWarsGUIFactory.GetGUI();
            var armyCost = 500;
            var engine = GEngine.StartNewGame(unitFabric, armyCost);
            while (true)
            {
                gui.UndoAvailable = CommandsInvoker.CanUndo;
                gui.RedoAvailable = CommandsInvoker.CanRedo;
                gui.GameEnded = engine.GameEnded;
                var input = gui.GetUserInput();
                switch (input)
                {
                case UserInput.CreateNewGame:
                    engine = GEngine.StartNewGame(unitFabric, armyCost);
                    break;
                case UserInput.MakeTurn:
                    engine.Turn();
                    break;
                case UserInput.PlayToEnd:
                    while (!engine.GameEnded)
                        engine.Turn();
                    break;
                case UserInput.ShowArmies:
                    Console.WriteLine(engine.Army1);
                    Console.WriteLine(engine.Army2);
                    break;
                case UserInput.SelectStrategy1vs1:
                    engine.Strategy = FightStrategy1Vs1.Singleton;
                    gui.ShowMessage("Strategy changed to 1 vs 1");
                    break;
                case UserInput.SelectStrategy3vs3:
                    engine.Strategy = FightStrategy3Vs3.Singleton;
                    gui.ShowMessage("Strategy changed to 3 vs 3");
                    break;
                case UserInput.SelectStrategyAllvsAll:
                    engine.Strategy = FightStrategyAllVsAll.Singleton;
                    gui.ShowMessage("Strategy changed to all vs all");
                    break;
                case UserInput.Undo:
                    CommandsInvoker.Undo();
                    engine.GameEnded = false;
                    gui.ShowMessage("Undo done");
                    break;
                case UserInput.Redo:
                    CommandsInvoker.Redo();
                    gui.ShowMessage("Redo done");
                    break;
                case UserInput.Exit:
                    return;
                }
                if (!engine.GameEnded)
                    continue;
                if (engine.Army1.Count == 0)
                    gui.ShowMessage(engine.Army2.Name + " wins");
                else if (engine.Army2.Count == 0)
                    gui.ShowMessage(engine.Army1.Name + " wins");
                else
                    gui.ShowMessage("Draw");
            }
        }
    }
}