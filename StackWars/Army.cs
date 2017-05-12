using System;
using System.Collections.Generic;
using System.Text;
using StackWars.Commands;
using StackWars.UnitFactory;
using StackWars.Units.Interfaces;

namespace StackWars
{
    public sealed class Army : List<IUnit>
    {
        public Army(string name, IUnitFactory fabric, int armyCost)
        {
            if (fabric == null)
                throw new ArgumentNullException(nameof(fabric));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));
            Name = name;
            while (armyCost > 0)
            {
                IUnit unit = fabric.GetUnit(ref armyCost);
                if (unit == null)
                    break;
                this.Add(unit);
            }
        }
        public string Name { get; }

        public CollectDeadCommand CollectDead()
        {
            var result = new List<KeyValuePair<int, IUnit>>();
            for (int i = 0; i < this.Count; i++)
            {
                IUnit unit = this[i];
                if (unit.CurrentHealth <= 0)
                    result.Add(new KeyValuePair<int, IUnit>(i, unit.Clone()));
            }
            return new CollectDeadCommand(this, result);
        }

        public override string ToString()
        {
            if (this.Count == 0)
                return $"{Name} is empty";
            StringBuilder result = new StringBuilder();
            result.AppendLine($"{Name}: {this.Count} units ");
            foreach (var unit in this)
                result.AppendLine(unit.ToString());
            return result.ToString();
        }
    }
}
