using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	public Canvas canvas;
	public UIWarriorsPanelControl warriorsControl;
	public GameObject mainMenuControl;

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

	public void ShowChooseWarriors()
	{
		mainMenuControl.SetActive (false);

		warriorsControl.Clear();
		warriorsControl.gameObject.SetActive (true);

		foreach (WarriorType warrior in GameData.WarriorTypes.Values)
			warriorsControl.AddAvailableWarrior (warrior);
	}

	public void OnClickedNewBattle()
	{
		ShowChooseWarriors ();
	}

	public void OnClickedExit()
	{
		Application.Quit();
	}

	public void StartBattle(BattleConfig config)
	{
		GameData.StartBattle (config);
		SceneManager.LoadScene ("BattleScene");
	}
}
