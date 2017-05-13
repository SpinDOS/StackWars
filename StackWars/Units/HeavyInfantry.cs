using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 20)]
    public sealed class HeavyInfantry : Unit, IBuffableUnit
    {
        public HeavyInfantry()
        {
            MaxHealth = CurrentHealth = 140;
            Attack = 25;
            Defense = 45;
        }

        public bool CanBeBuffed(BuffType type) { return true; }
    }
}