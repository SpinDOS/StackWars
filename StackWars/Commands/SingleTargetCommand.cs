using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars.Commands
{
    public abstract class SingleTargetCommand : Command
    {
        protected SingleTargetCommand(Army source, int sourceIndex, Army target, int targetIndex)
            : base(source, sourceIndex)
        {
            TargetArmy = target;
            TargetUnitIndex = targetIndex;
        }
        public Army TargetArmy { get; }
        public int TargetUnitIndex { get; }
    }
}
