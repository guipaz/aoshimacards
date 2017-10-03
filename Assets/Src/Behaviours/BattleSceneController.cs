using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneController : MonoBehaviour
{
    public static BattleSceneController Main;

    // ui
	public Canvas Canvas;
    public Text HeadsupText;
    public Text InfoText;
    public UIPerformMenu PerformMenu;

    // overall
    //TODO put inside a TurnInfo
    public FlowState CurrentFlow;
    public BattleActor SelectedActor;
    public bool PerformedMovement;
    public bool PerformedAttack;

    Dictionary<FlowState, IFlowController> controllers;
    Vector3 lastMouseDragPos;

	public void Awake()
	{
        Main = this;

		if (!GameData.Loaded)
        {
			GameData.Load ();
		}

		if (GameData.CurrentBattle == null) {
            BattleConfig config = new BattleConfig();
            config.ChosenWarriors.Add(GameData.WarriorTypes["holy_knight"]);
            config.ChosenWarriors.Add(GameData.WarriorTypes["dark_cleric"]);
            GameData.StartBattle(config);
		}

        controllers = new Dictionary<FlowState, IFlowController>();
        controllers.Add(FlowState.AddingWarriors,           new Flow_AddWarriors());
        controllers.Add(FlowState.ChooseActorToPerform,     new Flow_ChooseActorToPerform());
        controllers.Add(FlowState.PerformAttackLocation,    new Flow_PerformAttack());
        controllers.Add(FlowState.PerformMovementLocation,  new Flow_PerformMovement());
        controllers.Add(FlowState.FinishPlayerTurn,         new Flow_FinishTurn());
        controllers.Add(FlowState.EnemyTurn,                new Flow_EnemyTurn());

        CanvasUtils.Canvas = Canvas;
	}

	public void Start()
	{
        PerformMenu.Deactivate();

        foreach (IFlowController c in controllers.Values)
            c.Setup();

        CreateBoard();
        CreateEnemyActors();
        CreateActorObjects();
        Camera.main.transform.position += new Vector3((GameData.CurrentBattle.Config.SideWidth * 2 + GameData.CurrentBattle.Config.NeutralWidth) / 2f,
                                                      GameData.CurrentBattle.Config.Height / 2f);

        SwitchFlow(FlowState.AddingWarriors);
	}

    void CreateEnemyActors()
    {
        //TODO mock only
        BattleActor actor = new BattleActor(GameData.WarriorTypes["holy_knight"], "enemy");
        GameData.CurrentBattle.AddActor(actor);
    }

    public void SwitchFlow(FlowState flow)
    {
        CurrentFlow = flow;

        if (controllers.ContainsKey(CurrentFlow))
            controllers[CurrentFlow].Start();
    }

    void CreateBoard()
    {
        GameObject boardObject = new GameObject("_BOARD");

        MeshFilter meshFilter = (MeshFilter)boardObject.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = MeshGenerator.GetBoardMesh(GameData.CurrentBattle.Config);

        MeshRenderer renderer = boardObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material.shader = Shader.Find ("Sprites/Default");

        Texture2D texture = (Texture2D)Resources.Load("Sprites/board");
        renderer.material.mainTexture = texture;

        Board board = new Board(GameData.CurrentBattle.Config);
        board.GameObject = boardObject;
        GameData.CurrentBattle.Board = board;
    }

    void CreateActorObjects()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/actors");

        foreach (BattleActor actor in GameData.CurrentBattle.Actors)
        {
            GameObject obj = new GameObject("Actor_" + actor.Owner + "_" + actor.Type.Identifier);
            SpriteRenderer r = obj.AddComponent<SpriteRenderer>();

            if (actor.Owner == "player")
                r.sprite = sprites[0];
            else
                r.sprite = sprites[1];
            
            actor.GameObject = obj;

            obj.SetActive(false);
        }
    }

    public void Update()
    {
        InfoText.text = "";

        if (!PerformMenu.Active ||
            !CanvasUtils.ElementContainsScreenPosition(PerformMenu.GetComponent<RectTransform>(), Input.mousePosition))
        {
            if (Input.GetMouseButton(1))
            {
                PerformMenu.Deactivate();

                if (Input.GetMouseButtonDown(1))
                {
                    lastMouseDragPos = Input.mousePosition;
                }
                else
                {
                    Vector3 diff = (lastMouseDragPos - Input.mousePosition) / 50f;
                    Camera.main.transform.position += diff;
                    lastMouseDragPos = Input.mousePosition;
                }
            }

            if (controllers.ContainsKey(CurrentFlow))
                controllers[CurrentFlow].HandleInput();
        }

        Vector2 pos = BoardUtils.ScreenToBoardPosition(Input.mousePosition);
        if (!BoardUtils.IsInsideBoard(pos))
            return;
        
        BattleActor act = GameData.CurrentBattle.Board.GetActorAt(pos);
        if (act != null)
        {
            InfoText.text = act.Type.Name + " (" + act.CurrentHP + ")";
        }

        if (controllers.ContainsKey(CurrentFlow))
            controllers[CurrentFlow].Update();
    }
}