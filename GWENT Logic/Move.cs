namespace GWENT_Logic
{
    public class Move
    {
        public Card Card { get; private set; }
        public int[] AditionalData { get; private set; }
        public (int, int) Position { get; private set; }
        public bool Pass { get; private set; }

        public Move(Card card, (int, int) position, int[] aditionalData)
        {
            Pass = false;
            Card = card;
            AditionalData = aditionalData;
            Position = position;
        }
    }
}