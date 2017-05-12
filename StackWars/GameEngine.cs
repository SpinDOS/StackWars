using System;
using System.Collections.Generic;
using System.Linq;
using StackWars.Abilities;
using StackWars.Commands;
using StackWars.UnitFactory;
using StackWars.Units.Interfaces;

namespace StackWars
{
    public sealed class GameEngine
    {
        static GameEngine _currentGame = null;
        public static GameEngine CurrentGame => _currentGame;
        public static GameEngine StartNewGame(IUnitFactory unitFabric, int armyCost) 
            => _currentGame = new GameEngine(unitFabric, armyCost);

        readonly Random _random = new Random();
        private GameEngine(IUnitFactory unitFabric, int armyCost)
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
            CommandsInvoker.AddCommand(commands.OrderBy(a => _random.Next()));

            (Command melee1, Command melee2) = GetMeleeAttacks();
            CommandsInvoker.AddCommand(melee1);
            CommandsInvoker.AddCommand(melee2);

            CommandsInvoker.EndTurn();
            if (CommandsInvoker.Army1.Count == 0 || CommandsInvoker.Army2.Count == 0)
                GameEnded = true;
        }

        private List<Command> GetCommands(Army allies, Army enemies)
        {
            var result = new List<Command>();
            for (int i = 0; i < allies.Count; i++)
            {
                foreach (var ability in allies[i].MakeTurn())
                {
                    Command command = null;
                    if (ability is RangeAttack rangeAttack)
                        command = HandleRangeAttack(allies, enemies, rangeAttack, i);


                    if (command != null)
                        result.Add(command);
                }
            }
            return result;
        }

        private Command HandleRangeAttack(Army allies, Army enemies, RangeAttack rangeAttack, int sourceIndex)
        {
            if (rangeAttack.Range <= sourceIndex)
                return null;
            int target = _random.Next(Math.Min(rangeAttack.Range - sourceIndex, enemies.Count));
            int damage = CountDamageWithDefense(rangeAttack.Attack, enemies[target].Defense);
            return new DamageCommand(allies, sourceIndex, enemies, target, damage);
        }

        private (Command melee1, Command melee2) GetMeleeAttacks()
        {
            IUnit unit1 = CommandsInvoker.Army1.First(), unit2 = CommandsInvoker.Army2.First();
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
