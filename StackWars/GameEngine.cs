using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StackWars.Commands;
using StackWars.Logger;
using StackWars.UnitFactory;
using StackWars.Units;
using StackWars.Units.Interfaces;

namespace StackWars
{
    public sealed class GameEngine
    {
        private static double _heavyInfantryBuffChance = 0.2;
        private static double _healChance = 0.3;
        private static double _cloneChance = 0.1;

        static GameEngine _currentGame = null;
        public static GameEngine CurrentGame => _currentGame;
        public static GameEngine StartNewGame(UnitFactory.UnitFactory unitFabric, int armyCost) 
            => _currentGame = new GameEngine(unitFabric, armyCost);

        static readonly Random _random = new Random();
        private GameEngine(UnitFactory.UnitFactory unitFabric, int armyCost)
        {
            if (unitFabric == null)
                throw new ArgumentNullException(nameof(unitFabric));
            Army1 = new Army("Army 1", unitFabric, armyCost);
            Army2 = new Army("Army 2", unitFabric, armyCost);

            IUnitObserver observer1 = new ConsoleUnitObserver(), observer2 = new BeepUnitObserver();
            foreach (var unit in Army1.Concat(Army2))
                if (unit is IObservableUnit observable)
                {
                    observable.AddObserver(observer1);
                    observable.AddObserver(observer2);
                }

            ILogger logger = new FileLogger("ProxyLogs.txt");
            for (int i = 0; i < Army1.Count; i++)
            {
                Unit unit = Army1[i];
                if (unit is ArcherUnit)
                    Army1[i] = new ProxyUnit(unit, logger);
            }
            for (int i = 0; i < Army2.Count; i++)
            {
                Unit unit = Army2[i];
                if (unit is Cleric)
                    Army2[i] = new ProxyUnit(unit, logger);
            }

        }

        public bool GameEnded { get; private set; } = false;

        public Army Army1 { get; }
        public Army Army2 { get; }

        public void Turn()
        {
            if (GameEnded)
                return;

            var combinations = Enumerable.Range(1, Army1.Count - 1).Select(i => (Army1, Army2, i))
                .Concat(
                    Enumerable.Range(1, Army2.Count - 1).Select(i => (Army2, Army1, i)))
                    .OrderBy(x => _random.Next());
            
            foreach (var tuple in combinations)
                HandleCommands(tuple.Item1, tuple.Item2, tuple.Item3);

            if (_random.Next(2) == 0)
            {
                HandleMeleeAttack(Army1, Army2);
                HandleMeleeAttack(Army2, Army1);
            }
            else
            {
                HandleMeleeAttack(Army2, Army1);
                HandleMeleeAttack(Army1, Army2);
            }

            CommandsInvoker.Execute(Army1.CollectDead());
            CommandsInvoker.Execute(Army2.CollectDead());
            CommandsInvoker.EndTurn();

            if (Army1.Count == 0 || Army2.Count == 0)
                GameEnded = true;
        }

        private void HandleCommands(Army allies, Army enemies, int i)
        {
            var unit = allies[i];
            if (unit.CurrentHealth <= 0)
                return;
            if (unit is IRangedUnit)
                HandleRangeAttack(allies, enemies, i);
            if (unit is LightInfantry)
                HandleHeavyInfantryBuffer(allies, i);
            if (unit is IHealer)
                HandleHealer(allies, i);
            if (unit is IClonerUnit)
                HandleCloner(allies, i);

        }

        private void HandleRangeAttack(Army allies, Army enemies, int unitIndex)
        {
            IRangedUnit rangedUnit = allies[unitIndex] as IRangedUnit;
            int range = rangedUnit.Range - unitIndex;
            if (range <= 0)
                return;
            int target = FindUnitInRange(enemies, 0, range, unit => unit.CurrentHealth > 0);
            if (target < 0)
                return;
            HandleDamage(allies, unitIndex, enemies, target, rangedUnit.RangeAttack);
        }

        private bool IsBuffable(Unit unit) => (unit is HeavyInfantry || unit is BuffUnit) && unit.CurrentHealth > 0;

        private void HandleHeavyInfantryBuffer(Army allies, int unitIndex)
        {
            if (_random.NextDouble() > _heavyInfantryBuffChance)
                return;
            int target = FindUnitInRange(allies, unitIndex, 1, IsBuffable);
            if (target < 0)
                return;

            BuffType[] buffTypes = Enum.GetValues(typeof(BuffType)) as BuffType[];
            BuffType buffType = buffTypes[_random.Next(buffTypes.Length)];

            CommandsInvoker.Execute(new BuffCommand(allies, unitIndex, allies, target, buffType));
        }

        private bool IsHealable(Unit unit) => unit.CurrentHealth > 0;

        private void HandleHealer(Army allies, int unitIndex)
        {
            if (_random.NextDouble() > _healChance)
                return;
            IHealer healer = allies[unitIndex] as IHealer;
            int target = FindUnitInRange(allies, unitIndex, healer.HealRange, IsHealable);
            if (target < 0)
                return;
            Unit targetUnit = allies[target];
            int heal = Math.Min(targetUnit.MaxHealth, targetUnit.CurrentHealth + healer.Heal);
            CommandsInvoker.Execute(new HealCommand(allies, unitIndex, allies, target, heal));
        }

        private bool IsClonable(Unit unit) => unit.CurrentHealth > 0 && unit is IClonableUnit;

        private void HandleCloner(Army allies, int unitIndex)
        {
            if (_random.NextDouble() > _cloneChance)
                return;
            IClonerUnit cloner = allies[unitIndex] as IClonerUnit;
            int target = FindUnitInRange(allies, unitIndex, cloner.CloneRange, IsClonable);
            if (target < 0)
                return;
            CommandsInvoker.Execute(new CloneCommand(allies, unitIndex, allies, target, 
                _random.Next(allies.Count + 1)));
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
            for (int i = start; i < rand; i++)
            {
                if (selector(army[i]))
                    return i;
            }
            return -1;
        }

        private void HandleMeleeAttack(Army allies, Army enemies)
        {
            Unit attacker = allies[0];
            if (attacker.CurrentHealth > 0)
                HandleDamage(allies, 0, enemies, 0, attacker.Attack);
        }

        private static void HandleDamage(Army sourceArmy, int? sourceUnitIndex, 
            Army targetArmy, int targetUnitIndex, int damage)
        {
            double CountDamage(int defense) => 1.0 * damage * (100 - defense) / 100;

            Unit target = targetArmy[targetUnitIndex];
            if (target.CurrentHealth <= 0)
                return;
            int resultDamage = (int) CountDamage(target.Defense);
            var dmgCommand = new DamageCommand(sourceArmy, sourceUnitIndex, targetArmy, targetUnitIndex, resultDamage);
            CommandsInvoker.Execute(dmgCommand);

            if (!(target is BuffUnit buffUnit))
                return;
            
            var removeBuff = new RemoveBuffCommand(sourceArmy, sourceUnitIndex,
                targetArmy, targetUnitIndex, _random.Next(buffUnit.BuffCount));
            CommandsInvoker.Execute(removeBuff);
        }

    }
}
