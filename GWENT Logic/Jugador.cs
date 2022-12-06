using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWENT_Logic
{
    /// <summary> Provee la clase abstracta base para jugadores de juegos de mesa. </summary>
    public abstract class Jugador
    {
        /// <summary> Representa el nombre del jugador. </summary>
        public string Nombre { get; }

        /// <summary> Constructor base de la clase abstracta Jugador. </summary>
        /// <param name="nombre"> Representa el nombre del jugador. </param>
        protected Jugador(string nombre, Card[] deck)
        {
            Nombre = nombre;
            Deck = deck;
        }

        public Card[] Deck { get; }
        /// <summary> Devuelve la jugada seleccionada por el jugador. </summary>
    
        
        public abstract Move Play(JuegoGWENT game, IEnumerable<Card> hand);

        public override string ToString()
        {
            return Nombre;
        }
    }
}
