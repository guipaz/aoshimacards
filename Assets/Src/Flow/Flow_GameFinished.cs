using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flow_GameFinished : IFlowController
{
    public void Setup()
    {

    }

    public void Start()
    {
        BattleSceneController.Main.HeadsupText.text = GameData.CurrentBattle.Winner.Name + " won!\nPress Space to go back to the menu";
    }

    public void Update()
    {

    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("MainMenu");
    }
}