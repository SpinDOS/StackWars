using System;
using System.Collections.Generic;
using System.Linq;
using StackWars.Commands;
using StackWars.UnitFactory;
using StackWars.Units;
using StackWars.Units.Interfaces;

namespace StackWars
{
    public sealed class GameEngine
    {
        private static double _heavyInfantryBuffChance = 0.2;

        static GameEngine _currentGame = null;
        public static GameEngine CurrentGame => _currentGame;
        public static GameEngine StartNewGame(UnitFactory.UnitFactory unitFabric, int armyCost) 
            => _currentGame = new GameEngine(unitFabric, armyCost);

        readonly Random _random = new Random();
        private GameEngine(UnitFactory.UnitFactory unitFabric, int armyCost)
        {
            if (unitFabric == null)
                throw new ArgumentNullException(nameof(unitFabric));
            CommandsInvoker.Army1 = new Army("Army 1", unitFabric, armyCost);
            CommandsInvoker.Army2 = new Army("Army 2", unitFabric, armyCost);
        }

        public bool GameEnded { get; private set; } = false;

        public void Turn()
        {
            if (GameEnded)
                return;

            var commands = GetCommands(CommandsInvoker.Army1, CommandsInvoker.Army2);
            commands.AddRange(GetCommands(CommandsInvoker.Army2, CommandsInvoker.Army1));
            commands = commands.OrderBy(a => _random.Next()).ToList();
            (Command melee1, Command melee2) = GetMeleeAttacks();
            commands.Add(melee1);
            commands.Add(melee2);

            for (int i = 0; i < commands.Count; i++)
            {
                DamageCommand cmd = commands[i] as DamageCommand;
                if (cmd?.Damage > 10 &&
                    cmd.TargetArmy[cmd.TargetUnitIndex] is BuffUnit)
                {
                    commands.Insert(i + 1, new RemoveRandomBuffCommand(cmd.SourceArmy, cmd.SourceUnitIndex ?? -1, 
                                                                        cmd.TargetArmy, cmd.TargetUnitIndex));
                }

            }

            CommandsInvoker.AddCommand(commands);

            CommandsInvoker.EndTurn();
            if (CommandsInvoker.Army1.Count == 0 || CommandsInvoker.Army2.Count == 0)
                GameEnded = true;
        }

        private List<Command> GetCommands(Army allies, Army enemies)
        {
            var result = new List<Command>();
            for (int i = 0; i < allies.Count; i++)
            {
                Unit unit = allies[i];
                if (unit is IRangedUnit)
                    HandleRangeAttack(allies, enemies, i, result);
                if (unit is LigthInfantry)
                    HandleHeavyInfantryBuff(allies, i, result);
            }
            return result;
        }

        private void HandleRangeAttack(Army allies, Army enemies, int unitIndex, List<Command> list)
        {
            IRangedUnit rangedUnit = allies[unitIndex] as IRangedUnit;
            int range = rangedUnit.Range;
            if (range <= unitIndex)
                return;
            int target = _random.Next(Math.Min(range - unitIndex, enemies.Count));
            int damage = CountDamageWithDefense(rangedUnit.RangeAttack, enemies[target].Defense);
            list.Add(new DamageCommand(allies, unitIndex, enemies, target, damage));
        }

        bool IsBuffable(Unit unit) => unit is HeavyInfantry || unit is BuffUnit;

        private void HandleHeavyInfantryBuff(Army allies, int unitIndex, List<Command> list)
        {
            int target = -1;
            if (unitIndex != 0 && IsBuffable(allies[unitIndex - 1]))
                target = unitIndex - 1;
            if (_random.Next(2) == 0 && unitIndex != allies.Count - 1 && IsBuffable(allies[unitIndex + 1]))
                target = unitIndex + 1;
            if (target == -1 || _random.NextDouble() > _heavyInfantryBuffChance)
                return;

            BuffType[] buffTypes = Enum.GetValues(typeof(BuffType)) as BuffType[];
            BuffType buffType = buffTypes[_random.Next(buffTypes.Length)];

            list.Add(new BuffCommand(allies, unitIndex, allies, target, buffType));
        }

        private (Command melee1, Command melee2) GetMeleeAttacks()
        {
            Unit unit1 = CommandsInvoker.Army1.First(), unit2 = CommandsInvoker.Army2.First();
            Command command1 = new MeleeDamageCommand(CommandsInvoker.Army1, CommandsInvoker.Army2, 
                CountDamageWithDefense(unit2.Attack, unit1.Defense));
            Command command2 = new MeleeDamageCommand(CommandsInvoker.Army2, CommandsInvoker.Army1, 
                CountDamageWithDefense(unit1.Attack, unit2.Defense));
            if (_random.Next(2) == 0)
                return (command1, command2);
            else
                return (command2, command1);
        }

        private static int CountDamageWithDefense(int damage, int defense)
        {
            double result = 1.0 * damage * (100 - defense) / 100;
            return (int) result;
        }

    }
}
