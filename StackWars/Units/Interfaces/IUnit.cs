using System.Collections.Generic;
using StackWars.Abilities;

namespace StackWars.Units
{
    public interface IUnit
    {
        int Attack { get; set; }
        int Defense { get; set; }
        int Health { get; set; }
        int MaxHealth { get; set; }
        IUnit Clone();
    }
}