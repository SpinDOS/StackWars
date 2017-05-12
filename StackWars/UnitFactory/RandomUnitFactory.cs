using System;
using System.Collections.Generic;
using System.Linq;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.UnitFactory
{
    public sealed class RandomUnitFactory : IUnitFactory
    {
        readonly Dictionary<IUnit, int> _units;
        readonly Random _random = new Random();
        public RandomUnitFactory()
        {
            Type baseType = typeof(IUnit);
            var derivedTypes = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                               from type in assembly.GetTypes()
                               let costAttribute = Attribute.GetCustomAttribute(type, typeof(CostAttribute)) as CostAttribute
                               where costAttribute != null && baseType.IsAssignableFrom(type) 
                                    && !type.IsAbstract
                               orderby costAttribute.Cost
                               select new { type, costAttribute.Cost };
            _units = derivedTypes.ToDictionary(pair => Activator.CreateInstance(pair.type) as IUnit, 
                                                pair => pair.Cost);
        }
        

        public IUnit GetUnit(ref int maxPossibleCost)
        {
            int argCost = maxPossibleCost;
            var possibleTypes = _units.TakeWhile(pair => pair.Value <= argCost).ToList();
            if (possibleTypes.Count == 0)
                return null;

            double randomCost = _random.Next(100);

            double average = possibleTypes.Average(pair => pair.Value);
            double mid = 100.0 / possibleTypes.Count;
            double sum = 0;

            foreach (var pair in possibleTypes)
            {
                sum += mid + (pair.Value - average);
                if (sum < randomCost)
                    continue;
                maxPossibleCost -= pair.Value;
                return pair.Key.Clone();
            }
            KeyValuePair<IUnit, int> last = possibleTypes.Last();
            maxPossibleCost -= last.Value;
            return last.Key.Clone();
        }
    }
}
