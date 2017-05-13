using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 30)]
    public sealed class Cleric : Unit, IRangedUnit, IHealer
    {
        public Cleric()
        {
            Attack = 10;
            Defense = 5;
            MaxHealth = CurrentHealth = 100;
        }

        public override Unit Clone()
        {
            Cleric cleric = new Cleric();
            cleric.Heal = this.Heal;
            cleric.RangeAttack = this.RangeAttack;
            (cleric as IRangedUnit).Range = (this as IRangedUnit).Range;
            (cleric as IHealer).Range = (this as IHealer).Range;
            return base.Clone(cleric);
        }

        int IHealer.Range { get; set; } = int.MaxValue;

        public int Heal { get; set; } = 10;

        int IRangedUnit.Range { get; set; } = 5;

        public int RangeAttack { get; set; } = 10;
    }
}
