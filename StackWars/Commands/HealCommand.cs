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

        private int backup_health;

        public override void Execute(ILogger logger)
        {
            Unit unit = TargetArmy[TargetUnitIndex];
            backup_health = unit.CurrentHealth;
            unit.CurrentHealth = Math.Min(unit.CurrentHealth + Heal, unit.MaxHealth);
        }

        public override void Undo(ILogger logger) { TargetArmy[TargetUnitIndex].CurrentHealth = backup_health; }
    }
}
