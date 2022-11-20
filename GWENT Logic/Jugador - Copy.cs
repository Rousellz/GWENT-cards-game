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
        public override Move Play()
        {
            throw new NotImplementedException();
        }
    }
}
