using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 60)]
    public sealed class ClonerUnit : Unit, IRangedUnit, IClonerUnit
    {
        public ClonerUnit()
        {
            Attack = 10;
            Defense = 15;
            MaxHealth = CurrentHealth = 40;
        }

        public int CloneChance { get; set; } = 5;

        public int CloneRange { get; set; } = 3;

        public int Range { get; set; } = 2;

        public int RangeAttack { get; set; } = 25;
    }
}