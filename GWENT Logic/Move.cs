namespace GWENT_Logic
{
    public class Move
    {
        public Card Card { get; private set; }
        public int[] AditionalData { get; private set; }

        public bool Pass { get; private set; }

        public Move(bool pass)
        {
         
             Pass = pass;

        }

        public Move(Card card, int[] aditionalData)
        {
            Card = card;
            AditionalData = aditionalData;
        }
    }
}