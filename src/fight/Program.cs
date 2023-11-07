const string m1 = "m1.txt";
const string m2 = "m2.txt";

TicTacToe game = TicTacToe.New();

while(true)
{
    Thread.Sleep(500);

    try {
        if(game.PlayerOneTurn)
            get(m1, m2);
        else 
            get(m2, m1);
    } catch {}

    if(game.GameOver)
        break;
}

string winner = game.Winner == 0 ? "( X )" : "( O )";
System.Console.WriteLine("Game Over, winner : " + winner);


void get(string path, string other)
{
    if(!File.Exists(path))
        return;

    var text = File.ReadAllText(path);


    if (!byte.TryParse(text, out byte pos))
        return;

    if (!game.TryPlay(pos))
        return;

    File.Delete(path);
    File.WriteAllText("[OUTPUT]" + other, pos.ToString());

    System.Console.WriteLine(game);
}