using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Units;

namespace StackWars.GameEngine
{
    public sealed class FightStrategy3Vs3 : IFightStrategy
    {
        private static readonly Random Random = new Random();

        private static readonly Lazy<FightStrategy3Vs3> Instance =
            new Lazy<FightStrategy3Vs3>(() => new FightStrategy3Vs3());

        public static FightStrategy3Vs3 Singleton => Instance.Value;

        private FightStrategy3Vs3() { }

        public IEnumerable<(Army allies, Army enemies, int alliesIndex)> GetMagicUnits(Army army1, Army army2)
        {
            var result = new List<(Army allies, Army enemies, int alliesIndex)>();
            if (army1.Count > 3)
                result.AddRange(Enumerable.Range(3, army1.Count - 3)
                .Select(i => (army1, army2, i)));

            if (army2.Count > 3)
                result.AddRange(Enumerable.Range(3, army2.Count - 3)
                    .Select(i => (army2, army1, i)));

            return result.OrderBy(x => Random.Next());
        }

        public IEnumerable<(Army allies, int alliesIndex, Army enemies, int targetIndex)>
            GetMeleeAttacks(Army army1, Army army2)
        {
            if (army1.Count == 0 || army2.Count == 0)
                return Enumerable.Empty<(Army allies, int alliesIndex, Army enemies, int targetIndex)>();
            var result = new List<(Army allies, int alliesIndex, Army enemies, int targetIndex)>(6);
            HandleThreeMelees(army1, army2, result);
            HandleThreeMelees(army2, army1, result);
            return result.OrderBy(x => Random.Next());
        }

        public int? FindRandomUnitInRange(Army army, int sourceIndex, int range, Func<Unit, bool> selector)
        {
            int start = Math.Max(0, sourceIndex - range * 3), 
                end = Math.Min(army.Count - 1, sourceIndex + range * 3);
            if (start > end)
                return null;

            var possible = (from ind in Enumerable.Range(start, end - start + 1)
                            let diff = Math.Abs(ind - sourceIndex)
                            where diff % 3 + diff / 3 <= range && selector(army[ind])
                            select ind).ToList();
            if (possible.Count == 0)
                return null;
            return possible[Random.Next(possible.Count)];
        }

        public int? FindRandomEnemyUnitInRange(Army allies, int alliesIndex, Army enemies, int range,
            Func<Unit, bool> selector)
        {
            int searchRange = range - alliesIndex / 3 - 1;
            if (searchRange < 0)
                return null;
            return FindRandomUnitInRange(enemies, alliesIndex % 3, searchRange, selector);
        }

        private void HandleThreeMelees(Army army1, Army army2, 
            List<(Army allies, int alliesIndex, Army enemies, int targetIndex)> list)
        {
            int end1 = Math.Min(3, army1.Count);
            int end2 = Math.Min(3, army2.Count);
            for (int i = 0; i < end1; i++)
            {
                var attacker = army1[i];
                if (attacker.CurrentHealth <= 0)
                    continue;
                int target = Random.Next(end2);
                for (int j = 0; j < end2; j++)
                {
                    if (army2[target].CurrentHealth > 0)
                    {
                        list.Add((army1, i, army2, target));
                        break;
                    }
                    target = (target + 1) % end2;
                }
            }
        }
    }
}
