using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIPerformMenu : MonoBehaviour
{
    public BattleActor Actor { get; private set; }
    public bool Active { get; private set; }

    public Button MoveButton;
    public Button AttackButton;

    public Action<BattleActor> OnMove { get; set; }
    public Action<BattleActor> OnAttack { get; set; }
    public Action<BattleActor> OnFinish { get; set; }

    public void Activate(BattleActor actor, bool movement = true, bool attack = true)
    {
        Active = true;
        Actor = actor;

        Vector2 actorPos = BoardUtils.BoardToWorldPosition(actor.Position);
        actorPos.x += 0.5f;

        CanvasUtils.SetElementAtWorldPosition(GetComponent<RectTransform>(), actorPos);
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        MoveButton.interactable = movement;
        AttackButton.interactable = attack;
    }

    public void Deactivate()
    {
        Active = false;
        Actor = null;
        gameObject.SetActive(false);
    }

    public void OnClickButton(string message)
    {
        if (message == "move")
        {
            if (OnAttack != null)
                OnMove.Invoke(Actor);
        }
        else if (message == "attack")
        {
            if (OnAttack != null)
                OnAttack.Invoke(Actor);
        }
        else if (message == "finish")
        {
            if (OnFinish != null)
                OnFinish.Invoke(Actor);
        }
    }
}
