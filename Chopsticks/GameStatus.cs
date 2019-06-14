using MonteCarloLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chopsticks
{
    [DebuggerDisplay("{Hands[0]}, {Hands[1]}, {Hands[2]}, {Hands[3]}")]
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

        //de1337 later
        public GameStatus Parent;

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

            CheckGameOver();
        }
        public GameStatus(int[] hands, bool maximizer, GameStatus parent)
        {
            Hands = new List<int>(hands);
            Maximizer = maximizer;
            moves = null;

            Parent = parent;

            CheckGameOver();
        }

        public GameStatus Attack(int move, int hand)
        {
            int[] hands = Hands.ToArray();

            hands[move] += hands[hand];

            if (hands[hand] < 0)
            {
                ;//uh oh
            }

            if (hands[move] > 4)
            {
                hands[move] = 0;
            }

            return new GameStatus(hands, !Maximizer, this);
        }
        public GameStatus Transfer(int move, int hand, int amount)
        {
            int[] hands = Hands.ToArray();

            hands[move] += amount;
            hands[hand] -= amount;

            if (hands[hand] < 0)
            {
                ;//uh oh
            }

            if (hands[move] > 4)
            {
                hands[move] = 0;
            }

            return new GameStatus(hands, !Maximizer, this);
        }

        public List<GameStatus> GenerateMoves()
        {
            if (IsTerminal)
            {
                return new List<GameStatus>();
            }

            List<GameStatus> output = new List<GameStatus>();

            //Hands = new List<int>() { 1, 1, 2, 2 };

            if (Maximizer) //hands 0 to count/2
            {
                //attacks
                for (int i = 0; i < Hands.Count / 2; i++) //hands.count is guaranteed to be an even number
                {      
                    if (Hands[i + Hands.Count / 2] == 0) continue;

                    for (int j = 0; j < Hands.Count / 2; j++)
                    {
                        if (Hands[j] == 0) continue;

                        var hi = Attack(i + Hands.Count / 2, j);

                        if (hi.Hands.SequenceEqual(Hands))
                        {
                            ;
                        }

                        output.Add(hi);
                    }
                }

                //transfers
                for (int i = 0; i < Hands.Count / 2; i++)
                {
                    for (int j = 0; j < Hands.Count / 2; j++)
                    {
                        if (j == i) continue;

                        for (int k = 1; k <= Hands[j]; k++)
                        {
                            if (Hands[i] == 0 && k == Hands[j]) continue;

                            var result = Transfer(i, j, k);
                            var copy = result.Hands.ToArray();
                            ;
                            //if (result.Hands.SequenceEqual(Hands))

                            output.Add(result);
                            var derp = Transfer(i, j, k);
                            ;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Hands.Count / 2; i++)
                {
                    if (Hands[i] == 0) continue;

                    for (int j = 0; j < Hands.Count / 2; j++)
                    {
                        if (Hands[j + Hands.Count / 2] == 0) continue;

                        output.Add(Attack(i, j + Hands.Count / 2));
                    }
                }

                for (int i = Hands.Count / 2; i < Hands.Count; i++)
                {
                    for (int j = Hands.Count / 2; j < Hands.Count; j++)
                    {
                        if (j == i) continue;

                        for (int k = 1; k <= Hands[j]; k++)
                        {
                            if (Hands[i] == 0 && k == Hands[j]) continue;

                            output.Add(Transfer(i, j, k));
                        }
                    }
                }

            }

            return output;
        }

        public void CheckGameOver()
        {
            // Hands = new List<int>(new int[]{ 0, 0, 2, 2});

            Value = 0;
            IsTerminal = false;

            if (Hands.GetRange(0, Hands.Count / 2).Sum() == 0)
            {
                IsTerminal = true;
                Value = -1;
            }
            else if (Hands.GetRange(Hands.Count / 2, Hands.Count / 2).Sum() == 0)
            {
                IsTerminal = true;
                Value = 1;
            }
        }
    }
}
