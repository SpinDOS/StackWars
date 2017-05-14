namespace StackWars.Units.Interfaces
{
    public interface IClonerUnit
    {
        int CloneChance { get; set; }
        int CloneRange { get; set; }
    }

    public interface IClonableUnit
    {
        Unit Clone();
    }
}