using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    public sealed class HorseBuffedUnit : BuffedUnit
    {
        public HorseBuffedUnit(IBuffableUnit baseUnit) : base(baseUnit) { }
        public override int Attack => base.Attack + 10;
        public override string ToString() { return base.ToString() + " (with horse)"; }

        public override bool CanBeBuffed(BuffType type) { return type != BuffType.Horse && base.CanBeBuffed(type); }
    }

    public sealed class ArmorBuffedUnit : BuffedUnit
    {
        public ArmorBuffedUnit(IBuffableUnit baseUnit) : base(baseUnit) { }
        public override int Defense => base.Defense + 10;
        public override string ToString() { return base.ToString() + " (with armor)"; }

        public override bool CanBeBuffed(BuffType type) { return type != BuffType.Armor && base.CanBeBuffed(type); }
    }

    public sealed class HelmetBuffedUnit : BuffedUnit
    {
        public HelmetBuffedUnit(IBuffableUnit baseUnit) : base(baseUnit) { }
        public override int Defense => base.Defense + 5;
        public override string ToString() { return base.ToString() + " (with helmet)"; }

        public override bool CanBeBuffed(BuffType type) { return type != BuffType.Helmet && base.CanBeBuffed(type); }
    }

    public sealed class RapierBuffedUnit : BuffedUnit
    {
        public RapierBuffedUnit(IBuffableUnit baseUnit) : base(baseUnit) { }
        public override int Attack => base.Attack + 20;
        public override string ToString() { return base.ToString() + " (with rapier)"; }

        public override bool CanBeBuffed(BuffType type) { return type != BuffType.Rapier && base.CanBeBuffed(type); }
    }
}