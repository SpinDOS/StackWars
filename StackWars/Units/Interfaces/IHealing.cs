namespace StackWars.Units.Interfaces
{
    public interface IHealerUnit
    {
        int HealRange { get; set; }
        int Heal { get; set; }
    }

    public interface IHealableUnit { }
}