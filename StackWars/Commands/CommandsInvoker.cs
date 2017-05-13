using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;

namespace StackWars.Commands
{
    public static class CommandsInvoker
    {
        public static ILogger Logger { get; set; } = null;

        static readonly Stack<Command> UndoStack = new Stack<Command>();
        static readonly Stack<Command> RedoStack = new Stack<Command>();

        public static void Execute(Command command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            command.Execute(Logger);
            UndoStack.Push(command);
        }

        public static void EndTurn()
        {
            UndoStack.Push(null);
            RedoStack.Clear();
        }

        public static void Undo()
        {
            if (UndoStack.Count == 0)
                return;
            RedoStack.Push(UndoStack.Pop());
            while (UndoStack.Count > 0)
            {
                Command command = UndoStack.Pop();
                if (command != null)
                    command.Undo(Logger);
                else
                    break;
                RedoStack.Push(command);
            }
        }
        public static void Redo()
        {
            while (RedoStack.Count > 0)
            {
                Command command = RedoStack.Pop();
                UndoStack.Push(command);
                if (command != null)
                    command.Execute(null);
                else
                    break;
            }
        }
    }
}
