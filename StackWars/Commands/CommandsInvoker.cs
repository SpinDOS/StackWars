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
        public static Army Army1 { get; set; } = null;
        public static Army Army2 { get; set; } = null;
        public static ILogger Logger { get; set; } = null;

        static readonly List<Command> Commands = new List<Command>();
        static readonly Stack<Command> UndoStack = new Stack<Command>();

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

        public static void EndTurn()
        {
            foreach (var command in Commands)
            {
                command.Execute(Logger);
                UndoStack.Push(command);
            }
            Commands.Clear();

            foreach (var command in 
                new Command[] { Army1.CollectDead(), Army2.CollectDead() })
            {
                command.Execute(Logger);
                UndoStack.Push(command);
            }
            UndoStack.Push(null);
        }
    }
}
