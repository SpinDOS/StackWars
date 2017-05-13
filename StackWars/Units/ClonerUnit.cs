using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 60)]
    public sealed class ClonerUnit : Unit, IRangedUnit, IClonerUnit
    {
        public ClonerUnit()
        {
            Attack = 5;
            Defense = 15;
            MaxHealth = CurrentHealth = 30;
        }

        public int CloneRange { get; set; } = 5;

        public int Range { get; set; } = 3;

        public int RangeAttack { get; set; } = 5;
    }
}