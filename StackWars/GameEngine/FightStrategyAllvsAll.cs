using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Units;

namespace StackWars.GameEngine
{
    public sealed class FightStrategyAllvsAll : IFightStrategy
    {
        private static readonly Random Random = new Random();

        private static readonly Lazy<FightStrategyAllvsAll> Instance =
            new Lazy<FightStrategyAllvsAll>(() => new FightStrategyAllvsAll());

        public static FightStrategyAllvsAll Singleton => Instance.Value;

        private FightStrategyAllvsAll() { }


        public IEnumerable<(Army allies, Army enemies, int alliesIndex)> GetMagic(Army army1, Army army2) { throw new NotImplementedException(); }
        public IEnumerable<(Army allies, int alliesIndex, Army enemies, int targetIndex, int damage)> GetMelees(Army army1, Army army2) { throw new NotImplementedException(); }
        public int? FindRandomUnitInRange(Army army, int sourceIndex, int range, Func<Unit, bool> selector) { throw new NotImplementedException(); }
        public int? FindRandomEnemyUnitInRange(Army allies, int alliesIndex, Army enemies, int range, Func<Unit, bool> selector) { throw new NotImplementedException(); }
    }
}
