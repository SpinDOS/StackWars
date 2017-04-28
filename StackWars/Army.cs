using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Units;

namespace StackWars
{
    public sealed class Army
    {
        public List<IUnit> Units = new List<IUnit>();
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
                Units.Add(unit);
            }
        }
        public void CollectDeads()
        {
            for (int i = 0; i < Units.Count; i++)
            {
                if (Units[i].Health <= 0)
                    Units.RemoveAt(i);
            }
        }
        public string Name { get; }

        public override string ToString()
        {
            if (Units.Count == 0)
                return $"{Name} is empty";
            StringBuilder result = new StringBuilder();
            result.AppendLine($"{Name}: {Units.Count} units ");
            foreach (var unit in Units)
                result.AppendLine(unit.ToString());
            return result.ToString();
        }
    }
}
