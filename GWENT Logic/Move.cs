namespace GWENT_Logic
{
    public class Move
    {
        public Card Card { get; private set; }
        public int[] AditionalData { get; private set; }

        public bool PassOrGive { get; private set; }

        public Move(bool passOrGive)
        {
            PassOrGive = passOrGive;

        }

        public Move(Card card, int[] aditionalData)
        {
            Card = card;
            AditionalData = aditionalData;
        }
    }
}