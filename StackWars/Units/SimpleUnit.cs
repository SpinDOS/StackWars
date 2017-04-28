using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Abilities;
using StackWars.Units;

namespace StackWars
{
    public abstract class SimpleUnit : IUnit
    {
        protected ILogger Logger { get; }
        protected SimpleUnit(ILogger logger)
        {
            Logger = logger;
        }
        public virtual int MaxHealth { get; set; }
        public virtual int Health { get; set; }
        public virtual int Attack { get; set; }
        public virtual int Defense { get; set; }
        public override string ToString() => 
            $"{this.GetType().Name}: Health: {Health}, Attack: {Attack}, Defense: {Defense}";

        public abstract IUnit Clone();
        protected virtual IUnit Clone(IUnit pretendent)
        {
            pretendent.MaxHealth = MaxHealth;
            pretendent.Health = Health;
            pretendent.Defense = Defense;
            pretendent.Attack = Attack;
            return pretendent;
        }
    }
}
