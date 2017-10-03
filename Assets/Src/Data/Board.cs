using System;
using UnityEngine;

public class Board
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int NeutralSize { get; private set; }
    public GameObject GameObject { get; set; }
    public BattleActor[,] ActorGrid { get; private set; }

    public Board(BattleConfig config)
    {
        Width = config.BoardWidth;
        Height = config.Height;
        NeutralSize = config.NeutralWidth;

        ActorGrid = new BattleActor[Width, Height];
    }

    public BattleActor GetActorAt(Vector2 position)
    {
        return ActorGrid[(int)position.x, (int)position.y];
    }

    public void SetActorAt(BattleActor actor, Vector2 position)
    {
        ActorGrid[(int)position.x, (int)position.y] = actor;
    }
}
