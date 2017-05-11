using StackWars.Units.Interfaces;

namespace StackWars.UnitFactory
{
    public interface IUnitFactory
    {
        IUnit GetUnit(ref int maxPossibleCost);
    }
}