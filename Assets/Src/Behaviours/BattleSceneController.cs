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
    public bool PerformedTurn;

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
            BattlePlayer playerA = new BattlePlayer("Stryfe");
            BattlePlayer playerB = new BattlePlayer("Player Two");

            playerA.AddWarrior(GameData.WarriorTypes["holy_knight"]);
            playerA.AddWarrior(GameData.WarriorTypes["shadow_cleric"]);
            playerA.AddWarrior(GameData.WarriorTypes["ninja"]);

            playerB.AddWarrior(GameData.WarriorTypes["blood_mage"]);
            playerB.AddWarrior(GameData.WarriorTypes["wild_hunter"]);
            playerB.AddWarrior(GameData.WarriorTypes["sorcerer"]);

            GameData.StartBattle(playerA, playerB);
		}

        controllers = new Dictionary<FlowState, IFlowController>();
        controllers.Add(FlowState.AddingWarriors,           new Flow_AddWarriors());
        controllers.Add(FlowState.ChooseActorToPerform,     new Flow_ChooseActorToPerform());
        controllers.Add(FlowState.PerformAttackLocation,    new Flow_PerformAttack());
        controllers.Add(FlowState.PerformMovementLocation,  new Flow_PerformMovement());
        controllers.Add(FlowState.FinishPlayerTurn,         new Flow_FinishTurn());
        controllers.Add(FlowState.EnemyTurn,                new Flow_EnemyTurn());
        controllers.Add(FlowState.GameFinished,             new Flow_GameFinished());

        CanvasUtils.Canvas = Canvas;
	}

	public void Start()
	{
        PerformMenu.Deactivate();

        foreach (IFlowController c in controllers.Values)
            c.Setup();

        CreateBoard();
        CreateOrbs();
        CreateActorObjects();
        Camera.main.transform.position += new Vector3(GameData.CurrentBattle.Board.Width / 2f,
                                                      GameData.CurrentBattle.Board.Height / 2f);

        GameData.CurrentBattle.CurrentPlayer = GameData.CurrentBattle.PlayerA;
        SwitchFlow(FlowState.AddingWarriors);
	}

    void CreateOrbs()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/actors");

        BattleObject obj = new BattleObject(GameData.CurrentBattle.PlayerA);
        obj.Properties["isOrb"] = "true";
        GameObject gObj = new GameObject("Orb_" + obj.Owner);
        SpriteRenderer r = gObj.AddComponent<SpriteRenderer>();
        r.sprite = sprites[2];
        obj.GameObject = gObj;
        obj.SetDescription(obj.Owner.Name + "'s Orb");
        gObj.SetActive(true);
        obj.SetPosition(new Vector2(0, GameData.CurrentBattle.Board.Height / 2));

        obj = new BattleObject(GameData.CurrentBattle.PlayerB);
        obj.Properties["isOrb"] = "true";
        gObj = new GameObject("Orb_" + obj.Owner);
        r = gObj.AddComponent<SpriteRenderer>();
        r.sprite = sprites[2];
        obj.GameObject = gObj;
        obj.SetDescription(obj.Owner.Name + "'s Orb");
        gObj.SetActive(true);
        obj.SetPosition(new Vector2(GameData.CurrentBattle.Board.Width - 1, GameData.CurrentBattle.Board.Height / 2));
    }

    public void SwitchFlow(FlowState flow)
    {
        CurrentFlow = flow;

        if (controllers.ContainsKey(CurrentFlow))
            controllers[CurrentFlow].Start();
    }

    void CreateBoard()
    {
        Board board = new Board();

        GameObject boardObject = new GameObject("_BOARD");
        MeshFilter meshFilter = (MeshFilter)boardObject.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = MeshGenerator.GetBoardMesh(board);
        MeshRenderer renderer = boardObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material.shader = Shader.Find ("Sprites/Default");
        Texture2D texture = (Texture2D)Resources.Load("Sprites/board");
        renderer.material.mainTexture = texture;

        board.GameObject = boardObject;
        GameData.CurrentBattle.Board = board;
    }

    void CreateActorObjects()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/actors");

        List<BattleActor> all = new List<BattleActor>();
        all.AddRange(GameData.CurrentBattle.PlayerA.AvailableActors);
        all.AddRange(GameData.CurrentBattle.PlayerB.AvailableActors);
        foreach (BattleActor actor in all)
        {
            GameObject obj = new GameObject("Actor_" + actor.Owner.Name + "_" + actor.Type.Identifier);
            SpriteRenderer r = obj.AddComponent<SpriteRenderer>();

            if (actor.Owner == GameData.CurrentBattle.PlayerA)
                r.sprite = sprites[0];
            else
                r.sprite = sprites[1];
            
            actor.GameObject = obj;

            obj.SetActive(false);

            // also inverts player b's actors
            if (actor.Owner == GameData.CurrentBattle.PlayerB)
                actor.SetInverted(true);
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
        
        BattleObject obj = GameData.CurrentBattle.Board.GetObjectAt(pos);
        if (obj != null)
        {
            InfoText.text = obj.Description;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && PerformMenu.Active)
            PerformMenu.Deactivate();

        if (controllers.ContainsKey(CurrentFlow))
            controllers[CurrentFlow].Update();
    }
}