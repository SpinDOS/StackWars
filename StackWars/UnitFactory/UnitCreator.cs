using StackWars.Units;

namespace StackWars.UnitFactory
{
    public interface UnitFactory
    {
        Unit GetUnit(ref int maxPossibleCost);
    }
}