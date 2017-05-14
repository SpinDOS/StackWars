using System;
using System.Reflection;
using SpecialUnits;

namespace StackWars.Units
{
    [Cost(Cost = 65)]
    public sealed class GulyayGorodUnit : Unit
    {
        private readonly GulyayGorod _unit = new GulyayGorod(180, 0, 25);

        public override int MaxHealth
        {
            get => _unit.GetHealth();
            set => throw new NotSupportedException();
        }

        public override int CurrentHealth
        {
            get => _unit.GetCurrentHealth();
            set
            {
                if (value < _unit.GetCurrentHealth())
                    _unit.TakeDamage(_unit.GetDefence() + _unit.GetCurrentHealth() - value);
                else // this type does not support clone or health restore, so we use reflection
                    _unit.GetType()
                        .GetField("_currentHealth",
                            BindingFlags.NonPublic |
                            BindingFlags.Instance)
                        .SetValue(_unit, value);
            }
        }

        public override int Attack
        {
            get => 0;
            set => throw new NotSupportedException();
        }

        public override int Defense { get; set; } = 33;
    }
}