using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackWars.Abilities;
using StackWars.Units;

namespace StackWars
{
    public class GameEngine
    {
        static GameEngine _currentGame = null;
        public static GameEngine CurrentGame => _currentGame;
        public static GameEngine StartNewGame(IUnitFactory unitFabric, int armyCost) 
            => _currentGame = new GameEngine(unitFabric, armyCost);

        Random random = new Random();
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
            foreach (var unit in Army1.Units)
            {
                IAbilitiable iabilitiable = unit as IAbilitiable;
                var abilities = iabilitiable?.MakeTurn();
                if (abilities == null)
                    continue;
                foreach (var ability in abilities)
                    InvokeAbility(ability, iabilitiable, Army1, Army2);
            }
            foreach (var unit in Army2.Units)
            {
                IAbilitiable iabilitiable = unit as IAbilitiable;
                var abilities = iabilitiable?.MakeTurn();
                if (abilities == null)
                    continue;
                foreach (var ability in abilities)
                    InvokeAbility(ability, iabilitiable, Army2, Army1);
            }
            MeleeAttack();
            Army1.CollectDeads();
            Army2.CollectDeads();
        }
        private void InvokeAbility(Ability ability, IAbilitiable creator, Army friends, Army enemies)
        { }
        private void MeleeAttack()
        {
            IUnit left = Army1.Units.First();
            IUnit right = Army2.Units.First();
            if (left.Health > 0)
                right.Health -= (100 - right.Defense) * left.Attack / 100;
            if (right.Health > 0)
                left.Health -= (100 - left.Defense) * right.Attack / 100;
        }

    }
}
