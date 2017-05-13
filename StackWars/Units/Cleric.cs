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

        public override Unit Clone()
        {
            Cleric cleric = new Cleric();
            cleric.Heal = this.Heal;
            cleric.RangeAttack = this.RangeAttack;
            cleric.Range = this.Range;
            cleric.HealRange = this.HealRange;
            foreach (var observer in _observers)
                cleric.AddObserver(observer);
            return base.Clone(cleric);
        }

        public int HealRange { get; set; } = int.MaxValue;

        public int Heal { get; set; } = 10;

        public int Range { get; set; } = 5;

        public int RangeAttack { get; set; } = 10;
        public override bool CanBeAffectedBy(Type typeOfAbility)
        {
            if (typeOfAbility == typeof(BuffCommand))
                return false;
            return base.CanBeAffectedBy(typeOfAbility);
        }

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
