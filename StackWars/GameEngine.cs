using System;
using System.Collections;
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
        private static double _healChance = 0.3;

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
                    cmd.TargetArmy[cmd.TargetUnitIndex] is BuffUnit buffUnit)
                {
                    int buffNumber = _random.Next(buffUnit.BuffCount);
                    commands.Insert(i + 1, new RemoveRandomBuffCommand(cmd.SourceArmy, cmd.SourceUnitIndex ?? -1, 
                                                                        cmd.TargetArmy, cmd.TargetUnitIndex, 
                                                                        buffNumber));
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
                if (unit is IHealer)
                    HandleHealer(allies, i, result);

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
            if (_random.NextDouble() > _heavyInfantryBuffChance)
                return;
            int target = FindUnitInRange(allies, unitIndex, 1, IsBuffable);
            if (target == -1)
                return;

            BuffType[] buffTypes = Enum.GetValues(typeof(BuffType)) as BuffType[];
            BuffType buffType = buffTypes[_random.Next(buffTypes.Length)];

            list.Add(new BuffCommand(allies, unitIndex, allies, target, buffType));
        }

        bool IsHealable(Unit unit) => true;

        private void HandleHealer(Army allies, int unitIndex, List<Command> list)
        {
            if (_random.NextDouble() > _healChance)
                return;
            IHealer healer = allies[unitIndex] as IHealer;
            int target = FindUnitInRange(allies, unitIndex, healer.Range, IsHealable);
            if (target >= 0)
                list.Add(new HealCommand(allies, unitIndex, allies, target, healer.Heal));
        }

        private int FindUnitInRange(Army army, int mid, int range, Func<Unit, bool> selector)
        {
            if (army.Count == 0)
                return -1;
            int start = Math.Max(0, mid - range), end = Math.Min(army.Count - 1, mid + range);
            if (end < 0)
                end = army.Count - 1; // fix overflow error
            int rand = _random.Next(start, end);
            for (int i = rand; i <= end; i++)
            {
                if (selector(army[i]))
                    return i;
            }
            for (int i = 0; i < rand; i++)
            {
                if (selector(army[i]))
                    return i;
            }
            return -1;
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
