using System.Collections.Generic;
using StackWars.Abilities;

namespace StackWars.Units
{
    public interface IAbilitiable : IUnit
    {
        IEnumerable<Ability> MakeTurn();
    }
}