namespace GWENT_Logic
{
    public class Move
    {
        public Card Card { get; private set; }
        public (int, int) Position { get; private set; }
        

        public Move(Card card, (int, int) position)
        {
            Card = card;
            Position = position;
        }
    }
}