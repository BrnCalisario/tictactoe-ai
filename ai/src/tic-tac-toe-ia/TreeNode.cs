using System;
using System.Collections.Generic;
using System.Linq;

public class Node
{
    public TicTacToe State { get; set; }

    public float Score { get; set; }
    public List<Node> Children { get; set; } = new();

    public bool Expanded { get; set; } = false;

    public bool YourTurn { get; set; } = true;


    public Node Play(byte x, byte y)
    {
        foreach (var child in Children)
        {
            var last = child.State.LastMove;
            if (last.x == x && last.y == y)
                return child;
        }

        return null;
    }

    public Node PlayBest()
    {
        return Children.MaxBy(n =>
            YourTurn ? n.Score : -n.Score);
    }


    public void Expand(int deep)
    {
        if (deep == 0)
            return;

        if (!this.Expanded)
        {
            var next = State.NextMoves();

            foreach (var move in next)
            {
                var clone = move.Clone();

                var node = new Node
                {
                    State = clone,
                    YourTurn = !this.YourTurn
                };

                this.Children.Add(node);
                this.Expanded = true;
            }
        }

        foreach (var child in this.Children)
            child.Expand(deep - 1);
    }


    public float AlphaBeta() => this.AlphaBetaPruning(float.NegativeInfinity, float.PositiveInfinity);

    float AlphaBetaPruning(float alpha, float beta)
    {
        if (this.Children.Count == 0)
        {
            this.Score = aval();
            return this.Score;
        }

        float value;

        if(YourTurn)
        {
            value = float.NegativeInfinity;
            foreach(var child in Children)
            {
                value = float.Max(value, child.AlphaBetaPruning(alpha, beta));

                if(value > beta)
                    break;
                
                beta = float.Max(beta, value);
            }
        }
        else
        {
            value = float.PositiveInfinity;

            foreach(var child in Children)
            {
                value = float.Min(value, child.AlphaBetaPruning(alpha, beta));

                if(value < alpha)
                    break;

                alpha = float.Max(alpha, value);
            }
        }

        this.Score = value;
        return value;
    }


    float aval()
    {
        if(State.GameOver)
            return State.PlayerOneTurn ? float.NegativeInfinity : float.PositiveInfinity;

        return Random.Shared.NextSingle();
    }
}