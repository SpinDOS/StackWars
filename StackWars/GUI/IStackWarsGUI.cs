using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars.GUI
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
        Exit
    }

    public interface IStackWarsGUI
    {
        bool UndoAvailable { get; set; }
        bool RedoAvailable { get; set; }
        bool GameEnded { get; set; }
        UserInput GetUserInput();
        void ShowMessage(string message);
    }

    public static class StackWarsGUIFactory
    {
        private static IStackWarsGUI _gui = null;
        public static IStackWarsGUI GetGUI() => _gui ?? (_gui = new ConsoleGUI());
    }
}
