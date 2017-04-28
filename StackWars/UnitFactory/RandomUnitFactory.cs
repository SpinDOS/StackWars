using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units;

namespace StackWars
{
    public sealed class RandomUnitFactory : IUnitFactory
    {
        Dictionary<IUnit, int> units;
        Random random = new Random();
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
            ILogger logger = null;
            units = derivedTypes.ToDictionary(pair => Activator.CreateInstance(pair.type, logger) as IUnit, 
                                                pair => pair.Cost);
        }
        

        public IUnit GetUnit(ref int maxPossibleCost)
        {
            int argCost = maxPossibleCost;
            var possibleTypes = units.TakeWhile(pair => pair.Value <= argCost);
            if (!possibleTypes.Any())
                return null;
            double sum = possibleTypes.Sum(pair => 1.0 / pair.Value);
            double randomCost = random.NextDouble() * sum;
            sum = 0;
            foreach (var pair in possibleTypes)
            {
                sum += 1.0 / pair.Value;
                if (sum < randomCost)
                    continue;
                maxPossibleCost -= pair.Value;
                return pair.Key.Clone();
            }
            return null;
        }
    }
}
