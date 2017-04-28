using StackWars.Units;

namespace StackWars
{
    public interface IUnitFactory
    {
        IUnit GetUnit(ref int maxPossibleCost);
    }
}