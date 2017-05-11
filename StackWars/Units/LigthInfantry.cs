using StackWars.Units.Interfaces;

namespace StackWars.Units
{
    [Cost(Cost = 10)]
    public sealed class LigthInfantry : SimpleUnit
    {
        public LigthInfantry()
        {
            MaxHealth = CurrentHealth = 100;
            Attack = 30;
            Defense = 15;
        }

        public override IUnit Clone() => base.Clone(new LigthInfantry());
    }
}
