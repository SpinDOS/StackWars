using StackWars.Units;

namespace StackWars.UnitFactory
{
    public interface IUnitFactory
    {
        Unit GetUnit(ref int maxPossibleCost);
    }
}