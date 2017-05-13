using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 10)]
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
    }
}