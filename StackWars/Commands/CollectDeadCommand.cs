﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.Commands
{
    public sealed class CollectDeadCommand : Command
    {
        public CollectDeadCommand(Army army, IEnumerable<KeyValuePair<int, IUnit>> dead)
            : base(army, null)
        {
            Dead = dead.OrderBy(pair => pair.Key).ToArray();
        }
        public IEnumerable<KeyValuePair<int, IUnit>> Dead { get; }
        public override void Execute(ILogger logger)
        {
            logger?.Log($"Collecting {Dead.Count()} deads from {SourceArmy.Name}");
            foreach (var pair in Dead.Reverse())
                SourceArmy.RemoveAt(pair.Key);
        }

        public override void Undo(ILogger logger)
        {
            foreach (var pair in Dead)
                SourceArmy.Insert(pair.Key, pair.Value.Clone());
        }
    }
}