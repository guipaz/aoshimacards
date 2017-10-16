using System;
using System.Collections.Generic;

public class BattlePlayer
{
    public string Name {get;private set;}
    public List<BattleActor> AvailableActors {get; private set;}
    public List<BattleActor> DefeatedActors {get; private set;}
    public List<BattleActor> UsedActors { get; private set; }

    public BattlePlayer(string name)
    {
        Name = name;
        AvailableActors = new List<BattleActor>();
        DefeatedActors = new List<BattleActor>();
        UsedActors = new List<BattleActor>();
    }

    public BattlePlayer GetEnemy()
    {
        if (GameData.CurrentBattle.PlayerA == this)
            return GameData.CurrentBattle.PlayerB;
        return GameData.CurrentBattle.PlayerA;
    }

    public void AddWarrior(WarriorType type)
    {
        BattleActor actor = new BattleActor(type, this);
        AvailableActors.Add(actor);
    }

    public void DefeatWarrior(BattleActor actor)
    {
        if (actor != null && AvailableActors.Contains(actor))
        {
            AvailableActors.Remove(actor);
            UsedActors.Remove(actor);
            DefeatedActors.Add(actor);

            actor.RemoveFromBoard();
        }
    }
}