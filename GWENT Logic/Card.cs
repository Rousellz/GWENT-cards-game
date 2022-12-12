using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWENT_Logic
{
    public class Card
    {
        public enum CardType { Especial, Estructure, Unit }

        public Card(string name, int power)
        {
            if (power <= 0) throw new Exception("Power must be greater than 0");
            Name = name;
            Power = power;
        }

        public string Efect()
        {
            return "";
        }

        public CardType Type { get; }
        public string Name { get; }
        public string Description { get; } = "";
        public int Power { get; }
        public override string ToString()
        {
            return Name + " " + Power;
        }
    }
}
