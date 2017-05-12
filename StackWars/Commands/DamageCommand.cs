using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.Commands
{
    public class DamageCommand : SingleTargetCommand
    {
        public DamageCommand(Army source, int sourceIndex, Army target, int targetIndex, int damage)
            : base(source, sourceIndex, target, targetIndex)
        {
            Damage = damage;
        }
        public int Damage { get; }
        public override void Execute(ILogger logger)
        {
            logger?.Log($"{SourceArmy.Name}'s unit {SourceUnitIndex} attacks " +
                        $"{TargetArmy.Name}'s unit {TargetUnitIndex} with {Damage} damage");
            TargetArmy[TargetUnitIndex].CurrentHealth -= Damage;
        }

        public override void Undo(ILogger logger)
        {
            TargetArmy[TargetUnitIndex].CurrentHealth += Damage;
        }
    }
}
