using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Commands;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 30)]
    public sealed class Cleric : Unit, IRangedUnit, IHealer, IObservableUnit
    {
        public Cleric()
        {
            Attack = 10;
            Defense = 5;
            MaxHealth = CurrentHealth = 1;
        }

        public int HealRange { get; set; } = int.MaxValue;

        public int Heal { get; set; } = 10;

        public int Range { get; set; } = 5;

        public int RangeAttack { get; set; } = 10;

        private readonly List<IUnitObserver> _observers = new List<IUnitObserver>();

        public void AddObserver(IUnitObserver observer) { if (observer != null) _observers.Add(observer); }
        public void RemoveObserver(IUnitObserver observer) { _observers.Remove(observer); }

        public override int CurrentHealth
        {
            set
            {
                if (value <= 0)
                {
                    UnitObservingState state = new UnitObservingState(this.CurrentHealth, value);
                    foreach (var observer in _observers)
                        observer.Notify(this, state);
                }

                base.CurrentHealth = value;
            }
        }
    }
}
