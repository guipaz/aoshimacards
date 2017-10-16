using System;
using UnityEngine;
using System.Collections.Generic;

public class BattleObject
{
    public BattlePlayer Owner { get; protected set; }
    public GameObject GameObject { get; set; }
    public Vector2 Position { get; protected set; }
    public Dictionary<string, string> Properties { get; protected set; }

    protected string _description;
    public virtual string Description { get { return _description; } }

    public BattleObject(BattlePlayer owner)
    {
        Owner = owner;
        Properties = new Dictionary<string, string>();
    }

    public void SetPosition(Vector2 position)
    {
        GameData.CurrentBattle.Board.SetObjectAt(null, Position);

        Position = position;
        GameObject.transform.position = BoardUtils.BoardToWorldPosition(position);

        GameData.CurrentBattle.Board.SetObjectAt(this, Position);
    }

    public void RemoveFromBoard()
    {
        GameData.CurrentBattle.Board.SetObjectAt(null, Position);
        GameObject.SetActive(false);
    }

    public virtual void SetDescription(string value)
    {
        _description = value;
    }

    public virtual void TakeDamage(int damage)
    {
        
    }
}
