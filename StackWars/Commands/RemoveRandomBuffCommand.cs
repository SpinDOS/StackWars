using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units;
using StackWars.Units.Interfaces;

namespace StackWars.Commands
{
    public sealed class RemoveRandomBuffCommand : SingleTargetCommand
    {
        public RemoveRandomBuffCommand(Army source, int sourceIndex, Army target, int targetIndex, int buffNumber)
            : base(source, sourceIndex, target, targetIndex)
        {
            BuffNumber = buffNumber;
        }

        private BuffUnit backup = null;

        public int BuffNumber { get; }

        public override void Execute(ILogger logger)
        {
            Unit unit = TargetArmy[TargetUnitIndex];
            if (BuffNumber == 0)
            {
                backup = unit as BuffUnit;
                TargetArmy[TargetUnitIndex] = backup.BaseUnit;
                return;
            }
            for (int i = 0; i < BuffNumber - 1; i++)
                unit = (unit as BuffUnit).BaseUnit;
            BuffUnit buffUnit = unit as BuffUnit;
            backup = buffUnit.BaseUnit as BuffUnit;
            buffUnit.BaseUnit = backup.BaseUnit;
        }

        public override void Undo(ILogger logger)
        {
            if (BuffNumber == 0)
            {
                TargetArmy[TargetUnitIndex] = backup;
                return;
            }
            BuffUnit buffUnit = TargetArmy[TargetUnitIndex] as BuffUnit;
            for (int i = 0; i < BuffNumber - 1; i++)
                buffUnit = buffUnit.BaseUnit as BuffUnit;
            buffUnit.BaseUnit = backup;
        }
    }
}
