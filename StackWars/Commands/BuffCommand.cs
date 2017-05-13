using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;
using StackWars.Units;
using StackWars.Units.Interfaces;

namespace StackWars.Commands
{
    public enum BuffType
    {
        Horse,
        Armor,
        Helmet,
        Rapier,
    }
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
            Unit unit = TargetArmy[TargetUnitIndex];
            switch (_type)
            {
            case BuffType.Armor:
                unit = new ArmorBuffUnit(unit);
                break;
            case BuffType.Helmet:
                unit = new HelmetBuffUnit(unit);
                break;
            case BuffType.Horse:
                unit = new HorseBuffUnit(unit);
                break;
            case BuffType.Rapier:
                unit = new RapierBuffUnit(unit);
                break;
            }
            TargetArmy[TargetUnitIndex] = unit;
        }

        public override void Undo(ILogger logger)
            { TargetArmy[TargetUnitIndex] = (TargetArmy[TargetUnitIndex] as BuffUnit).BaseUnit; }

    }
}
