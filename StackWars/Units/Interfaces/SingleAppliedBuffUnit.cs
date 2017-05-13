using System;

namespace StackWars.Units.Interfaces
{
    public abstract class SingleAppliedBuffUnit : BuffUnit
    {
        protected SingleAppliedBuffUnit(Unit baseUnit) : base(baseUnit)
        {
//            BuffUnit buffUnit = baseUnit as BuffUnit;
//            if (buffUnit == null)
//                return;
//            Type thisType = this.GetType();
//            if (buffUnit.GetType() == thisType)
//            {
//                BaseUnit = buffUnit.BaseUnit;
//                return;
//            }
//            BuffUnit current = buffUnit;
//            while (current != null)
//            {
//                if (current.BaseUnit.GetType() == thisType)
//                {
//                    current.BaseUnit = (current.BaseUnit as BuffUnit).BaseUnit;
//                    return;
//                }
//                current = current.BaseUnit as BuffUnit;
//            }
        }
        public override bool CanBeAffectedBy(Type typeOfAbility)
        {
            if (typeOfAbility == this.GetType())
                return false;
            return base.CanBeAffectedBy(typeOfAbility);
        }
    }
}
