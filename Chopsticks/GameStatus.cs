using MonteCarloLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopsticks
{
    class GameStatus : MonteCarloLib.IGameStatus
    {
        private List<GameStatus> moves;

        //interface members
        public IEnumerable<IGameStatus> Moves
        {
            get
            {
                if (moves is null)
                {
                    moves = GenerateMoves();
                }
                return moves;
            }
            set
            {
                moves = (List<GameStatus>)value;
            }
        }

        public double Wins { get; set; }
        public double Simulations { get; set; }
        public bool Visited { get; set; }
        public int Value { get; set; }
        public bool IsTerminal { get; set; }

        //class members
        public bool Maximizer; //true for maximizer
        public List<int> Hands;

        public GameStatus(int hands)
        {
            Maximizer = true;
            Hands = new List<int>();
            for (int i = 0; i < hands; i++)
            {
                Hands.Add(1);
            }

            moves = null;

            Value = 0;
            IsTerminal = false;
        }
        public GameStatus(int[] hands, bool maximizer)
        {
            Hands = new List<int>(hands);
            Maximizer = maximizer;
            moves = null;

            Value = 0;
            IsTerminal = false;

            CheckGameOver();
        }

        public GameStatus Attack(int move, int hand)
        {
            Hands[move] += Hands[hand];
            if (Hands[move] > 4)
            {
                Hands[move] = 0;
            }

            return new GameStatus(Hands.ToArray(), !Maximizer);
        }
        public GameStatus Transfer(int move, int amount)
        {
            Hands[move] += amount;
            if (Hands[move] > 4)
            {
                Hands[move] = 0;
            }

            return new GameStatus(Hands.ToArray(), !Maximizer);
        }

        public List<GameStatus> GenerateMoves()
        {
            if (IsTerminal)
            {
                return new List<GameStatus>();
            }

            List<GameStatus> output = new List<GameStatus>();

            if (Maximizer) //hands 0 to count/2
            {
                //attacks
                for (int i = 0; i < Hands.Count / 2; i++) //hands.count is guaranteed to be an even number
                {
                    for (int j = 0; j < Hands.Count / 2; j++)
                    {
                        output.Add(Attack(i, j + Hands.Count / 2));
                    }
                }

                //transfers
                for (int i = Hands.Count / 2; i < Hands.Count; i++)
                {
                    for (int k = 0; k < Hands[i]; k++)
                    {
                        output.Add(Transfer(i, k));
                    }
                }
            }
            else
            {
                for (int i = 0; i < Hands.Count / 2; i++)
                {
                    for (int j = 0; j < Hands.Count / 2; j++)
                    {
                        output.Add(Attack(i + Hands.Count / 2, j));
                    }
                }

                for (int i = Hands.Count / 2; i < Hands.Count; i++)
                {
                    for (int k = 0; k < Hands[i]; k++)
                    {
                        output.Add(Transfer(i, k));
                    }
                }
            }

            return output;
        }

        public void CheckGameOver()
        {
            if (Hands.GetRange(0, Hands.Count / 2).Sum() == 0)
            {
                IsTerminal = true;
                Value = 1;
            }
            else if (Hands.GetRange(Hands.Count / 2, Hands.Count/2).Sum() == 0)
            {
                IsTerminal = true;
                Value = -1;
            }
        }
    }
}
