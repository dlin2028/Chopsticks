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

        public GameTree(int hands, bool humanFirst = true)
        {
            CurrentStatus = new GameStatus(hands * 2);

            if (!humanFirst)
            {
                CurrentStatus = (GameStatus)BestMove(true, 1000);
            }
        }

        public void Attack(int move, int hand)
        {
            if (CurrentStatus.IsTerminal)
            {
                return;
            }
            CurrentStatus = CurrentStatus.Attack(move, hand);

            if (!CurrentStatus.IsTerminal)
            {
                CurrentStatus = (GameStatus)BestMove(CurrentStatus.Maximizer, 1000);
            }
        }

        public void Transfer(int move, int hand)
        {
            if (CurrentStatus.IsTerminal)
            {
                return;
            }
            CurrentStatus = CurrentStatus.Transfer(move, CurrentStatus.Hands[hand]);

            if (!CurrentStatus.IsTerminal)
            {
                CurrentStatus = (GameStatus)BestMove(CurrentStatus.Maximizer, 1000);
            }
        }

    }
}
