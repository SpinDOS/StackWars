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
        public BuffUnit(Unit baseUnit) { BaseUnit = baseUnit; }
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
            return base.Clone(pretendent);
        }

        public static Unit RemoveRandomBuff(BuffUnit buffUnit)
        {
            buffUnit = buffUnit.Clone() as BuffUnit;
            if (!(buffUnit.BaseUnit is BuffUnit) || Random.Next(2) == 0)
                return buffUnit.BaseUnit;
            BuffUnit current = buffUnit;
            while (true)
            {
                BuffUnit baseUnit = current.BaseUnit as BuffUnit;
                if (!(baseUnit.BaseUnit is BuffUnit) || Random.Next(2) == 0)
                {
                    current.BaseUnit = baseUnit.BaseUnit;
                    return buffUnit;
                }
                current = baseUnit;
            }
        }
    }
}
