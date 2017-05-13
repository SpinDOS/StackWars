using System;
using System.Linq;
using StackWars.Commands;
using StackWars.Logger;
using StackWars.Units;
using StackWars.Units.Interfaces;

namespace StackWars
{
    public sealed class GameEngine
    {
        private int _turnsWithoutDeath;

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

            ILogger logger = FileLogger.GetFileLogger("ProxyLogs.txt");

            void ReplaceClonersWithProxy(Army army)
            {
                for (var i = 0; i < army.Count; i++)
                {
                    var unit = army[i];
                    if (unit is IClonerUnit clonerUnit)
                        army[i] = new ProxyClonerUnit(clonerUnit, logger);
                }
            }

            ReplaceClonersWithProxy(Army1);
            ReplaceClonersWithProxy(Army2);
        }

        public bool GameEnded { get; set; }

        public Army Army1 { get; }
        public Army Army2 { get; }

        public void Turn()
        {
            if (GameEnded)
                return;

            var combinations = Enumerable.Range(1, Army1.Count - 1)
                .Select(i => (Army1, Army2, i))
                .Concat(Enumerable.Range(1, Army2.Count - 1).Select(i => (Army2, Army1, i)))
                .OrderBy(x => Random.Next());

            foreach (var tuple in combinations)
                HandleCommands(tuple.Item1, tuple.Item2, tuple.Item3);

            if (Random.Next(2) == 0)
            {
                HandleMeleeAttack(Army1, Army2);
                HandleMeleeAttack(Army2, Army1);
            }
            else
            {
                HandleMeleeAttack(Army2, Army1);
                HandleMeleeAttack(Army1, Army2);
            }

            var unitsBefore = Army1.Count + Army2.Count;
            CommandsInvoker.Execute(Army1.CollectDead());
            CommandsInvoker.Execute(Army2.CollectDead());
            CommandsInvoker.EndTurn();
            if (Army1.Count + Army2.Count == unitsBefore)
                _turnsWithoutDeath++;
            else
                _turnsWithoutDeath = 0;

            if (_turnsWithoutDeath == DrawLimit || Army1.Count == 0 || Army2.Count == 0)
                GameEnded = true;
        }

        private void HandleCommands(Army allies, Army enemies, int i)
        {
            var unit = allies[i];
            if (unit.CurrentHealth <= 0)
                return;
            if (unit is IRangedUnit)
                HandleRangeAttack(allies, enemies, i);
            if (unit is IBufferUnit)
                HandleBuffer(allies, i);
            if (unit is IHealerUnit)
                HandleHealer(allies, i);
            if (unit is IClonerUnit)
                HandleCloner(allies, i);
        }

        private void HandleRangeAttack(Army allies, Army enemies, int unitIndex)
        {
            var rangedUnit = allies[unitIndex] as IRangedUnit;
            var range = rangedUnit.Range - unitIndex;
            if (range <= 0)
                return;
            var target = FindUnitInRange(enemies, 0, range,
                unit => unit.CurrentHealth > 0);
            if (target < 0)
                return;
            HandleDamage(allies, unitIndex, enemies, target, rangedUnit.RangeAttack);
        }

        private void HandleBuffer(Army allies, int unitIndex)
        {
            if (Random.NextDouble() > _heavyInfantryBuffChance)
                return;
            var buff = BuffTypes[Random.Next(BuffTypes.Length)];
            var target = FindUnitInRange(allies, unitIndex, 1,
                unit => unit.CurrentHealth > 0 && unit is IBuffableUnit buffable && buffable.CanBeBuffed(buff));
            if (target < 0)
                return;
            CommandsInvoker.Execute(new BuffCommand(allies, unitIndex, allies, target, buff));
        }

        private void HandleHealer(Army allies, int unitIndex)
        {
            if (Random.NextDouble() > _healChance)
                return;
            var healer = allies[unitIndex] as IHealerUnit;
            var target = FindUnitInRange(allies, unitIndex, healer.HealRange,
                unit => unit.CurrentHealth > 0 && unit is IHealableUnit);
            if (target < 0)
                return;
            var targetUnit = allies[target];
            var heal = Math.Min(targetUnit.MaxHealth, targetUnit.CurrentHealth + healer.Heal);
            CommandsInvoker.Execute(new HealCommand(allies, unitIndex, allies, target, heal));
        }


        private void HandleCloner(Army allies, int unitIndex)
        {
            if (Random.NextDouble() > _cloneChance)
                return;
            var cloner = allies[unitIndex] as IClonerUnit;
            var target = FindUnitInRange(allies, unitIndex, cloner.CloneRange,
                unit => unit.CurrentHealth > 0 && unit is IClonableUnit);
            if (target < 0)
                return;
            CommandsInvoker.Execute(new CloneCommand(allies, unitIndex, allies, target,
                Random.Next(allies.Count + 1)));
        }

        private int FindUnitInRange(Army army, int mid, int range, Func<Unit, bool> selector)
        {
            if (army.Count == 0)
                return -1;
            int start = Math.Max(0, mid - range), end = Math.Min(army.Count - 1, mid + range);
            if (end < 0)
                end = army.Count - 1; // fix overflow error
            var rand = Random.Next(start, end + 1);
            for (var i = rand; i <= end; i++)
                if (selector(army[i]))
                    return i;
            for (var i = start; i < rand; i++)
                if (selector(army[i]))
                    return i;
            return -1;
        }

        private void HandleMeleeAttack(Army allies, Army enemies)
        {
            var attacker = allies[0];
            if (attacker.CurrentHealth > 0)
                HandleDamage(allies, 0, enemies, 0, attacker.Attack);
        }

        private static void HandleDamage(Army sourceArmy, int? sourceUnitIndex,
            Army targetArmy, int targetUnitIndex, int damage)
        {
            if (damage <= 0)
                return;

            double CountDamage(int defense) { return 1.0 * damage * (100 - defense) / 100; }

            var target = targetArmy[targetUnitIndex];
            if (target.CurrentHealth <= 0)
                return;
            var resultDamage = (int) CountDamage(target.Defense);
            var dmgCommand = new DamageCommand(sourceArmy, sourceUnitIndex, targetArmy, targetUnitIndex, resultDamage);
            CommandsInvoker.Execute(dmgCommand);

            if (target.CurrentHealth <= 0 || !(target is BuffedUnit buffUnit))
                return;

            var removeBuff = new RemoveBuffCommand(sourceArmy, sourceUnitIndex,
                targetArmy, targetUnitIndex, Random.Next(buffUnit.BuffCount));
            CommandsInvoker.Execute(removeBuff);
        }

        #region Chances

        private static readonly double _heavyInfantryBuffChance = 0.2;
        private static readonly double _healChance = 0.3;
        private static readonly double _cloneChance = 0.1;

        private static readonly int DrawLimit = 10;

        #endregion

        #region Static members

        public static GameEngine CurrentGame { get; private set; }

        public static GameEngine StartNewGame(UnitFactory.UnitFactory unitFabric, int armyCost)
        {
            return CurrentGame = new GameEngine(unitFabric, armyCost);
        }

        private static readonly Random Random = new Random();
        private static readonly BuffType[] BuffTypes = Enum.GetValues(typeof(BuffType)) as BuffType[];

        #endregion
    }
}