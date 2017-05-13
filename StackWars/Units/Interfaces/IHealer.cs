using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars.Units.Interfaces
{
    interface IHealer
    {
        int HealRange { get; set; }
        int Heal { get; set; }
    }
}
