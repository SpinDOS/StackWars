using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units;

namespace StackWars.Commands
{
    public sealed class RemoveRandomBuffCommand : SingleTargetCommand
    {
        public RemoveRandomBuffCommand(Army source, int sourceIndex, Army target, int targetIndex)
            : base(source, sourceIndex, target, targetIndex) { }

        private Unit backup = null;
        private Unit newUnit = null;

        public override void Execute(ILogger logger)
        {
            if (backup == null)
            {
                backup = TargetArmy[TargetUnitIndex].Clone();
                newUnit = BuffUnit.RemoveRandomBuff(backup as BuffUnit);
            }
            TargetArmy[TargetUnitIndex] = newUnit.Clone();
        }

        public override void Undo(ILogger logger)
        {
            TargetArmy[TargetUnitIndex] = backup.Clone();
        }
    }
}
