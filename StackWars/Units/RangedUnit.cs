using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Abilities;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    public abstract class RangedUnit : SimpleUnit, IRangedUnit
    {
        public virtual int Range { get; set; }
        public virtual int RangeAttack { get; set; }

        public override IEnumerable<Ability> MakeTurn()
        {
            yield return new RangeAttack
            {
                Range = this.Range,
                Attack = this.RangeAttack
            };
        }
    }
}
