using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StackWars.Abilities;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    public abstract class SimpleUnit : IUnit
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public override string ToString() => 
            $"{this.GetType().Name}: Health: {CurrentHealth}, Attack: {Attack}, Defense: {Defense}";

        public abstract IUnit Clone();
        public virtual IEnumerable<Ability> MakeTurn() => Enumerable.Empty<Ability>();

        protected virtual IUnit Clone(IUnit pretendent)
        {
            pretendent.MaxHealth = MaxHealth;
            pretendent.CurrentHealth = CurrentHealth;
            pretendent.Defense = Defense;
            pretendent.Attack = Attack;
            return pretendent;
        }
    }
}
