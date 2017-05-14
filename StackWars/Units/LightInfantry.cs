using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 15)]
    public sealed class LightInfantry : Unit, IClonableUnit, IHealableUnit, IBufferUnit
    {
        public LightInfantry()
        {
            MaxHealth = CurrentHealth = 100;
            Attack = 30;
            Defense = 15;
        }

        public Unit Clone()
        {
            var clone = new LightInfantry();
            clone.Attack = Attack;
            clone.CurrentHealth = CurrentHealth;
            clone.MaxHealth = MaxHealth;
            clone.Defense = Defense;
            return clone;
        }

        public int BuffChance { get; set; } = 10;
        public int BuffRange { get; set; } = 1;
    }
}