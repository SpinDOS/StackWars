using System;
using System.Collections.Generic;
using System.Linq;
using StackWars.Units;

namespace StackWars.GameEngine
{
    public sealed class FightStrategy1Vs1 : IFightStrategy
    {
        private static readonly Random Random = new Random();

        private static readonly Lazy<FightStrategy1Vs1> Instance =
            new Lazy<FightStrategy1Vs1>(() => new FightStrategy1Vs1());

        private FightStrategy1Vs1() { }

        public static FightStrategy1Vs1 Singleton => Instance.Value;

        public IEnumerable<(Army allies, Army enemies, int alliesIndex)> GetMagicUnits(Army army1, Army army2)
        {
            var result = new List<(Army allies, Army enemies, int alliesIndex)>();
            if (army1.Count > 1)
                result.AddRange(Enumerable.Range(1, army1.Count - 1)
                    .Select(i => (army1, army2, i)));

            if (army2.Count > 1)
                result.AddRange(Enumerable.Range(1, army2.Count - 1)
                    .Select(i => (army2, army1, i)));

            return result.OrderBy(x => Random.Next());
        }

        public IEnumerable<(Army allies, int alliesIndex, Army enemies, int targetIndex)>
            GetMeleeAttacks(Army army1, Army army2)
        {
            var result = new List<(Army allies, int alliesIndex, Army enemies, int targetIndex)>();
            if (army1[0].CurrentHealth > 0 && army2[0].CurrentHealth > 0)
            {
                result.Add((army1, 0, army2, 0));
                result.Add((army2, 0, army1, 0));
            }
            ;
            return result.OrderBy(x => Random.Next());
        }

        public int? FindRandomUnitInRange(Army army, int sourceIndex, int range, Func<Unit, bool> selector)
        {
            int start = Math.Max(0, sourceIndex - range), end = Math.Min(army.Count, sourceIndex + range + 1);
            if (start >= end)
                return null;

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
            var searchRange = range - alliesIndex - 1;
            if (searchRange < 0)
                return null;
            return FindRandomUnitInRange(enemies, 0, searchRange, selector);
        }
    }
}