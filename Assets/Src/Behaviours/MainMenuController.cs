using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	public Canvas canvas;
	public UIWarriorsPanelControl warriorsControl;
	public GameObject mainMenuControl;

    BattlePlayer playerA;
    BattlePlayer playerB;

	public void Awake()
	{
		warriorsControl = canvas.transform.Find ("WarriorChoosePanel").GetComponent<UIWarriorsPanelControl>();
		mainMenuControl = canvas.transform.Find ("MainMenuPanel").gameObject;
	}

	public void Start()
	{
		GameData.Load();

		ShowMainMenu ();
	}

	public void ShowMainMenu()
	{
		warriorsControl.gameObject.SetActive (false);

		mainMenuControl.SetActive (true);
	}

    public void ShowChooseWarriors(string playerName)
	{
		mainMenuControl.SetActive (false);

		warriorsControl.Clear();
		warriorsControl.gameObject.SetActive(true);

		foreach (WarriorType warrior in GameData.WarriorTypes.Values)
			warriorsControl.AddAvailableWarrior(warrior);
	}

	public void OnClickedNewBattle()
	{
		ShowChooseWarriors("Player A");
	}

	public void OnClickedExit()
	{
		Application.Quit();
	}

    public void SetPlayer(BattlePlayer player)
	{
        if (playerA == null)
        {
            playerA = player;
            ShowChooseWarriors("Player B");
            return;
        }

        playerB = player;

        GameData.StartBattle(playerA, playerB);
		SceneManager.LoadScene("BattleScene");
	}
}
