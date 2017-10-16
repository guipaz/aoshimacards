using System;
using UnityEngine;

public class Board
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int NeutralSize { get; private set; }
    public GameObject GameObject { get; set; }
    public BattleObject[,] ObjectGrid { get; private set; }

    public Board()
    {
        Width = 11;
        Height = 7;
        NeutralSize = 3;

        ObjectGrid = new BattleObject[Width, Height];
    }

    public BattleActor GetActorAt(Vector2 position)
    {
        BattleObject obj = GetObjectAt(position);
        if (obj is BattleActor)
            return obj as BattleActor;
        return null;
    }

    public BattleObject GetObjectAt(Vector2 position)
    {
        return ObjectGrid[(int)position.x, (int)position.y];
    }

    public void SetObjectAt(BattleObject obj, Vector2 position)
    {
        ObjectGrid[(int)position.x, (int)position.y] = obj;
    }
}
