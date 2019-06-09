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
            List<int> target = CurrentStatus.Attack(move, hand).Hands;

            CurrentStatus = (GameStatus)CurrentStatus.Moves.FirstOrDefault(x => ((GameStatus)x).Hands.SequenceEqual(target));

            if (!CurrentStatus.IsTerminal)
            {
                CurrentStatus = (GameStatus)BestMove(CurrentStatus.Maximizer, 1000);
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
                CurrentStatus = (GameStatus)BestMove(CurrentStatus.Maximizer, 1000);
            }
        }

    }
}
