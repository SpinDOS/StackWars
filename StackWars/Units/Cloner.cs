using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 60)]
    public sealed class Cloner : Unit, IRangedUnit, IClonerUnit
    {
        public Cloner()
        {
            Attack = 5;
            Defense = 15;
            MaxHealth = CurrentHealth = 30;
        }
        public override Unit Clone()
        {
            Cloner cloner = new Cloner();
            cloner.RangeAttack = this.RangeAttack;
            (cloner as IRangedUnit).Range = (this as IRangedUnit).Range;
            (cloner as IClonerUnit).Range = (this as IClonerUnit).Range;
            return base.Clone(cloner);
        }

        int IRangedUnit.Range { get; set; } = 3;

        public int RangeAttack { get; set; } = 5;

        int IClonerUnit.Range { get; set; } = int.MaxValue;
    }
}
