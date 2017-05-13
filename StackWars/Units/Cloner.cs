using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Commands;
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
            cloner.Range = this.Range;
            cloner.CloneRange = this.Range;
            return base.Clone(cloner);
        }

        public int Range { get; set; } = 3;

        public int RangeAttack { get; set; } = 5;

        public int CloneRange { get; set; } = int.MaxValue;
        public override bool CanBeAffectedBy(Type typeOfAbility)
        {
            if (typeOfAbility == typeof(BuffCommand) || typeOfAbility == typeof(CloneCommand))
                return false;
            return base.CanBeAffectedBy(typeOfAbility);
        }
    }
}
