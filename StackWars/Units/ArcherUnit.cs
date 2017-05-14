using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 40)]
    public sealed class ArcherUnit : Unit, IRangedUnit, IClonableUnit, IHealableUnit
    {
        public ArcherUnit()
        {
            MaxHealth = CurrentHealth = 60;
            Attack = 10;
            Defense = 10;
        }

        public Unit Clone()
        {
            var clone = new ArcherUnit
            {
                Attack = Attack,
                CurrentHealth = CurrentHealth,
                MaxHealth = MaxHealth,
                Defense = Defense,
                RangeAttack = RangeAttack,
                Range = Range
            };
            return clone;
        }

        public int Range { get; set; } = 4;
        public int RangeAttack { get; set; } = 40;
    }
}