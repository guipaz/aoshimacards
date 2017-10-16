using System;
using UnityEngine;
using System.Collections.Generic;

[Flags]
public enum PatternFlags
{
	Movement = 1,
	Attack = 2,
}

public class WarriorPattern
{
	PatternFlags[] grid;

	// just 7x7 for now is enough (41 tiles around the warrior)
	const int PATTERN_WIDTH = 7;
	const int PATTERN_HEIGHT = 7;

	public WarriorPattern ()
	{
		grid = new PatternFlags[PATTERN_WIDTH * PATTERN_HEIGHT];
	}

	public void SetFlagsAt(int x, int y, PatternFlags flags)
	{
		grid [GetIndexFromRelativePosition (x, y)] = flags;
	}

    public bool CanAttackAt(Vector2 from, Vector2 to, bool xInverted = false)
	{
        int x = (int)(to.x - from.x) * (xInverted ? -1 : 1);
        int y = (int)(to.y - from.y);

        int index = GetIndexFromRelativePosition(x, y);
        Debug.Log("Tried attacking at index:" + index);

        if (index < 0 || index > grid.Length)
            return false;

        return (grid [index] & PatternFlags.Attack) == PatternFlags.Attack;
	}

	public bool CanMoveTo(Vector2 from, Vector2 to, bool xInverted = false)
	{
        int x = (int)(to.x - from.x) * (xInverted ? -1 : 1);
        int y = (int)(to.y - from.y);

        int index = GetIndexFromRelativePosition(x, y);
        Debug.Log("Tried moving to index:" + index);

        if (index < 0 || index > grid.Length)
            return false;
        
        return (grid [index] & PatternFlags.Movement) == PatternFlags.Movement;
	}

    public List<Vector2> GetLocationsForFlags(PatternFlags flags, bool xInverted = false)
    {
        List<Vector2> locations = new List<Vector2>();
        for (int y = 0; y < PATTERN_HEIGHT; y++)
        {
            for (int x = 0; x < PATTERN_WIDTH; x++)
            {
                if ((grid[y * PATTERN_WIDTH + x] & flags) == flags)
                    locations.Add(GetRelativePosition(x, y, xInverted));
            }
        }
        return locations;
    }

	int GetIndexFromRelativePosition(int x, int y)
	{
        if (Math.Abs(x) > 3 || Math.Abs(y) > 3)
            return -1;

		// center of the 7x7
		int rX = x + 3;
		int rY = y + 3;

		return rY * PATTERN_WIDTH + rX;
	}

    Vector2 GetRelativePosition(int rX, int rY, bool xInverted = false)
    {
        return new Vector2((rX - 3) * (xInverted ? -1 : 1), rY - 3);
    }
}