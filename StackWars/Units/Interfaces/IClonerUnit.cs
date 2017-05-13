namespace StackWars.Units.Interfaces
{
    public interface IClonerUnit
    {
        int CloneRange { get; set; }
    }

    public interface IClonableUnit
    {
        Unit Clone();
    }
}