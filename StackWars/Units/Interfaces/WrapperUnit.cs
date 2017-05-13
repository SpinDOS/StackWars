using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars.Units.Interfaces
{
    public abstract class WrapperUnit : Unit
    {
        protected WrapperUnit(Unit baseUnit) { BaseUnit = baseUnit; }
        public override int CurrentHealth
        {
            get => BaseUnit.CurrentHealth;
            set => BaseUnit.CurrentHealth = value;
        }
        public override int MaxHealth
        {
            get => BaseUnit.MaxHealth;
            set => BaseUnit.MaxHealth = value;
        }
        public override int Attack
        {
            get => BaseUnit.Attack;
            set => BaseUnit.Attack = value;
        }
        public override int Defense
        {
            get => BaseUnit.Defense;
            set => BaseUnit.Defense = value;
        }

        public Unit BaseUnit { get; set; }

        protected Unit Clone(BuffUnit pretendent)
        {
            pretendent.BaseUnit = BaseUnit.Clone();
            return pretendent;
        }

        public override bool CanBeAffectedBy(Type typeOfAbility) => BaseUnit.CanBeAffectedBy(typeOfAbility);
        public override string ToString() => BaseUnit.ToString();
    }
}
