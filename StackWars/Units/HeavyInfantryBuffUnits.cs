using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{

    public sealed class HorseBuffUnit : SingleAppliedBuffUnit
    {
        public HorseBuffUnit(Unit baseUnit) : base(baseUnit) { }
        public override int Attack => BaseUnit.Attack + 10;
        public override Unit Clone() => base.Clone(new HorseBuffUnit(BaseUnit));
        public override string ToString() => base.ToString() + " (with horse)";
    }

    public sealed class ArmorBuffUnit : SingleAppliedBuffUnit
    {
        public ArmorBuffUnit(Unit baseUnit) : base(baseUnit) { }
        public override int Defense => BaseUnit.Defense + 10;
        public override Unit Clone() => base.Clone(new ArmorBuffUnit(BaseUnit));
        public override string ToString() => base.ToString() + " (with armor)";
    }

    public sealed class HelmetBuffUnit : SingleAppliedBuffUnit
    {
        public HelmetBuffUnit(Unit baseUnit) : base(baseUnit) { }
        public override int Defense => BaseUnit.Defense + 5;
        public override Unit Clone() => base.Clone(new HelmetBuffUnit(BaseUnit));
        public override string ToString() => base.ToString() + " (with helmet)";
    }

    public sealed class RapierBuffUnit : SingleAppliedBuffUnit
    {
        public RapierBuffUnit(Unit baseUnit) : base(baseUnit) { }
        public override int Attack => BaseUnit.Attack + 20;
        public override Unit Clone() => base.Clone(new RapierBuffUnit(BaseUnit));
        public override string ToString() => base.ToString() + " (with repier)";
    }
}
