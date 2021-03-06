﻿using System.Collections.Generic;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 30)]
    public sealed class Cleric : Unit, IRangedUnit, IHealerUnit, IObservableUnit
    {
        private readonly List<IUnitObserver> _observers = new List<IUnitObserver>();

        public Cleric()
        {
            Attack = 10;
            Defense = 5;
            MaxHealth = CurrentHealth = 30;
        }

        public override int CurrentHealth
        {
            set
            {
                if (value <= 0)
                {
                    var state = new UnitObservingState(CurrentHealth, value);
                    foreach (var observer in _observers)
                        observer.Notify(this, state);
                }

                base.CurrentHealth = value;
            }
        }

        public int HealChance { get; set; } = 20;
        public int HealRange { get; set; } = 100;

        public int Heal { get; set; } = 8;

        public void AddObserver(IUnitObserver observer)
        {
            if (observer != null) _observers.Add(observer);
        }

        public void RemoveObserver(IUnitObserver observer) { _observers.Remove(observer); }

        public int Range { get; set; } = 3;

        public int RangeAttack { get; set; } = 15;
    }
}