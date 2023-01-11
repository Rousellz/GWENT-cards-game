// See https://aka.ms/new-console-template for more information


using GWENT_Logic;


Card[] deck1 = new Card[] {
new Card(1), new Card(2), new Card(3), new Card(4),
new Card(5), new Card(6), new Card(7), new Card(8),
new Card(9), new Card(10), new Card(12), new Card(11)};
Card[] deck2 = new Card[] {
new Card(19), new Card(15), new Card(13), new Card(14),
new Card(16), new Card(20), new Card(17), new Card(18),
new Card(21), new Card(22), new Card(23), new Card(24)
};
JugadorHumano ply1 = new JugadorHumano("ply1", deck1);
JugadorHumano ply2 = new JugadorHumano("ply2", deck2);

JuegoGWENT juego = new(ply1,ply2) ;
juego.StarGame();
while (Console.ReadLine() != null)
{

    Console.WriteLine("Player 1");
    Console.WriteLine("deck");
    Show(juego.Deck[ply1]);
    Console.WriteLine("hand");
    Show(juego.Hand[ply1]);
    Console.WriteLine("graveyard");
    Show(juego.Graveyard[ply1]);

    Console.WriteLine("Player 2");
    Console.WriteLine("deck");
    Show(juego.Deck[ply2]);
    Console.WriteLine("hand");
    Show(juego.Hand[ply2]);
    Console.WriteLine("graveyard");
    Show(juego.Graveyard[ply2]);

    juego.RunNextMove();
}
//Console.WriteLine("mano");
//Show(juego.hand[ply1]);
//juego.ShufflingCards(ply1); 
//juego.StarGame();


juego.DrawCard(ply1, 4);
juego.Destroy(juego.Hand[ply1][0]);
Console.WriteLine("deck");
Show(juego.Deck[ply1]);
Console.WriteLine("hand");
Show(juego.Hand[ply1]);
Console.WriteLine("graveyard");
Show(juego.Graveyard[ply1]);
/*Console.WriteLine("mano");
Show(juego.hand[ply1]);*/

static void Show(IEnumerable<Card> cards)
{
    foreach (Card card in cards)
        Console.WriteLine(card);
}