using System;
using UnityEngine;

public class Flow_AddWarriors : IFlowController
{
    int addWarriors_index;

    public void Setup()
    {
    }

    public void Start()
    {
        BattleSceneController.Main.HeadsupText.gameObject.SetActive(true);
        addWarriors_index = 0;
        Next();
    }

    void Next()
    {
        BattleActor actor = GameData.CurrentBattle.Actors[addWarriors_index];
        BattleSceneController.Main.HeadsupText.text = "Place your " + actor.Type.Name;
    }

    void Place(Vector2 at, BattleActor actor)
    {
        if (!BoardUtils.IsInsideBoard(at))
            return;
        
        if (at.x >= BoardUtils.Current.SideWidth)
            return;

        actor.SetPosition(at);
        actor.GameObject.SetActive(true);

        bool adding = false;
        while (addWarriors_index + 1 < GameData.CurrentBattle.Actors.Count)
        {
            addWarriors_index++;
            if (GameData.CurrentBattle.Actors[addWarriors_index].Owner == "player")
            {
                adding = true;
                Next();
                break;
            }
        }

        if (!adding && addWarriors_index + 1 >= GameData.CurrentBattle.Actors.Count)
        {
            SetupEnemy();
            BattleSceneController.Main.SwitchFlow(FlowState.ChooseActorToPerform);
        }
    }

    void SetupEnemy()
    {
        System.Random r = new System.Random();

        foreach (BattleActor actor in GameData.CurrentBattle.Actors)
        {
            if (actor.Owner == "player")
                continue;

            int x = r.Next(4);
            int y = r.Next(6);

            x += BoardUtils.Current.BoardWidth - BoardUtils.Current.SideWidth;
            actor.SetPosition(new Vector2(x, y));
            actor.GameObject.SetActive(true);
        }
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Place(BoardUtils.ScreenToBoardPosition(Input.mousePosition), GameData.CurrentBattle.Actors[addWarriors_index]);
        }
    }

    public void Update()
    {

    }
}

