using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units;

namespace StackWars
{
    [Cost(Cost = 10)]
    public class LigthInfantry : SimpleUnit
    {
        public LigthInfantry(ILogger logger) : base(logger)
        {
            MaxHealth = Health = 100;
            Attack = 30;
            Defense = 15;
        }

        public override IUnit Clone() => base.Clone(new LigthInfantry(Logger));
    }
}
