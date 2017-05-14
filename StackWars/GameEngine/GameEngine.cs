using System;
using System.Linq;
using StackWars.Commands;
using StackWars.Logger;
using StackWars.Units;
using StackWars.Units.Interfaces;

namespace StackWars.GameEngine
{
    public sealed class GameEngine
    {
        private int _turnsWithoutDeath = 0;
        private int _turns = 0;

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
            logger.Log($"New game started: {DateTime.Now}");

            void ReplaceClonersWithProxy(Army army)
            {
                for (var i = 0; i < army.Count; i++)
                {
                    var unit = army[i];
                    if (unit is ClonerUnit clonerUnit)
                        army[i] = new ProxyClonerUnit(clonerUnit, logger);
                }
            }

            ReplaceClonersWithProxy(Army1);
            ReplaceClonersWithProxy(Army2);
        }

        public bool GameEnded { get; set; }

        public Army Army1 { get; }
        public Army Army2 { get; }

        public IFightStrategy Strategy { get; set; } = FightStrategy1Vs1.Singleton;

        public void Turn()
        {
            if (GameEnded)
                return;

            var magics = Strategy.GetMagicUnits(Army1, Army2);
            foreach (var tuple in magics)
                HandleCommands(tuple.allies, tuple.enemies, tuple.alliesIndex);

            var melees = Strategy.GetMeleeAttacks(Army1, Army2);
            foreach (var tuple in melees)
                MakeDamage(tuple.allies, tuple.alliesIndex, tuple.enemies, 
                    tuple.targetIndex, tuple.allies[tuple.alliesIndex].Attack);

            var unitsBefore = Army1.Count + Army2.Count;
            CommandsInvoker.Execute(Army1.CollectDead());
            CommandsInvoker.Execute(Army2.CollectDead());
            CommandsInvoker.EndTurn();

            if (Army1.Count + Army2.Count == unitsBefore)
                _turnsWithoutDeath++;
            else
                _turnsWithoutDeath = 0;

            if (++_turns == DrawLimit || _turnsWithoutDeath == DrawLimitTurnsWithoutDeaths ||
                            Army1.Count == 0 || Army2.Count == 0)
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
            var target = Strategy.FindRandomEnemyUnitInRange(allies, unitIndex, enemies, 
                                        rangedUnit.Range, unit => unit.CurrentHealth > 0);
            if (!target.HasValue)
                return;
            MakeDamage(allies, unitIndex, enemies, target.Value, rangedUnit.RangeAttack);
        }

        private void HandleBuffer(Army allies, int unitIndex)
        {
            var buffer = allies[unitIndex] as IBufferUnit;
            if (Random.Next(100) > buffer.BuffChance)
                return;
            var buff = BuffTypes[Random.Next(BuffTypes.Length)];
            var target = Strategy.FindRandomUnitInRange(allies, unitIndex, buffer.BuffRange,
                unit => unit.CurrentHealth > 0 && unit is IBuffableUnit buffable && buffable.CanBeBuffed(buff));
            if (!target.HasValue)
                return;
            CommandsInvoker.Execute(new BuffCommand(allies, unitIndex, allies, target.Value, buff));
        }

        private void HandleHealer(Army allies, int unitIndex)
        {
            var healer = allies[unitIndex] as IHealerUnit;
            if (Random.Next(100) > healer.HealChance)
                return;
            var target = Strategy.FindRandomUnitInRange(allies, unitIndex, healer.HealRange,
                unit => unit.CurrentHealth > 0 && unit is IHealableUnit);
            if (!target.HasValue)
                return;
            var targetUnit = allies[target.Value];
            var endHealth = Math.Min(targetUnit.MaxHealth, targetUnit.CurrentHealth + healer.Heal);
            int heal = endHealth - targetUnit.CurrentHealth;
            if (heal > 0)
                CommandsInvoker.Execute(new HealCommand(allies, unitIndex, allies, target.Value, heal));
        }


        private void HandleCloner(Army allies, int unitIndex)
        {
            var cloner = allies[unitIndex] as IClonerUnit;
            if (Random.Next(100) > cloner.CloneChance)
                return;
            var target = Strategy.FindRandomUnitInRange(allies, unitIndex, cloner.CloneRange,
                unit => unit.CurrentHealth > 0 && unit is IClonableUnit);
            if (!target.HasValue)
                return;
            CommandsInvoker.Execute(new CloneCommand(allies, unitIndex, allies, target.Value,
                Random.Next(allies.Count + 1)));
        }

        private static void MakeDamage(Army sourceArmy, int? sourceUnitIndex,
            Army targetArmy, int targetUnitIndex, int damage)
        {
            if (damage <= 0 || 
                (sourceUnitIndex.HasValue && sourceArmy[sourceUnitIndex.Value].CurrentHealth <= 0))
                return;

            var target = targetArmy[targetUnitIndex];
            if (target.CurrentHealth <= 0)
                return;

            double CountDamage(int defense) { return 1.0 * damage * (100 - defense) / 100; }
            
            var resultDamage = (int) CountDamage(target.Defense);
            var dmgCommand = new DamageCommand(sourceArmy, sourceUnitIndex, targetArmy, targetUnitIndex, resultDamage);
            CommandsInvoker.Execute(dmgCommand);

            if (target.CurrentHealth <= 0 || !(target is BuffedUnit buffUnit))
                return;

            var removeBuff = new RemoveBuffCommand(sourceArmy, sourceUnitIndex,
                targetArmy, targetUnitIndex, Random.Next(buffUnit.BuffCount));
            CommandsInvoker.Execute(removeBuff);
        }
        
        #region Static members

        public static GameEngine CurrentGame { get; private set; }

        public static GameEngine StartNewGame(UnitFactory.UnitFactory unitFabric, int armyCost)
        {
            return CurrentGame = new GameEngine(unitFabric, armyCost);
        }

        private static readonly Random Random = new Random();
        private static readonly BuffType[] BuffTypes = Enum.GetValues(typeof(BuffType)) as BuffType[];

        private const int DrawLimit = 1000;
        private const int DrawLimitTurnsWithoutDeaths = 15;

        #endregion
    }
}