using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Logger;

namespace StackWars.Commands
{
    public class MeleeDamageCommand : DamageCommand
    {
        public MeleeDamageCommand(Army source, Army target, int damage)
            : base(source, 0, target, 0, damage) { }

        public override void Execute(ILogger logger)
        {
            if (SourceArmy[0].CurrentHealth > 0)
                base.Execute(logger);
        }

        public override void Undo(ILogger logger)
        {
            if (SourceArmy[0].CurrentHealth > 0)
                base.Undo(logger);
        }
    }
}
