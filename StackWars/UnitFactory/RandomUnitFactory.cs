using System;
using System.Collections.Generic;
using System.Linq;
using StackWars.Logger;
using StackWars.Units;

namespace StackWars.UnitFactory
{
    public sealed class RandomUnitFactory : UnitFactory
    {
        readonly Dictionary<Type, int> _units;
        readonly Random _random = new Random();
        public RandomUnitFactory()
        {
            Type baseType = typeof(Unit);
            _units = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                let costAttribute = Attribute.GetCustomAttribute(type, typeof(CostAttribute)) as CostAttribute
                where costAttribute != null && baseType.IsAssignableFrom(type) 
                && !type.IsAbstract
                orderby costAttribute.Cost
                select new KeyValuePair<Type, int>(type, costAttribute.Cost)
            ).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
        

        public Unit GetUnit(ref int maxPossibleCost)
        {
            int argCost = maxPossibleCost;
            var possibleTypes = _units.TakeWhile(pair => pair.Value <= argCost).ToList();
            if (possibleTypes.Count == 0)
                return null;
            var selectedPair = possibleTypes[_random.Next(possibleTypes.Count)];
            maxPossibleCost -= selectedPair.Value;
            return Activator.CreateInstance(selectedPair.Key) as Unit;
//
//            double randomCost = _random.Next(100);
//
//            int n = possibleTypes.Count;
//            double average = possibleTypes.Average(pair => pair.Value);
//            double mid = 100.0 / n;
//            double sum = 0;
//
//            foreach (var pair in possibleTypes)
//            {
//                sum += mid + (pair.Value - average);
//                if (sum < randomCost)
//                    continue;
//                maxPossibleCost -= pair.Value;
//                return pair.Key.Clone();
//            }
//            KeyValuePair<Unit, int> last = possibleTypes.Last();
//            maxPossibleCost -= last.Value;
//            return last.Key.Clone();
        }
    }
}
