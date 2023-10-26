namespace test;

public class PlayTests
{
    [Fact]
    public void SinglePlay()
    {
        TicTacToe game = TicTacToe.New();
        game.Play(0, 0);
    }
}