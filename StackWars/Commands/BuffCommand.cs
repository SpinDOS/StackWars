using StackWars.Logger;
using StackWars.Units;
using StackWars.Units.Interfaces;

namespace StackWars.Commands
{
    public class BuffCommand : SingleTargetCommand
    {
        private readonly BuffType _type;

        public BuffCommand(Army source, int sourceIndex, Army target, int targetIndex, BuffType type)
            : base(source, sourceIndex, target, targetIndex)
        {
            _type = type;
        }

        public override void Execute(ILogger logger)
        {
            var unit = TargetArmy[TargetUnitIndex] as IBuffableUnit;
            switch (_type)
            {
            case BuffType.Armor:
                unit = new ArmorBuffedUnit(unit);
                break;
            case BuffType.Helmet:
                unit = new HelmetBuffedUnit(unit);
                break;
            case BuffType.Horse:
                unit = new HorseBuffedUnit(unit);
                break;
            case BuffType.Rapier:
                unit = new RapierBuffedUnit(unit);
                break;
            }
            TargetArmy[TargetUnitIndex] = unit as Unit;
        }

        public override void Undo(ILogger logger)
        {
            TargetArmy[TargetUnitIndex] = (TargetArmy[TargetUnitIndex] as BuffedUnit).BaseUnit;
        }
    }
}