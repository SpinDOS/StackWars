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
            set => _unit.TakeDamage(_unit.GetDefence() + _unit.GetCurrentHealth() - value);
        }
        public override int Attack
        {
            get => 0;
            set => throw new NotSupportedException();
        }

        public override int Defense { get; set; } = 33;
    }
}
