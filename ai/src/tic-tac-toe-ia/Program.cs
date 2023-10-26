using System;

TicTacToe game = TicTacToe.New();
System.Console.WriteLine(game);

game.Play(0, 0);

game.Play(0, 2);

game.Play(1, 0);

game.Play(1, 2);

game.Play(2, 0);

System.Console.WriteLine("\n" + game);
System.Console.WriteLine(game.GameEnded);
System.Console.WriteLine(game.winner);
