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
    public sealed class RemoveBuffCommand : SingleTargetCommand
    {
        public RemoveBuffCommand(Army source, int? sourceIndex, Army target, int targetIndex, int buffNumber)
            : base(source, sourceIndex, target, targetIndex) => BuffNumber = buffNumber;

        public int BuffNumber { get; }

        private BuffedUnit _backup = null;

        public override void Execute(ILogger logger)
        {
            Unit unit = TargetArmy[TargetUnitIndex];
            if (BuffNumber == 0)
            {
                _backup = unit as BuffedUnit;
                TargetArmy[TargetUnitIndex] = _backup.BaseUnit;
                return;
            }
            for (int i = 0; i < BuffNumber - 1; i++)
                unit = (unit as BuffedUnit).BaseUnit;
            BuffedUnit buffedUnit = unit as BuffedUnit;
            _backup = buffedUnit.BaseUnit as BuffedUnit;
            buffedUnit.BaseUnit = _backup.BaseUnit;
        }

        public override void Undo(ILogger logger)
        {
            if (BuffNumber == 0)
            {
                TargetArmy[TargetUnitIndex] = _backup;
                _backup = null;
                return;
            }
            BuffedUnit buffedUnit = TargetArmy[TargetUnitIndex] as BuffedUnit;
            for (int i = 0; i < BuffNumber - 1; i++)
                buffedUnit = buffedUnit.BaseUnit as BuffedUnit;
            buffedUnit.BaseUnit = _backup;
            _backup = null;
        }
    }
}
