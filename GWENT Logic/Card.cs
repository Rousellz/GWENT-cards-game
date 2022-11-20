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

            Name = name;
            /* Description = description;
             RecruitmentCost = recruitmentCost;*/
            Power = power;
        }

        public string Efect(int[] n)
        {
            throw new NotImplementedException();
        }

        public CardType Type { get; }
        public string Name { get; }
        public string Description { get; }
        public int RecruitmentCost { get; }
        public int Power { get; }

        public override string ToString()
        {
            return Name + " " + Power;
        }
    }
}
