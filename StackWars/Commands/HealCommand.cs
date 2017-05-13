using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units;

namespace StackWars.Commands
{
    public class HealCommand : SingleTargetCommand
    {
        public HealCommand(Army source, int sourceIndex, Army target, int targetIndex, int heal)
            : base(source, sourceIndex, target, targetIndex)
        {
            Heal = heal;
        }
        public int Heal { get; set; }

        public override void Execute(ILogger logger)
        {
            TargetArmy[TargetUnitIndex].CurrentHealth += Heal;
        }

        public override void Undo(ILogger logger) { TargetArmy[TargetUnitIndex].CurrentHealth -= Heal; }
    }
}
