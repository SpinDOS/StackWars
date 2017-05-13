using System;
using StackWars.Units.Interfaces;

namespace StackWars.Logger
{
    public class ConsoleUnitObserver : IUnitObserver
    {
        public void Notify(IObservableUnit sender, UnitObservingState state)
        {
            Console.WriteLine($"Unfortunately, {sender} is dead ({state.HealthBefore} => {state.HealthAfter})");
        }
    }

    public class BeepUnitObserver : IUnitObserver
    {
        public void Notify(IObservableUnit sender, UnitObservingState state) { Console.Beep(); }
    }
}