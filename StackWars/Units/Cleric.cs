﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Commands;
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
            cleric.Range = this.Range;
            cleric.HealRange = this.HealRange;
            return base.Clone(cleric);
        }

        public int HealRange { get; set; } = int.MaxValue;

        public int Heal { get; set; } = 10;

        public int Range { get; set; } = 5;

        public int RangeAttack { get; set; } = 10;
        public override bool CanBeAffectedBy(Type typeOfAbility)
        {
            if (typeOfAbility == typeof(BuffCommand))
                return false;
            return base.CanBeAffectedBy(typeOfAbility);
        }
    }
}
