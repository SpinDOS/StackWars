using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StackWars.Units
{
    public abstract class SingleAppliedBuffUnit : BuffUnit
    {
        protected SingleAppliedBuffUnit(Unit baseUnit) : base(baseUnit)
        {
            BuffUnit buffUnit = baseUnit as BuffUnit;
            if (buffUnit == null)
                return;
            Type thisType = this.GetType();
            if (buffUnit.GetType() == thisType)
            {
                BaseUnit = buffUnit.BaseUnit;
                return;
            }
            BuffUnit current = buffUnit;
            while (current != null)
            {
                if (current.BaseUnit.GetType() == thisType)
                {
                    current.BaseUnit = (current.BaseUnit as BuffUnit).BaseUnit;
                    return;
                }
                current = current.BaseUnit as BuffUnit;
            }
        }
    }


    public sealed class HorseBuffUnit : SingleAppliedBuffUnit
    {
        public HorseBuffUnit(Unit baseUnit) : base(baseUnit) { }
        public override int Attack => BaseUnit.Attack + 10;
        public override Unit Clone() => base.Clone(new HorseBuffUnit(BaseUnit));
        public override string ToString() => BaseUnit.ToString() + " (with horse)";
    }

    public sealed class ArmorBuffUnit : SingleAppliedBuffUnit
    {
        public ArmorBuffUnit(Unit baseUnit) : base(baseUnit) { }
        public override int Defense => BaseUnit.Defense + 10;
        public override Unit Clone() => base.Clone(new ArmorBuffUnit(BaseUnit));
        public override string ToString() => BaseUnit.ToString() + " (with armor)";
    }

    public sealed class HelmetBuffUnit : SingleAppliedBuffUnit
    {
        public HelmetBuffUnit(Unit baseUnit) : base(baseUnit) { }
        public override int Defense => BaseUnit.Defense + 5;
        public override Unit Clone() => base.Clone(new HelmetBuffUnit(BaseUnit));
        public override string ToString() => BaseUnit.ToString() + " (with helmet)";
    }

    public sealed class RapierBuffUnit : SingleAppliedBuffUnit
    {
        public RapierBuffUnit(Unit baseUnit) : base(baseUnit) { }
        public override int Attack => BaseUnit.Attack + 20;
        public override Unit Clone() => base.Clone(new RapierBuffUnit(BaseUnit));
        public override string ToString() => BaseUnit.ToString() + " (with repier)";
    }
}
