using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 40)]
    public sealed class ArcherUnit : RangedUnit
    {
        public ArcherUnit()
        {
            MaxHealth = CurrentHealth = 60;
            Attack = 15;
            Defense = 1;
            Range = 10;
            RangeAttack = 15;
        }
        public override Unit Clone()
        {
            var archer = new ArcherUnit
            {
                Range = this.Range,
                RangeAttack = this.Range
            };
            return base.Clone(archer);
        }
    }
}
