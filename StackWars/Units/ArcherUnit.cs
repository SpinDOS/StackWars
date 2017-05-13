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
    public sealed class ArcherUnit : Unit, IRangedUnit, IClonableUnit
    {
        public ArcherUnit()
        {
            MaxHealth = CurrentHealth = 60;
            Attack = 15;
            Defense = 10;
            Range = 10;
            RangeAttack = 15;
        }

        public int Range { get; set; }
        public int RangeAttack { get; set; }

        public Unit Clone()
        {
            var clone = new ArcherUnit();
            clone.Attack = this.Attack;
            clone.CurrentHealth = this.CurrentHealth;
            clone.MaxHealth = this.MaxHealth;
            clone.Defense = this.Defense;
            clone.RangeAttack = this.RangeAttack;
            clone.Range = this.Range;
            return clone;
        }
    }
}
