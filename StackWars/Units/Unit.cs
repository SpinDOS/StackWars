using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StackWars.Logger;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    public abstract class Unit
    {
        public virtual int MaxHealth { get; set; }
        public virtual int CurrentHealth { get; set; }
        public virtual int Attack { get; set; }
        public virtual int Defense { get; set; }
        public override string ToString() => 
            $"{this.GetType().Name}: Health: {CurrentHealth}, Attack: {Attack}, Defense: {Defense}";

        public abstract Unit Clone();

        protected virtual Unit Clone(Unit pretendent)
        {
            pretendent.MaxHealth = MaxHealth;
            pretendent.CurrentHealth = CurrentHealth;
            pretendent.Defense = Defense;
            pretendent.Attack = Attack;
            return pretendent;
        }
    }
}
