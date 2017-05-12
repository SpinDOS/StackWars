using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    public abstract class BuffUnit : Unit
    {
        static readonly Random Random = new Random();
        protected BuffUnit(Unit baseUnit) { BaseUnit = baseUnit; }
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
        public int BuffCount => BaseUnit is BuffUnit buffUnit? buffUnit.BuffCount + 1 : 1;

        protected Unit Clone(BuffUnit pretendent)
        {
            pretendent.BaseUnit = BaseUnit.Clone();
            return base.Clone(pretendent);
        }
    }
}
