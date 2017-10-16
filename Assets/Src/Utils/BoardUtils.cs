using System;
using UnityEngine;

public static class BoardUtils
{
    public static bool IsInsideBoard(Vector2 position)
    {
        return position.x >= 0 &&
               position.y >= 0 &&
               position.x < (GameData.CurrentBattle.Board.Width) &&
               position.y < (GameData.CurrentBattle.Board.Height);
    }

    public static bool IsPositionEmpty(Vector2 position)
    {
        if (!IsInsideBoard(position))
            return false;
        
        return GameData.CurrentBattle.Board.GetObjectAt(position) == null;
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

        int boardHeight = GameData.CurrentBattle.Board.Height;
        point.y = boardHeight - point.y - 1;

        return point;
    }

    public static Vector3 BoardToWorldPosition(Vector2 position, int z = -1)
    {
        return new Vector3(position.x + 0.5f, GameData.CurrentBattle.Board.Height - position.y - 0.5f, z);
    }
}