using System;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    public sealed class ProxyClonerUnit : WrapperUnit, IClonerUnit, IRangedUnit
    {
        public ProxyClonerUnit(ClonerUnit baseUnit, ILogger logger)
            : base(baseUnit)
        {
            Logger = logger;
        }

        public ILogger Logger { get; }

        public override int CurrentHealth
        {
            set
            {
                if (BaseUnit.CurrentHealth == value)
                    return;
                if (value <= 0)
                    Logger?.Log($"{DateTime.Now}: Cloner is dead. " +
                                $"{this} health is changed from {CurrentHealth} to {value}");
                BaseUnit.CurrentHealth = value;
            }
        }

        public int CloneChance
        {
            get => (BaseUnit as IClonerUnit).CloneChance;
            set => (BaseUnit as IClonerUnit).CloneChance = value;
        }

        public int CloneRange
        {
            get => (BaseUnit as IClonerUnit).CloneRange;
            set => (BaseUnit as IClonerUnit).CloneRange = value;
        }

        public int Range
        {
            get => (BaseUnit as IRangedUnit).Range;
            set => (BaseUnit as IRangedUnit).Range = value;
        }

        public int RangeAttack
        {
            get => (BaseUnit as IRangedUnit).RangeAttack;
            set => (BaseUnit as IRangedUnit).RangeAttack = value;
        }
    }
}