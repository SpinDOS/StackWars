﻿namespace StackWars.Units.Interfaces
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
        public override string ToString() { return BaseUnit.ToString(); }
    }
}