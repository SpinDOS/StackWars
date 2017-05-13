using System;

namespace StackWars.Units.Interfaces
{
    public abstract class BuffUnit : WrapperUnit
    {
        static readonly Random Random = new Random();
        protected BuffUnit(Unit baseUnit) : base(baseUnit) { }
        
        public int BuffCount => BaseUnit is BuffUnit buffUnit? buffUnit.BuffCount + 1 : 1;
    }
}
