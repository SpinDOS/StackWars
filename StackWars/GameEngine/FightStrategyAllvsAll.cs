using System;
using System.Collections.Generic;
using System.Linq;
using StackWars.Units;

namespace StackWars.GameEngine
{
    public sealed class FightStrategyAllVsAll : IFightStrategy
    {
        private static readonly Random Random = new Random();

        private static readonly Lazy<FightStrategyAllVsAll> Instance =
            new Lazy<FightStrategyAllVsAll>(() => new FightStrategyAllVsAll());

        private FightStrategyAllVsAll() { }

        public static FightStrategyAllVsAll Singleton => Instance.Value;


        public IEnumerable<(Army allies, Army enemies, int alliesIndex)> GetMagicUnits(Army army1, Army army2)
        {
            if (army1.Count == army2.Count)
                return Enumerable.Empty<(Army allies, Army enemies, int alliesIndex)>();
            if (army1.Count < army2.Count)
                return Enumerable.Range(army1.Count, army2.Count - army1.Count).Select(i => (army2, army1, i));
            return Enumerable.Range(army2.Count, army1.Count - army2.Count).Select(i => (army1, army2, i));
        }

        public IEnumerable<(Army allies, int alliesIndex, Army enemies, int targetIndex)>
            GetMeleeAttacks(Army army1, Army army2)
        {
            var count = Math.Min(army1.Count, army2.Count);

            return Enumerable.Range(0, count)
                .Select(i => (army1, i, army2, Random.Next(count)))
                .Concat(
                    Enumerable.Range(0, count).Select(i => (army2, i, army1, Random.Next(count))))
                .OrderBy(x => Random.Next());
        }

        public int? FindRandomUnitInRange(Army army, int sourceIndex, int range, Func<Unit, bool> selector)
        {
            if (army.Count <= sourceIndex)
                return null;
            if (range == 0)
                if (selector(army[sourceIndex]))
                    return sourceIndex;
                else
                    return null;

            var rand = Random.Next(0, army.Count);
            for (var i = rand; i < army.Count; i++)
                if (selector(army[i]))
                    return i;
            for (var i = 0; i < rand; i++)
                if (selector(army[i]))
                    return i;
            return null;
        }

        public int? FindRandomEnemyUnitInRange(Army allies, int alliesIndex, Army enemies, int range,
            Func<Unit, bool> selector)
        {
            if (range < 1)
                return null;
            return FindRandomUnitInRange(enemies, 0, 1, selector);
        }
    }
}