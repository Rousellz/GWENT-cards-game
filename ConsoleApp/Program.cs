// See https://aka.ms/new-console-template for more information


using GWENT_Logic;


Card[] deck1 = new Card[] {
new Card("A", 2), new Card("B", 5), new Card("C", 2), new Card("D", 4), 
new Card("E", 7), new Card("F", 3), new Card("G", 1), new Card("H", 6),
new Card("A1", 2), new Card("B1", 5), new Card("C1", 2), new Card("D1", 4),
new Card("E1", 7), new Card("F1", 3), new Card("G1", 1), new Card("H1", 6),
};
Card[] deck2 = new Card[] {
new Card("2A", 2), new Card("B", 5), new Card("C", 2), new Card("D", 4),
new Card("2E", 7), new Card("F", 3), new Card("G", 1), new Card("H", 6),
new Card("2A1", 2), new Card("B1", 5), new Card("C1", 2), new Card("D1", 4),
new Card("2E1", 7), new Card("F1", 3), new Card("G1", 1), new Card("H1", 6),
};

JugadorHumano ply1 = new JugadorHumano("ply1", deck1);
JugadorHumano ply2 = new JugadorHumano("ply2", deck2);

JuegoGWENT juego = new(ply1,ply2) ;

//juego.starGame();

Console.WriteLine("deck");
Show(juego.Deck[ply1]);
Console.WriteLine("hand");
Show(juego.Hand[ply1]);
Console.WriteLine("graveyard");
Show(juego.Graveyard[ply1]);
//Console.WriteLine("mano");
//Show(juego.hand[ply1]);
//juego.ShufflingCards(ply1); 
//juego.StarGame();


juego.DrawCard(ply1, 4);
juego.Destroy(ply1, juego.Hand[ply1][0]);
Console.WriteLine("deck");
Show(juego.Deck[ply1]);
Console.WriteLine("hand");
Show(juego.Hand[ply1]);
Console.WriteLine("graveyard");
Show(juego.Graveyard[ply1]);
/*Console.WriteLine("mano");
Show(juego.hand[ply1]);*/
juego.RunNextMove();
static void Show(ICollection<Card> cards)
{
    foreach (Card card in cards)
        Console.WriteLine(card);

}