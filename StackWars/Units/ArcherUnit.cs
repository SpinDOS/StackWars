using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Abilities;

namespace StackWars.Units
{
    [Cost(Cost = 40)]
    class ArcherUnit : SimpleUnit, IRangedUnit
    {
        public ArcherUnit(ILogger logger) : base(logger)
        {
            MaxHealth = Health = 60;
            Attack = 10;
            Defense = 1;
        }

        public int Range { get; set; } = 8;
        public int RangeAttack { get; set; } = 15;
        public override IUnit Clone()
        {
            var archer = new ArcherUnit(Logger);
            archer.Range = this.Range;
            archer.RangeAttack = this.Range;
            return base.Clone(archer);
        }

        public IEnumerable<Ability> MakeTurn()
        {
            var command = new Ability();
            return new List<Ability>() { command };
        }
    }
}
