namespace StackWars.Units.Interfaces
{
    public enum BuffType
    {
        Horse,
        Armor,
        Helmet,
        Rapier
    }

    public interface IBuffableUnit
    {
        bool CanBeBuffed(BuffType type);
    }

    public interface IBufferUnit { }

    public abstract class BuffedUnit : WrapperUnit, IBuffableUnit
    {
        protected BuffedUnit(IBuffableUnit baseUnit) : base(baseUnit as Unit) { }
        public int BuffCount => BaseUnit is BuffedUnit buffUnit? buffUnit.BuffCount + 1 : 1;
        public virtual bool CanBeBuffed(BuffType type) { return (BaseUnit as IBuffableUnit).CanBeBuffed(type); }
    }
}