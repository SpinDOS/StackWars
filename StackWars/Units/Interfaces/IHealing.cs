using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars.Units.Interfaces
{
    public interface IHealerUnit
    {
        int HealRange { get; set; }
        int Heal { get; set; }
    }

    public interface IHealableUnit
    {
        
    }
}
