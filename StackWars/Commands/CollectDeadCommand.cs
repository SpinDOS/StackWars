using System.Collections.Generic;
using System.Linq;
using StackWars.Logger;
using StackWars.Units;

namespace StackWars.Commands
{
    public sealed class CollectDeadCommand : Command
    {
        public CollectDeadCommand(Army army, IEnumerable<KeyValuePair<int, Unit>> dead)
            : base(army, null)
        {
            Dead = dead.OrderBy(pair => pair.Key).ToArray();
        }

        public IEnumerable<KeyValuePair<int, Unit>> Dead { get; }

        public override void Execute(ILogger logger)
        {
            logger?.Log($"Collecting {Dead.Count()} deads from {SourceArmy.Name}");
            foreach (var pair in Dead.Reverse())
                SourceArmy.RemoveAt(pair.Key);
        }

        public override void Undo(ILogger logger)
        {
            foreach (var pair in Dead)
                SourceArmy.Insert(pair.Key, pair.Value);
        }
    }
}