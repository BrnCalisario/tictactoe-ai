namespace test;

public class PlayTests
{
    [Fact]
    public void SinglePlay()
    {
        TicTacToe game = TicTacToe.New();

        game.Play(0, 0);

        Assert.False(game.GameOver);
        Assert.Equal(2, game.Winner);
    }

    [Fact]
    public void PlayerTwoWins()
    {
        TicTacToe game = TicTacToe.New();

        game.Play(0, 2);
        Assert.False(game.GameOver);
        
        game.Play(0, 0);
        Assert.False(game.GameOver);

        game.Play(1, 2);
        Assert.False(game.GameOver);

        game.Play(1, 0);
        Assert.False(game.GameOver);

        game.Play(0, 1);
        Assert.False(game.GameOver);

        game.Play(2, 0);


        Assert.True(game.GameOver);
        Assert.Equal(1, game.Winner);
    }

    [Fact]
    public void FullGame()
    {
        TicTacToe game = TicTacToe.New();

        game.Play(0, 0);
        Assert.False(game.GameOver);

        game.Play(1, 1);
        Assert.False(game.GameOver);

        game.Play(1, 0);
        Assert.False(game.GameOver);

        game.Play(2, 2);
        Assert.False(game.GameOver);

        game.Play(2, 0);

        Assert.True(game.GameOver);
        Assert.Equal(0, game.Winner);
    }

    [Fact]
    public void RepeatMove()
    {
        TicTacToe game = TicTacToe.New();

        game.Play(0, 0);

        Assert.ThrowsAny<Exception>(() => game.Play(0, 0));
    }

    [Fact]
    public void TryInvalidMove()
    {
        TicTacToe game = TicTacToe.New();

        game.Play(0, 0);

        var result = game.TryPlay(0, 0);

        Assert.False(result);
    }

    [Fact]
    public void TryValidMove()
    {
        TicTacToe game = TicTacToe.New();

        game.Play(0, 0);

        var result = game.TryPlay(1, 0);

        Assert.True(result);
    }

    [Fact]
    public void HorizontalWin()
    {
        TicTacToe game = TicTacToe.New();

        game.Play(0, 0);
        Assert.False(game.GameOver);
        
        game.Play(0, 2);
        Assert.False(game.GameOver);

        game.Play(1, 0);
        Assert.False(game.GameOver);

        game.Play(1, 2);
        Assert.False(game.GameOver);

        game.Play(2, 0);

        Assert.True(game.GameOver);
        Assert.Equal(0, game.Winner);
    }

    [Fact]
    public void VerticalWin()
    {
        TicTacToe game = TicTacToe.New();

        game.Play(0, 0);
        Assert.False(game.GameOver);
        
        game.Play(2, 0);
        Assert.False(game.GameOver);

        game.Play(0, 1);
        Assert.False(game.GameOver);

        game.Play(2, 1);
        Assert.False(game.GameOver);

        game.Play(0, 2);

        Assert.True(game.GameOver);
        Assert.Equal(0, game.Winner);
    }

    [Fact]
    public void DiagonalWin()
    {
        TicTacToe game = TicTacToe.New();

        game.Play(0, 0);
        Assert.False(game.GameOver);
        
        game.Play(0, 2);
        Assert.False(game.GameOver);

        game.Play(1, 1);
        Assert.False(game.GameOver);

        game.Play(1, 0);
        Assert.False(game.GameOver);

        game.Play(2, 2);
        

        Assert.True(game.GameOver);
        Assert.Equal(0, game.Winner);
    }

    [Fact]
    public void GetPossibleMoves()
    {
        var game = TicTacToe.New();

        game.Play(1, 1);
        
        var next = game.NextMoves();

        Assert.Equal(8, next.Count());
    }
}   