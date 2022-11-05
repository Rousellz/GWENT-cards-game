using System;
using System.Reflection;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace GWENT_Logic
{
    public class JuegoGWENT
    {

        public Dictionary<Jugador, Card[,]> field;
        public Dictionary<Jugador, List<Card>> hand;
        public Dictionary<Jugador, List<Card>> deck;
        public Dictionary<Jugador, List<Card>> graveyard;
        public Jugador player1;
        public Jugador player2;


        public JuegoGWENT(Jugador player1, Jugador player2)
        {
            field = new Dictionary<Jugador, Card[,]>
            {
                [player1] = new Card[2, 5],
                [player2] = new Card[2, 5]
            };
            hand = new Dictionary<Jugador, List<Card>>
            {
                [player1] = new List<Card>(),
                [player2] = new List<Card>()
            };
            graveyard = new Dictionary<Jugador, List<Card>>
            {
                [player1] = new List<Card>(),
                [player2] = new List<Card>()
            };
            deck = new Dictionary<Jugador, List<Card>>
            {
                [player1] = player1.Deck.ToList(),
                [player2] = player2.Deck.ToList()
            };
            this.player1 = player1;
            this.player2 = player2;
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
            deck[player] = deck[player].OrderBy(x => new Random().Next(0, deck[player].Count)).ToList();
        }
        static void Show(ICollection<Card> cards)
        {
            foreach (Card card in cards)
                Console.WriteLine(card);
        }
        
        public void Play(Jugador player, Card card, int[] n)
        {
            Compila(card.Efect(n)); 
            hand[player].Remove(card);

            if (card.Type != Card.CardType.Especial)
                field[player][n[0], n[1]] = card;
        }

        private void Compila(string v)
        {
            throw new NotImplementedException();
        }

        public int Score(Jugador player)
        {
            int score = 0;
            foreach (Card item in field[player])
            {
                score += item.Power;
            }
            return score;
        }
        
        public bool Discard(Jugador player,Card card)
        {
            Console.WriteLine("Discard");
            bool discard = hand[player].Remove(card) || deck[player].Remove(card);
            if (discard)
                graveyard[player].Add(card);
            return discard;
        }
        public bool DrawCard(Jugador player, int n)
        {
            Console.WriteLine("DrawCard");
            if (deck[player].Count < n) return false;
            else
            {
                hand[player].AddRange(deck[player].TakeLast(n));
                deck[player].RemoveRange(deck[player].Count - 1 - n, n);
                return true;
            }
        }

        /* public override bool EstaTerminado => throw new NotImplementedException();

         protected override List<Jugada> PosiblesJugadas => throw new NotImplementedException();

         protected override Jugador JugadorEnTurno => throw new NotImplementedException();

         protected override void ActualizarTablaPuntuacion()
         {
             throw new NotImplementedException();
         }

         protected override void EjecutarJugada(Jugada jug)
         {
             throw new NotImplementedException();
         }

         protected override bool JugadaEsValida(Jugada jug)
         {
             throw new NotImplementedException();
         }*/
    }

}