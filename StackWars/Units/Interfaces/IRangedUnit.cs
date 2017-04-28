using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars.Units
{
    interface IRangedUnit : IAbilitiable
    {
        int Range { get; set; }
        int RangeAttack { get; set; }
    }
}
