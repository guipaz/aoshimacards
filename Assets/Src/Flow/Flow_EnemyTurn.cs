using System;
using UnityEngine;

public class Flow_EnemyTurn : IFlowController
{
    float actionCooldown = 0;

    public void Setup()
    {

    }

    public void Start()
    {
        //TODO
        BattleSceneController.Main.SwitchFlow(FlowState.ChooseActorToPerform);
        Next();
    }

    void Next()
    {
        actionCooldown -= Time.deltaTime;
        if (actionCooldown <= 0)
        {
            Next();
        }
    }

    public void Update()
    {

    }

    public void HandleInput()
    {

    }
}