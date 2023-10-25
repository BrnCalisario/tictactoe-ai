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
        foreach(var child in Children)
        {
            var last = child.State.LastMove;
            if(last.x == x && last.y == y)
                return child;
        }

        return null;
    }


    public Node PlayBest()
    {
        return Children.MaxBy(n => 
            YourTurn ? n.Score : -n.Score);
    }
}