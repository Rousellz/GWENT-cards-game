using System;
using System.Reflection;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Collections;

namespace GWENT_Logic
{
    public class JuegoGWENT : IEnumerable<Move>
    {


        public Dictionary<Jugador, Card[,]> Field { get; private set; }
        public Dictionary<Jugador, List<Card>> Hand { get; private set; }
        public Dictionary<Jugador, List<Card>> Deck { get; private set; }
        public Dictionary<Jugador, List<Card>> Graveyard { get; private set; }
        public Jugador Player1 { get; private set; }
        public Jugador Player2 { get; private set; }

        public Dictionary<Card, int> ActualPower { get; private set; } = new Dictionary<Card, int>();
        public Dictionary<Jugador, int> ScoreTable { get; private set; }


        public int Turn { get; private set; }
        public Jugador PlayerInTurn { get; private set; }
        public Dictionary<Jugador, bool> IsGived { get; private set; }
        public Dictionary<Jugador, bool> HasPlayed { get; private set; }
        public bool IsFinished
        {
            get => ScoreTable[Player1] == 3 || ScoreTable[Player1] == 3;
            private set
            {
            }
        }
        public bool InvalidMoveCommitted { get; private set; }

        public JuegoGWENT(Jugador player1, Jugador player2)
        {
            Field = new Dictionary<Jugador, Card[,]>
            {
                [player1] = new Card[2, 5],
                [player2] = new Card[2, 5]
            };
            Hand = new Dictionary<Jugador, List<Card>>
            {
                [player1] = new List<Card>(),
                [player2] = new List<Card>()
            };
            Graveyard = new Dictionary<Jugador, List<Card>>
            {
                [player1] = new List<Card>(),
                [player2] = new List<Card>()
            };
            Deck = new Dictionary<Jugador, List<Card>>
            {
                [player1] = player1.Deck.ToList(),
                [player2] = player2.Deck.ToList()
            };
            IsGived = new Dictionary<Jugador, bool>
            {
                [player1] = false,
                [player2] = false,
            };
            HasPlayed = new Dictionary<Jugador, bool>
            {
                [player1] = false,
                [player2] = false,
            };
            ScoreTable = new Dictionary<Jugador, int>
            {
                [player1] = 0,
                [player2] = 0,
            };
            this.Player1 = player1;
            this.Player2 = player2;
            PlayerInTurn = ((new Random().Next(0, 1) == 0) ? Player1 : Player2);

        }



        public void StarGame()
        {
            Console.WriteLine("StarGame");
            ShufflingCards(Player1);
            ShufflingCards(Player2);
            DrawCard(Player1, 8);
            DrawCard(Player2, 8);

        }



        public void ShufflingCards(Jugador player)
        {
            Console.WriteLine("ShufflingCards");
            Deck[player] = Deck[player].OrderBy(x => new Random().Next(0, Deck[player].Count)).ToList();
        }
        static void Show(ICollection<Card> cards)
        {
            foreach (Card card in cards)
                Console.WriteLine(card);
        }

        public void ExecuteMove(Move move)
        {

            Card card = move.Card;
            if (card.Name == "")
            {
                IsGived[PlayerInTurn] = true;
               
                
                if(IsGived[Player1] && IsGived[Player2])
                {
                    NewRound();
                }
                return;
            }

            if (card.Type == Card.CardType.Especial)
                Destroy(card);

            else
            {
                Hand[PlayerInTurn].Remove(card);
                Field[PlayerInTurn][move.Position.Item1, move.Position.Item2] = card;
                ActualPower[card] = card.Power;
            }
            Compila(card.Efect());
            DestroyDeathCards();
        }

        private void NewRound()
        {
            PlayerInTurn = ((new Random().Next(0, 1) == 0) ? Player1 : Player2);
            Turn = 0;
            foreach (Jugador player in new Jugador[] { Player1, Player2 })
            {
                IsGived[player] = false;
                DrawCard(player, 3);
                for (int i = 0; i < Field[player].GetLength(0); i++)
                    for (int j = 0; j < Field[player].GetLength(1); j++)
                        Destroy(Field[player][i, j]);
            }
        }

        void DestroyDeathCards()
        {
            
            foreach (Jugador player in new Jugador[] { Player1, Player2 })
            {
                for (int i = 0; i < Field[player].GetLength(0); i++)
                    for (int j = 0; j < Field[player].GetLength(1); j++)
                        if (Field[player][i, j].Power <= 0)
                        {
                            Destroy(Field[player][i, j]);
                        }
            }
        }
        private void Compila(string v)
        {
            //throw new NotImplementedException();
        }

        public int Score(Jugador player)
        {
            int score = 0;
            foreach (Card item in Field[player])
            {
                score += ActualPower[item];
            }
            return score;
        }

        public bool Destroy(Card card)
        {
            Console.WriteLine("Destroy");

            bool finded = false;
            foreach (Jugador player in new Jugador[] { Player1, Player2 })
            {
                finded = Hand[player].Remove(card) || Deck[player].Remove(card);
                for (int i = 0; i < Field[player].GetLength(0); i++)
                    for (int j = 0; j < Field[player].GetLength(1); j++)
                        if (Field[player][i, j] == card)
                        {
                            Field[player][i, j] = null;
                            finded = true;
                        }

                if (finded)
                    Graveyard[player].Add(card);

            }
            return finded;
        }

        public bool DrawCard(Jugador player, int n)
        {
            Console.WriteLine("DrawCard");
            n = Math.Min(n, Hand[player].Count - 8);

            if (Deck[player].Count < n) return false;

            else
            {
                Hand[player].AddRange(Deck[player].TakeLast(n));
                Deck[player].RemoveRange(Deck[player].Count - 1 - n, n);
                return true;
            }
        }
        public void RunNextMove()
        {

            foreach (Move move in this)
            {
                if (!IsAValidMove(move)) PenalizePlayer(PlayerInTurn);
                else ExecuteMove(move);
                break;
            }
            if (IsFinished && !InvalidMoveCommitted) UpdateScoreTable();
        }

        public void RunToTheEnd()

        {
            foreach (Move move in this)
            {
                if (!IsAValidMove(move))
                {
                    PenalizePlayer(PlayerInTurn);
                    break;
                }
                else ExecuteMove(move);
            }
            if (IsFinished && !InvalidMoveCommitted) UpdateScoreTable();
        }
        private void UpdateScoreTable()
        {
            int ply1Score = Score(Player1);
            int ply2Score = Score(Player2);
            if (ply1Score > ply2Score) ScoreTable[Player1]++;
            else if (ply1Score < ply2Score) ScoreTable[Player2]++;
            else
            {
                ScoreTable[Player1]++;
                ScoreTable[Player2]++;
            }
        }
        
        private void PenalizePlayer(Jugador player)
        {
            InvalidMoveCommitted = true;
            ScoreTable[((player == Player1) ? Player2 : Player1)]++;
        }

        private bool IsAValidMove(Move move)
        {
            if (!Hand[PlayerInTurn].Contains(move.Card)) Console.WriteLine("This is not a valid move");
            return Hand[PlayerInTurn].Contains(move.Card);
        }


        public IEnumerator<Move> GetEnumerator()
        {
            if (IsFinished || InvalidMoveCommitted) throw new InvalidOperationException("Este juego ya se ha terminado");
            while (!IsFinished)
            {
                Turn++;

                Jugador other = ((PlayerInTurn == Player2) ? Player1 : Player2);
                if (!IsGived[other]) PlayerInTurn = other;

                yield return PlayerInTurn.Play(this, Hand[PlayerInTurn]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}