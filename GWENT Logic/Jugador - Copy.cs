using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWENT_Logic
{
    /// <summary> Provee la clase abstracta base para jugadores de juegos de mesa. </summary>
    public class JugadorHumano : Jugador
    {
        public JugadorHumano(string nombre, Card[] deck) : base(nombre, deck)
        {
        }

        /// <summary> Devuelve la jugada seleccionada por el jugador. </summary>

        public override Move Play(JuegoGWENT game, IEnumerable<Card> hand)
        {
            Card card = hand.ElementAt(new Random().Next(0, hand.Count()));
            for (int i = 0; i < 2; i++)
                for (int j = 0; j<5; j++)
                {
                    if (game.Field[this][i, j] == null)
                    {
                        return new Move(card, (i, j));
                    } ;
                }
            return new Move(new Card("",0), (0,0));

        }
    }
}
