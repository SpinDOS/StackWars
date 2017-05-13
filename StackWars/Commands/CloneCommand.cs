using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;

namespace StackWars.Commands
{
    public class CloneCommand : SingleTargetCommand
    {
        public CloneCommand(Army source, int sourceIndex, Army target, int targetIndex, int insertPositon)
            : base(source, sourceIndex, target, targetIndex)
        {
            InsertPosition = insertPositon;
        }
        public int InsertPosition { get; }
        public override void Execute(ILogger logger)
        {
            TargetArmy.Insert(InsertPosition, TargetArmy[TargetUnitIndex].Clone());
        }

        public override void Undo(ILogger logger)
        {
            TargetArmy.RemoveAt(InsertPosition);
        }
    }
}
