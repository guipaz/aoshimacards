using System;
using System.Collections.Generic;

public class BattleState
{
    public BattlePlayer PlayerA;
    public BattlePlayer PlayerB;

    public BattlePlayer CurrentPlayer;

    public BattlePlayer Winner;

    public Board Board;

    public BattleState (BattlePlayer playerA, BattlePlayer playerB)
    {
        PlayerA = playerA;
        PlayerB = playerB;
	}

    public void RegisterUsedActor(BattleActor actor)
    {
        if (actor == null)
            return;

        actor.SetUsed(true);
        actor.Owner.UsedActors.Add(actor);
    }

    public void FinishRound()
    {
        foreach (BattleActor actor in PlayerA.UsedActors)
            actor.SetUsed(false);

        foreach (BattleActor actor in PlayerB.UsedActors)
            actor.SetUsed(false);

        PlayerA.UsedActors.Clear();
        PlayerB.UsedActors.Clear();
    }

    public bool IsActorUsed(BattleActor actor)
    {
        if (actor == null)
            return false;
        return actor.Owner.UsedActors.Contains(actor);
    }

    public bool IsRoundOver()
    {
        return PlayerA.AvailableActors.Count == PlayerA.UsedActors.Count &&
               PlayerB.AvailableActors.Count == PlayerB.UsedActors.Count;
    }

    public void EndGameWithWinner(BattlePlayer player)
    {
        Winner = player;

        BattleSceneController.Main.SwitchFlow(FlowState.GameFinished);
    }
}