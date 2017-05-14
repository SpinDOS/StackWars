using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Units;

namespace StackWars.GameEngine
{
    public sealed class FightStrategy1Vs1 : IFightStrategy
    {
        private static readonly Random Random = new Random();
        private static readonly Lazy<FightStrategy1Vs1> Instance = 
            new Lazy<FightStrategy1Vs1>(() => new FightStrategy1Vs1());

        public static FightStrategy1Vs1 Singleton => Instance.Value;

        private FightStrategy1Vs1() { }

        public IEnumerable<(Army allies, Army enemies, int alliesIndex)> GetMagic(Army army1, Army army2)
        {
            return Enumerable.Range(1, army1.Count - 1)
                .Select(i => (army1, army2, i))
                .Concat(Enumerable.Range(1, army2.Count - 1).Select(i => (army2, army1, i)))
                .OrderBy(x => Random.Next());
        }

        public IEnumerable<(Army allies, int alliesIndex, Army enemies, int targetIndex, int damage)>
            GetMelees(Army army1, Army army2)
        {
            var temp = new List<(Army allies, int alliesIndex, Army enemies, int targetIndex, int damage)>
            {
                (army1, 0, army2, 0, army1[0].Attack),
                (army2, 0, army1, 0, army2[0].Attack)
            };
            return temp.OrderBy(x => Random.Next());
        }

        public int? FindRandomUnitInRange(Army army, int sourceIndex, int range, Func<Unit, bool> selector)
        {
            if (army.Count == 0)
                return null;
            int start = Math.Max(0, sourceIndex - range), end = Math.Min(army.Count, sourceIndex + range + 1);
            if (end < 0)
                end = army.Count; // fix overflow error
            var rand = Random.Next(start, end);
            for (var i = rand; i < end; i++)
                if (selector(army[i]))
                    return i;
            for (var i = start; i < rand; i++)
                if (selector(army[i]))
                    return i;
            return null;
        }

        public int? FindRandomEnemyUnitInRange(Army allies, int alliesIndex, Army enemies, int range,
            Func<Unit, bool> selector)
        {
            int searchRange = range - alliesIndex - 1;
            if (searchRange < 0)
                return null;
            return FindRandomUnitInRange(enemies, 0, searchRange, selector);
        }
    }
}

