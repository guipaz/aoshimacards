using System;
using UnityEngine;

public class Flow_EnemyTurn : IFlowController
{
    float actionCooldown = 0;
    BaseThinker thinker;
    GameObject pattern;

    public void Setup()
    {
        thinker = new BaseThinker();
    }

    public void Start()
    {
        BattleSceneController.Main.HeadsupText.text = "Enemy's turn";
        thinker.Start();
        actionCooldown = 1;
    }

    void Next()
    {
        //TODO choose where to move
        //TODO choose where to attack
        thinker.Perform();
        if (thinker.Done)
        {
            BattleSceneController.Main.SwitchFlow(FlowState.ChooseActorToPerform);
            return;
        }

        if (pattern != null)
            GameObject.Destroy(pattern);

        if (thinker.NextAction == ThinkerActions.MOVE)
            pattern = thinker.currentActor.Type.Pattern.GetVisualObject(PatternFlags.Movement, thinker.currentActor.Position);

        if (thinker.NextAction == ThinkerActions.ATTACK)
            pattern = thinker.currentActor.Type.Pattern.GetVisualObject(PatternFlags.Attack, thinker.currentActor.Position);

        actionCooldown = 1;
    }

    public void Update()
    {
        actionCooldown -= Time.deltaTime;
        if (actionCooldown <= 0)
        {
            Next();
        }
    }

    public void HandleInput()
    {

    }
}