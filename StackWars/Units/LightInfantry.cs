using System;
using System.Collections.Generic;
using System.Linq;
using StackWars.Commands;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 10)]
    public sealed class LightInfantry : Unit, IClonableUnit, IHealableUnit, IBufferUnit
    {
        public LightInfantry()
        {
            MaxHealth = CurrentHealth = 100;
            Attack = 30;
            Defense = 15;
        }

        public Unit Clone()
        {
            var clone = new LightInfantry();
            clone.Attack = this.Attack;
            clone.CurrentHealth = this.CurrentHealth;
            clone.MaxHealth = this.MaxHealth;
            clone.Defense = this.Defense;
            return clone;
        }
    }
}
