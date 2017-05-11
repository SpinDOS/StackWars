using System.Collections.Generic;
using StackWars.Abilities;

namespace StackWars.Units.Interfaces
{
    public interface IUnit
    {
        int Attack { get; set; }
        int Defense { get; set; }
        int CurrentHealth { get; set; }
        int MaxHealth { get; set; }
        IUnit Clone();
        IEnumerable<Ability> MakeTurn();
    }
}