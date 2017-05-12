using System;
using System.Collections.Generic;
using System.Linq;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 10)]
    public sealed class LigthInfantry : Unit
    {
        public LigthInfantry()
        {
            MaxHealth = CurrentHealth = 100;
            Attack = 30;
            Defense = 15;
        }

        public override Unit Clone() => base.Clone(new LigthInfantry());
    }
}
