using System;
using UnityEngine;

public class BattleActor
{
	public WarriorType Type { get; private set; }
	public int CurrentHP { get; private set; }
	public string Owner { get; private set; }
    public GameObject GameObject { get; set; }
    public Vector2 Position { get; private set; }

	public BattleActor (WarriorType type, string owner)
	{
		Type = type;
		Owner = owner;
        CurrentHP = type.HP;
	}

    public void SetPosition(Vector2 position)
    {
        GameData.CurrentBattle.Board.SetActorAt(null, Position);

        Position = position;
        GameObject.transform.position = BoardUtils.BoardToWorldPosition(position);

        GameData.CurrentBattle.Board.SetActorAt(this, Position);
    }

    public bool CanMoveTo(Vector2 to)
    {
        return Type.Pattern.CanMoveTo(Position, to);
    }

    public bool CanAttackAt(Vector2 at)
    {
        BattleActor actorAt = GameData.CurrentBattle.Board.GetActorAt(at);
        return actorAt != null && actorAt.Owner != "player" && Type.Pattern.CanAttackAt(Position, at);
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
    }
}