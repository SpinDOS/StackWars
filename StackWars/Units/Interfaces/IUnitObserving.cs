using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackWars.Units.Interfaces
{
    public struct UnitObservingState
    {
        public UnitObservingState (int before, int after)
        {
            HealthAfter = after;
            HealthBefore = before;
        }
        public int HealthBefore { get; }
        public int HealthAfter { get; }
    }
    public interface IObservableUnit
    {
        void AddObserver(IUnitObserver observer);
        void RemoveObserver(IUnitObserver observer);
    }

    public interface IUnitObserver
    {
        void Notify(IObservableUnit sender, UnitObservingState state);
    }
}
