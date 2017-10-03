using System;
using System.Collections.Generic;

public class BattleState
{
    public List<BattleActor> Actors { get;private set; }
    public BattleConfig Config { get; private set; }
    public Board Board { get; set; }

	public BattleState (BattleConfig config)
    {
        Actors = new List<BattleActor>();
        Config = config;

        foreach (WarriorType type in config.ChosenWarriors)
        {
            BattleActor actor = new BattleActor(type, "player");
            AddActor(actor);
        }
	}

    public void AddActor(BattleActor actor)
    {
        Actors.Add(actor);
    }
}