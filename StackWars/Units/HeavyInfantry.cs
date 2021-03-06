﻿using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 25)]
    public sealed class HeavyInfantry : Unit, IBuffableUnit
    {
        public HeavyInfantry()
        {
            MaxHealth = CurrentHealth = 140;
            Attack = 25;
            Defense = 35;
        }

        public bool CanBeBuffed(BuffType type) { return true; }
    }
}