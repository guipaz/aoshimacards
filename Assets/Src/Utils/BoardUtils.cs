using System;
using UnityEngine;

public static class BoardUtils
{
    public static BattleConfig Current { get { return GameData.CurrentBattle.Config; } }

    public static bool IsInsideBoard(Vector2 position)
    {
        return position.x >= 0 &&
               position.y >= 0 &&
               position.x < (Current.BoardWidth) &&
               position.y < (Current.Height);
    }

    public static bool IsPositionEmpty(Vector2 position)
    {
        if (!IsInsideBoard(position))
            return false;
        
        return GameData.CurrentBattle.Board.GetActorAt(position) == null;
    }

    public static Vector2 ScreenToBoardPosition(Vector3 position)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(position);

        if (point.x < 0)
            point.x--;
        if (point.y < 0)
            point.y--;

        point.x = (int)point.x;
        point.y = (int)point.y;

        int boardHeight = Current.Height;
        point.y = boardHeight - point.y - 1;

        return point;
    }

    public static Vector3 BoardToWorldPosition(Vector2 position, int z = -1)
    {
        return new Vector3(position.x + 0.5f, Current.Height - position.y - 0.5f, z);
    }
}