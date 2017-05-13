using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Commands;
using StackWars.Units.Interfaces;

namespace StackWars.Units
{

    public sealed class HorseBuffedUnit : BuffedUnit
    {
        public HorseBuffedUnit(IBuffableUnit baseUnit) : base(baseUnit) { }
        public override int Attack => base.Attack + 10;
        public override string ToString() => base.ToString() + " (with horse)";
        public override bool CanBeBuffed(BuffType type)
            => type != BuffType.Horse && base.CanBeBuffed(type);
    }

    public sealed class ArmorBuffedUnit : BuffedUnit
    {
        public ArmorBuffedUnit(IBuffableUnit baseUnit) : base(baseUnit) { }
        public override int Defense => base.Defense + 10;
        public override string ToString() => base.ToString() + " (with armor)";
        public override bool CanBeBuffed(BuffType type)
            => type != BuffType.Armor && base.CanBeBuffed(type);

    }

    public sealed class HelmetBuffedUnit : BuffedUnit
    {
        public HelmetBuffedUnit(IBuffableUnit baseUnit) : base(baseUnit) { }
        public override int Defense => base.Defense + 5;
        public override string ToString() => base.ToString() + " (with helmet)";
        public override bool CanBeBuffed(BuffType type)
            => type != BuffType.Helmet && base.CanBeBuffed(type);
    }

    public sealed class RapierBuffedUnit : BuffedUnit
    {
        public RapierBuffedUnit(IBuffableUnit baseUnit) : base(baseUnit) { }
        public override int Attack => base.Attack + 20;
        public override string ToString() => base.ToString() + " (with rapier)";
        public override bool CanBeBuffed(BuffType type)
            => type != BuffType.Rapier && base.CanBeBuffed(type);
    }
}
