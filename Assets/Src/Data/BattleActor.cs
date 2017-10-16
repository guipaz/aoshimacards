using System;
using UnityEngine;

public class BattleActor : BattleObject
{
	public WarriorType Type { get; private set; }
	public int CurrentHP { get; private set; }
    public bool Inverted {get; private set;}

    public override string Description
    {
        get
        {
            return Type.Name + " (" + CurrentHP + ")";
        }
    }

    public BattleActor(WarriorType type, BattlePlayer owner) : base(owner)
	{
		Type = type;
        CurrentHP = type.HP;
	}

    public bool CanMoveTo(Vector2 to)
    {
        return Type.Pattern.CanMoveTo(Position, to, Inverted);
    }

    public bool CanAttackAt(Vector2 at)
    {
        BattleObject objectAt = GameData.CurrentBattle.Board.GetObjectAt(at);
        return objectAt != null && Type.Pattern.CanAttackAt(Position, at, Inverted);
    }

    public override void TakeDamage(int damage)
    {
        CurrentHP -= damage;

        if (CurrentHP <= 0)
            Owner.DefeatWarrior(this);
    }

    public void SetUsed(bool used)
    {
        if (used)
            GameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        else
            GameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void SetInverted(bool inverted)
    {
        Inverted = inverted;
        GameObject.GetComponent<SpriteRenderer>().flipX = inverted;
    }
}