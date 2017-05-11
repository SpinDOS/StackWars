namespace StackWars.Units.Interfaces
{
    public interface IRangedUnit : IUnit
    {
        int Range { get; set; }
        int RangeAttack { get; set; }
    }
}
