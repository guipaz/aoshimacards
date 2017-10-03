using System;
using UnityEngine;

public class Flow_PerformAttack : IFlowController
{
    GameObject currentPattern;

    public void Setup()
    {
    }

    public void Start()
    {
        BattleSceneController.Main.HeadsupText.text = "Choose the attack location (ESC to cancel)";

        GameObject patternObject = new GameObject("_ATTACK_PATTERN");
        patternObject.transform.position = new Vector3(patternObject.transform.position.x, patternObject.transform.position.y, -5); //TODO

        MeshFilter meshFilter = (MeshFilter)patternObject.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = MeshGenerator.GetPatternMesh(BattleSceneController.Main.SelectedActor.Type.Pattern, PatternFlags.Attack,
            BattleSceneController.Main.SelectedActor.Position);

        MeshRenderer renderer = patternObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material.shader = Shader.Find ("Sprites/Default");

        Texture2D texture = (Texture2D)Resources.Load("Sprites/board");
        renderer.material.mainTexture = texture;

        currentPattern = patternObject;
    }

    void Choose(Vector2 position)
    {
        if (BoardUtils.IsInsideBoard(position) &&
            BattleSceneController.Main.SelectedActor.CanAttackAt(position))
        {
            BattleActor actor = GameData.CurrentBattle.Board.GetActorAt(position);
            actor.TakeDamage(BattleSceneController.Main.SelectedActor.Type.Attack);

            //TODO eliminate if it's the case

            BattleSceneController.Main.PerformedAttack = true;
            GameObject.Destroy(currentPattern);
            BattleSceneController.Main.SwitchFlow(FlowState.ChooseActorToPerform);
        }
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Choose(BoardUtils.ScreenToBoardPosition(Input.mousePosition));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Destroy(currentPattern);
            BattleSceneController.Main.PerformMenu.Deactivate();
            BattleSceneController.Main.SwitchFlow(FlowState.ChooseActorToPerform);
        }
    }

    public void Update()
    {

    }
}