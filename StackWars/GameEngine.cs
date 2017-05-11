using System;
using System.Linq;
using StackWars.Abilities;
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
            Army1 = new Army("Army 1", unitFabric, armyCost);
            Army2 = new Army("Army 2", unitFabric, armyCost);
            GameEnded = false;
        }
        public bool GameEnded { get; private set; }
        public Army Army1 { get; }
        public Army Army2 { get; }
        public void Turn()
        {
            if (GameEnded)
                return;
        }
        private void MeleeAttack()
        {
            IUnit left = Army1.Units.First();
            IUnit right = Army2.Units.First();
            if (left.CurrentHealth > 0)
                right.CurrentHealth -= (100 - right.Defense) * left.Attack / 100;
            if (right.CurrentHealth > 0)
                left.CurrentHealth -= (100 - left.Defense) * right.Attack / 100;
        }

    }
}
