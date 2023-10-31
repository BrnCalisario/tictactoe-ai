using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

public struct TicTacToe
{
    ushort playerOneInfo;
    ushort playerTwoInfo;
    byte playerTurn;

    int playerOnePoints;
    int playerTwoPoints;

    public (byte x, byte y) LastMove;

    public byte Winner; // 0 - 1

    public readonly bool PlayerOneTurn
        => playerTurn == 0;

    public readonly bool GameOver
        => Winner < 2;


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
            Winner = 2,
            LastMove = (0, 0)
        };
    }


    public void Play(byte i, byte j)
    {
        int index = i + j * 3;

        if (index < 0 || index > 9)
            throw new Exception("Invalid Move");

        ushort play = (ushort)(u << index);

        (ushort info, ushort enemyInfo, int points) = ExtractInfo();

        if ((enemyInfo & play) > 0 || (info & play) > 0)
            throw new Exception("Move already played");

        info |= play;

        points |= u << j;
        points |= u << (i + 9);

        if (i + j == 2)
            points |= u << 18;

        if (i == j)
            points |= u << 21;

        AssignInfo(info, points);

        LastMove = (i, j);

        VerifyWin();
        Pass();
    }

    public void Play(int index)
    {
        byte line = (byte)(index / 3);
        byte coll = (byte)(index % 3);

        Play(coll, line);
    }

    private (ushort info, ushort enemyInfo, int points) ExtractInfo()
    {
        ushort info = PlayerOneTurn ? playerOneInfo : playerTwoInfo;
        ushort enemyInfo = PlayerOneTurn ? playerTwoInfo : playerOneInfo;
        int points = PlayerOneTurn ? playerOnePoints : playerTwoPoints;

        return (info, enemyInfo, points);
    }

    private void AssignInfo(ushort newInfo, int newPoints)
    {
        if (PlayerOneTurn)
        {
            playerOneInfo = newInfo;
            playerOnePoints = newPoints;
            return;
        }

        playerTwoInfo = newInfo;
        playerTwoPoints = newPoints;
    }

    public bool TryPlay(byte i, byte j)
    {
        try
        {
            Play(i, j);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public bool TryPlay(byte index)
    {
        try
        {
            Play(index);
        }
        catch
        {
            return false;
        }
        return true;
    }

    private void VerifyWin()
    {
        var gamePoints = playerTurn == 0 ? playerOnePoints : playerTwoPoints;

        for (int i = 0; i <= 6; i += 3)
        {
            var h = gamePoints >> i;

            if ((h & 7) == 7)
            {
                Winner = playerTurn;
                return;
            }

            var v = gamePoints >> (9 + i);

            if ((v & 7) == 7)
            {
                Winner = playerTurn;
                return;
            }
        }

        for (int i = 0; i <= 3; i += 3)
        {
            var d = gamePoints >> (19 + i);

            if ((d & 7) == 7)
            {
                Winner = playerTurn;
            }
        }
    }

    private string GetSymbol(int index)
    {
        if ((playerOneInfo & (u << index)) > 0)
            return " X ";
        else if ((playerTwoInfo & (u << index)) > 0)
            return " O ";

        return "   ";
    }

    public IEnumerable<TicTacToe> NextMoves()
    {
        TicTacToe copy = this.Clone();

        if(GameOver)
            yield break;

        for (byte i = 0; i < 9; i++)
        {
            if (!copy.TryPlay(i))
                continue;

            yield return copy;

            copy = this.Clone();
        }
    }


    public TicTacToe Clone()
    {
        return new TicTacToe
        {
            playerOneInfo = this.playerOneInfo,
            playerOnePoints = this.playerOnePoints,
            playerTwoInfo = this.playerTwoInfo,
            playerTwoPoints = this.playerTwoPoints,
            playerTurn = this.playerTurn,
            LastMove = this.LastMove,
            Winner = this.Winner
        };
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        for (int i = 0; i < 9; i++)
        {
            sb.Append(this.GetSymbol(i));

            if ((i % 3) != 2)
            {
                sb.Append('|');
                continue;
            }

            if ((i / 3) != 2)
                sb.Append("\n───────────\n");
        }


        return sb.ToString();
    }
}
