using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units;

namespace StackWars
{
    [Cost(Cost = 20)]
    public class HeavyInfantry : SimpleUnit
    {
        public HeavyInfantry(ILogger logger) : base(logger)
        {
            MaxHealth = Health = 140;
            Attack = 25;
            Defense = 45;
        }

        public override IUnit Clone() => base.Clone(new HeavyInfantry(Logger));
    }
}
