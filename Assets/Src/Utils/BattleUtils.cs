using System;
using UnityEngine;
using System.Collections.Generic;

public static class BattleUtils
{
    public static bool CanMoveAndAttack(BattleActor actor, BattleObject target, out Vector2 movePattern, out Vector2 attackPattern)
    {
        List<Vector2> moveLocations = actor.Type.Pattern.GetLocationsForFlags(PatternFlags.Movement);
        moveLocations.Add(Vector2.zero);

        List<Vector2> attackLocations = actor.Type.Pattern.GetLocationsForFlags(PatternFlags.Attack);
        Vector2 actorPos = actor.Position;
        Vector2 targetPos = target.Position;

        foreach (Vector2 movePos in moveLocations)
        {
            foreach (Vector2 attackPos in attackLocations)
            {
                if (movePos + attackPos + actorPos == targetPos &&
                    actor.CanMoveTo(actor.Position + movePos))
                {
                    movePattern = movePos;
                    attackPattern = attackPos;
                    return true;
                }
            }
        }

        movePattern = Vector2.zero;
        attackPattern = Vector2.zero;
        return false;
    }
}