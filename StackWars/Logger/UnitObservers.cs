using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Notify(IObservableUnit sender, UnitObservingState state)
        {
            Console.Beep();
        }
    }
}
