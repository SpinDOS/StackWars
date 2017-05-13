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

        static readonly List<Command> Commands = new List<Command>();
        static readonly Stack<Command> UndoStack = new Stack<Command>();
        static readonly Stack<Command> RedoStack = new Stack<Command>();

        public static void AddCommand(Command command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            Commands.Add(command);
        }
        public static void AddCommand(IEnumerable<Command> commands)
        {
            if (commands == null)
                throw new ArgumentNullException(nameof(commands));
            Commands.AddRange(commands);
        }

        public static void Flush()
        {
            foreach (var command in Commands)
            {
                if ((command.SourceUnitIndex.HasValue && command.SourceArmy[command.SourceUnitIndex.Value].CurrentHealth <= 0) || 
                    (command is SingleTargetCommand targetCommand && 
                        targetCommand.TargetArmy[targetCommand.TargetUnitIndex].CurrentHealth <= 0))
                    continue;
                command.Execute(Logger);
                UndoStack.Push(command);
            }
            Commands.Clear();
        }

        public static void EndTurn()
        {
            Flush();
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
