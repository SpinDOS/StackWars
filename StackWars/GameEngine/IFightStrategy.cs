using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Units;

namespace StackWars.GameEngine
{
    public interface IFightStrategy
    {
        IEnumerable<(Army allies, Army enemies, int alliesIndex)> GetMagic(Army army1, Army army2);

        IEnumerable<(Army allies, int alliesIndex, Army enemies, int targetIndex, int damage)> 
            GetMelees(Army army1, Army army2);

        int? FindRandomUnitInRange(Army army, int sourceIndex, int range, Func<Unit, bool> selector);
        int? FindRandomEnemyUnitInRange(Army allies, int alliesIndex, Army enemies, int range, Func<Unit, bool> selector);
    }
}
