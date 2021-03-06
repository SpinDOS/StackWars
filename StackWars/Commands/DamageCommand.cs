﻿using StackWars.Logger;

namespace StackWars.Commands
{
    public class DamageCommand : SingleTargetCommand
    {
        public DamageCommand(Army source, int? sourceIndex, Army target, int targetIndex, int damage)
            : base(source, sourceIndex, target, targetIndex)
        {
            Damage = damage;
        }

        public int Damage { get; }

        public override void Execute(ILogger logger)
        {
            var attacker = SourceUnitIndex.HasValue?
                $"{SourceArmy[SourceUnitIndex.Value]} ({SourceArmy.Name})" :
                SourceArmy.Name;

            logger?.Log($"{attacker} attacks " +
                        $"{TargetArmy[TargetUnitIndex]} ({TargetArmy.Name}) with {Damage} damage");
            TargetArmy[TargetUnitIndex].CurrentHealth -= Damage;
        }

        public override void Undo(ILogger logger) { TargetArmy[TargetUnitIndex].CurrentHealth += Damage; }
    }
}