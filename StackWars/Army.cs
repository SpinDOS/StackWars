using System;
using System.Collections.Generic;
using System.Text;
using StackWars.Commands;
using StackWars.Units;

namespace StackWars
{
    public sealed class Army : List<Unit>
    {
        private static readonly Random Random = new Random();

        public Army(string name, UnitFactory.UnitFactory fabric, int armyCost)
        {
            if (fabric == null)
                throw new ArgumentNullException(nameof(fabric));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));
            Name = name;
            while (armyCost > 0)
            {
                var unit = fabric.GetUnit(ref armyCost);
                if (unit == null)
                    break;
                Insert(Random.Next(Count + 1), unit);
            }
        }

        public string Name { get; }

        public CollectDeadCommand CollectDead()
        {
            var result = new List<KeyValuePair<int, Unit>>();
            for (var i = 0; i < Count; i++)
            {
                var unit = this[i];
                if (unit.CurrentHealth <= 0)
                    result.Add(new KeyValuePair<int, Unit>(i, unit));
            }
            return new CollectDeadCommand(this, result);
        }

        public override string ToString()
        {
            if (Count == 0)
                return $"{Name} is empty";
            var result = new StringBuilder();
            result.AppendLine($"{Name}: {Count} units ");
            foreach (var unit in this)
                result.AppendLine(unit.ToString());
            return result.ToString();
        }
    }
}