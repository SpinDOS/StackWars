using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    public abstract class RangedUnit : Unit, IRangedUnit
    {
        public virtual int Range { get; set; }
        public virtual int RangeAttack { get; set; }
    }
}
