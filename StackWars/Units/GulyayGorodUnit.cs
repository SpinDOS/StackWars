using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecialUnits;

namespace StackWars.Units
{
    [Cost(Cost = 15)]
    public sealed class GulyayGorodUnit : Unit
    {
        private readonly GulyayGorod _unit = new GulyayGorod(300, 0, 15);
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
                    System.Reflection.BindingFlags.NonPublic | 
                    System.Reflection.BindingFlags.Instance)
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
