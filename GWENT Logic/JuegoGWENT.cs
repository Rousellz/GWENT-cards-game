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
        public Jugador player1;
        public Jugador player2;

        public Dictionary<Jugador, int> ScoreTable { get; private set; }


        public int Turn { get; private set; }
        public Jugador PlayerInTurn { get; private set; }
        public Dictionary<Jugador, bool> IsGived { get; private set; }
        public Dictionary<Jugador, bool> HasPlayed { get; private set; }
        public bool IsFinished
        {
            get => IsGived[player1] && IsGived[player2];
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
            this.player1 = player1;
            this.player2 = player2;
            PlayerInTurn = ((new Random().Next(0, 1) == 0) ? player1 : player2);
        }



        public void StarGame()
        {
            Console.WriteLine("StarGame");
            ShufflingCards(player1);
            ShufflingCards(player2);
            DrawCard(player1, 10);
            DrawCard(player2, 10);

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
            
           
             if (move.Pass)
            {
                if (!HasPlayed[PlayerInTurn]) IsGived[PlayerInTurn] = true;
                return;
            }
          
            Card card = move.Card;
            int[] AD = move.AditionalData;
            if (card.Type == Card.CardType.Especial)
                Destroy(PlayerInTurn, card);
            
            else
            {
                Hand[PlayerInTurn].Remove(card);
                Field[PlayerInTurn][AD[0], AD[1]] = card;
            }
            Compila(card.Efect(AD));

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
                score += item.Power;
            }
            return score;
        }

        public bool Destroy(Jugador player, Card card)
        {
            Console.WriteLine("Destroy");
            bool discard = Hand[player].Remove(card) || Deck[player].Remove(card);
            if (discard)
                Graveyard[player].Add(card);
            return discard;
        }
        public bool DrawCard(Jugador player, int n)
        {
            Console.WriteLine("DrawCard");
            n =Math.Min(n, Hand[player].Count-8);
            
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
            int ply1Score = Score(player1);
            int ply2Score = Score(player2);
            if (ply1Score > ply2Score) ScoreTable[player1]++;
        }

        private void PenalizePlayer(Jugador player)
        {
            InvalidMoveCommitted = true;
            ScoreTable[((player == player1) ? player2 : player1)]++;
        }

        private bool IsAValidMove(Move move)
        {
            return Hand[PlayerInTurn].Contains(move.Card);

           
            
        }


        public IEnumerator<Move> GetEnumerator()
        {
            if (IsFinished || InvalidMoveCommitted) throw new InvalidOperationException("Este juego ya se ha terminado");
            while (!IsFinished)
            {
                Turn++;


                yield return PlayerInTurn.Play(); ;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}