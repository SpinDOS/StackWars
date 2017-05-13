using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Commands;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 40)]
    public sealed class ArcherUnit : Unit, IRangedUnit, IClonableUnit, IHealableUnit
    {
        public ArcherUnit()
        {
            MaxHealth = CurrentHealth = 60;
            Attack = 15;
            Defense = 10;
        }

        public int Range { get; set; } = 10;
        public int RangeAttack { get; set; } = 15;

        public Unit Clone()
        {
            var clone = new ArcherUnit
            {
                Attack = this.Attack,
                CurrentHealth = this.CurrentHealth,
                MaxHealth = this.MaxHealth,
                Defense = this.Defense,
                RangeAttack = this.RangeAttack,
                Range = this.Range
            };
            return clone;
        }
    }
}
