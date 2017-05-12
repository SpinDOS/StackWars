using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;

namespace StackWars.Commands
{
    public abstract class Command
    {
        protected Command(Army source, int? sourceUnitIndex)
        {
            SourceArmy = source;
            SourceUnitIndex = sourceUnitIndex;
        }
        public Army SourceArmy { get; }
        public int? SourceUnitIndex { get; }
        public abstract void Execute(ILogger logger);
        public abstract void Undo(ILogger logger);
    }
}
