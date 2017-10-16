using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThinkerActions
{
    NONE,
    ATTACK,
    MOVE,
}

public class BaseThinker
{
    public ThinkerActions NextAction {get;set;}
    public BattleActor currentActor;

    bool noTarget;
    bool performedMovement;
    bool performedAttack;
    BattleObject currentTarget;
    Vector2 movementPosition;
    Vector2 attackPosition;

    public bool Done { get; set; }

    public void Start()
    {
        NextAction = ThinkerActions.NONE;

        Done = false;
        performedMovement = false;
        performedAttack = false;
        noTarget = false;
        movementPosition = Vector2.zero;
        attackPosition = Vector2.zero;
        currentTarget = null;
        currentActor = null;
    }

    public void Perform()
    {
        //TODO try to protect their orb
        //TODO if can't kill the orb, tries to attack someone
        //TODO if can't attack someone, just move towards the orb
        bool didSomething = false;

        if (!noTarget &&
            currentTarget == null)
        {
            didSomething = FindTarget();
            if (!didSomething)
                noTarget = true;
        }

        if (!didSomething &&
            !performedMovement &&
            movementPosition != Vector2.zero)
            didSomething = DoMovement();

        if (!didSomething &&
            !performedAttack &&
            attackPosition != Vector2.zero &&
            currentTarget != null)
            didSomething = DoAttack();

        if (!didSomething || (!didSomething && performedMovement && performedAttack))
        {
            Debug.Log("Finished");
            Done = true;
        }
    }

    bool DoMovement()
    {
        Debug.Log("Moving");

        if (currentActor.CanMoveTo(movementPosition))
        {
            currentActor.SetPosition(movementPosition);
            performedMovement = true;

            if (attackPosition != Vector2.zero && currentTarget != null)
                NextAction = ThinkerActions.ATTACK;
            return true;
        }

        return false;
    }

    bool DoAttack()
    {
        Debug.Log("Attacking");

        if (currentActor.CanAttackAt(attackPosition) && !BoardUtils.IsPositionEmpty(attackPosition))
        {
            //TODO make objects take damage too
            BattleActor attackedObj = GameData.CurrentBattle.Board.GetActorAt(attackPosition);
            attackedObj.TakeDamage(currentActor.Type.Attack);
            performedAttack = true;

            NextAction = ThinkerActions.NONE;

            return true;
        }

        return false;
    }

    bool FindTarget()
    {
        Debug.Log("Finding target");

        bool performed = false;
        foreach (BattleActor actor in GameData.CurrentBattle.PlayerB.AvailableActors)
        {
            foreach (BattleObject obj in GameData.CurrentBattle.PlayerA.AvailableActors)
            {
                Vector2 movePattern;
                Vector2 attackPattern;
                if (BattleUtils.CanMoveAndAttack(actor, obj, out movePattern, out attackPattern))
                {
                    if (movePattern == Vector2.zero) // doesn't need to move
                    {
                        performedMovement = true;
                        NextAction = ThinkerActions.ATTACK;
                    }
                    else
                    {
                        movementPosition = actor.Position + movePattern;
                        NextAction = ThinkerActions.MOVE;
                    }

                    currentActor = actor;
                    currentTarget = obj;
                    attackPosition = movementPosition + attackPattern;
                    performed = true;
                    break;
                }
            }

            if (performed)
                break;
        }

        if (!performed)
        {
            // no one could attack anyone, just find some random guy to move randomly
            List<BattleActor> actors = new List<BattleActor>();
            foreach (BattleObject obj in GameData.CurrentBattle.PlayerB.AvailableActors)
            {
                if (obj is BattleActor)
                    actors.Add(obj as BattleActor);
            }
            actors.Shuffle();

            while (actors.Count > 0)
            {
                BattleActor actor = actors[0];
                actors.RemoveAt(0);

                List<Vector2> moveLocations = actor.Type.Pattern.GetLocationsForFlags(PatternFlags.Movement);
                moveLocations.Shuffle();

                bool moved = false;
                while (moveLocations.Count > 0)
                {
                    Vector2 move = moveLocations[0];
                    moveLocations.RemoveAt(0);

                    if (actor.CanMoveTo(actor.Position + move))
                    {
                        moved = true;
                        performed = true;
                        currentActor = actor;
                        movementPosition = move;
                        NextAction = ThinkerActions.MOVE;
                        break;
                    }
                }

                if (moved)
                    break;
            }
        }

        return performed;
    }
}
