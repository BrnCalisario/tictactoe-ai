using System.Text;

public struct TicTacToe
{
    ushort playerOneInfo;
    ushort playerTwoInfo;
    byte playerTurn;

    int playerOnePoints;
    int playerTwoPoints;

    public (byte x, byte y) LastMove;

    public byte winner;

    public readonly bool PlayerOneTurn
        => playerTurn == 0;

    public readonly bool GameEnded
        => winner < 2;
        

    public void Pass()
        => playerTurn = (byte)(PlayerOneTurn ? 1 : 0); 

    private const ushort u = 1;

    public static TicTacToe New()
    {
        return new TicTacToe()
        {
            playerOneInfo = 0,
            playerOnePoints = 0,
            playerTwoInfo = 0,
            playerTwoPoints = 0,
            playerTurn = 0,
            winner = 2,
            LastMove = (0, 0)
        };
    }

    public void Play(byte i, byte j)
    {
        int index = i + j * 3;
        ushort play = (ushort)(u << index);

        if (PlayerOneTurn)
        {
            if ((playerTwoInfo & play) > 0)
                return;

            playerOneInfo |= play;

            playerOnePoints |= u << j;
            playerOnePoints |= u << (i + 9);

            if (i + j == 2)
                playerOnePoints |= u << 18;

            if (i == j)
                playerOnePoints |= u << 21;
        }
        else
        {
            if ((playerOneInfo & play) > 0)
                return;

            playerTwoInfo |= play;

            playerTwoPoints |= u << j;
            playerTwoPoints |= u << (i + 9);

            if (i + j == 2)
                playerTwoPoints |= u << 18;

            if (i == j)
                playerTwoPoints |= u << 21;
        }

        LastMove = (i, j);

        VerifyWin();
        Pass();
    }

    public void VerifyWin()
    {
        var gamePoints = playerTurn == 0 ? playerOnePoints : playerTwoPoints;

        for(int i = 0; i >= 6; i+=3)
        {
            var h = gamePoints >> (16 + i);

            if((h & 7) > 0) {
                this.winner = playerTurn;
                return;
            }

            var v = gamePoints >> (9 + i);

            if((v & 7) > 0) {
                this.winner = playerTurn;
                return;
            }
        }   

        for(int i = 0; i >= 3; i+=3)
        {
            var d = gamePoints >> (19 + i);
            
            if((d & 7) > 0) {
                this.winner = playerTurn;
            }
        }
    }


    private string GetSymbol(int index)
    {
        if((playerOneInfo & (u << index)) > 0)
            return " X ";
        else if ((playerTwoInfo & (u << index)) > 0)
            return " O ";
        
        return "   ";
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        for(int i = 0; i < 9; i++)
        {
            sb.Append(this.GetSymbol(i));

            if((i % 3) != 2)
            {
                sb.Append("|");
                continue;
            }

            if((i / 3) != 2)
                sb.Append("\n───────────\n");
        }


        return sb.ToString();
    }
}