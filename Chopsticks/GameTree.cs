using MonteCarloLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopsticks
{
    class GameTree : MonteCarloLib.GameTree
    {
        public GameStatus CurrentStatus;
        protected override IGameStatus Current => CurrentStatus;

        private bool isMax;

        public GameTree(int hands, bool humanFirst = true)
        {
            CurrentStatus = new GameStatus(hands * 2);
            isMax = !humanFirst;

            if (!humanFirst)
            {
                CurrentStatus = (GameStatus)BestMove(isMax, 5000);
            }
        }

        public void Attack(int move, int hand)
        {
            if (CurrentStatus.IsTerminal)
            {
                return;
            }
            List<int> target = CurrentStatus.Attack(move, hand).Hands;

            CurrentStatus = (GameStatus)CurrentStatus.Moves.FirstOrDefault(x => ((GameStatus)x).Hands.SequenceEqual(target));

            if (!CurrentStatus.IsTerminal)
            {
                //3310 !max has duplicate
                CurrentStatus = (GameStatus)BestMove(isMax, 5000);
            }
        }

        public void Transfer(int move, int hand, int amount)
        {
            if (CurrentStatus.IsTerminal)
            {
                return;
            }
            List<int> target = CurrentStatus.Transfer(move, hand, amount).Hands;

            CurrentStatus = (GameStatus)CurrentStatus.Moves.FirstOrDefault(x => ((GameStatus)x).Hands.SequenceEqual(target));

            if (!CurrentStatus.IsTerminal)
            {
                CurrentStatus = (GameStatus)BestMove(isMax, 5000);
            }
        }

    }
}
