﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWarriorsPanelControl : MonoBehaviour {

	public MainMenuController controller;

	public GameObject warriorOptionPrefab;
	public GameObject availableWarriorsPanel;
	public GameObject selectedWarriorsPanel;
    public Text nameFieldText;

	public List<WarriorType> chosenWarriors;

	public void Awake()
	{
		chosenWarriors = new List<WarriorType>();

		controller = GameObject.Find ("_CONTROLLER").GetComponent<MainMenuController>();
		availableWarriorsPanel = transform.Find ("AvailableWarriorsPanel").Find("Inner").gameObject;
		selectedWarriorsPanel = transform.Find ("SelectedWarriorsPanel").Find("Inner").gameObject;
        nameFieldText = transform.Find("NameField").Find("Text").GetComponent<Text>();
	}

	public void Clear()
	{
		foreach (Transform t in availableWarriorsPanel.transform)
			Destroy (t.gameObject);

		foreach (Transform t in selectedWarriorsPanel.transform)
			Destroy (t.gameObject);

        nameFieldText.text = "";
		chosenWarriors.Clear ();
	}

	public void AddAvailableWarrior(WarriorType warrior)
	{
		GameObject warriorOption = GameObject.Instantiate (warriorOptionPrefab, availableWarriorsPanel.transform, false);
		warriorOption.name = warrior.Identifier;
		warriorOption.transform.Find ("Name").GetComponent<Text> ().text = warrior.Name;
		warriorOption.GetComponent<UIWarriorsPanelButton>().Setup(warrior.Identifier, this);
	}

	public void ToggleWarrior(WarriorType warrior)
	{
		Debug.Log ("Chose " + warrior.Name);
		if (chosenWarriors.Contains (warrior)) {
			RemoveSelectedWarrior (warrior);
		} else {
			if (chosenWarriors.Count < 3) {
				AddSelectedWarrior (warrior);
			}
		}
	}

	void AddSelectedWarrior(WarriorType warrior)
	{
		chosenWarriors.Add (warrior);

		Transform obj = availableWarriorsPanel.transform.Find (warrior.Identifier);
		GameObject newObj = GameObject.Instantiate (obj.gameObject, selectedWarriorsPanel.transform, false);
		newObj.name = warrior.Identifier;
		newObj.GetComponent<UIWarriorsPanelButton> ().Setup (warrior.Identifier, this);
	}

	void RemoveSelectedWarrior(WarriorType warrior)
	{
		chosenWarriors.Remove (warrior);

		Transform obj = selectedWarriorsPanel.transform.Find (warrior.Identifier);
		Destroy (obj.gameObject);
	}

	public void OnClickButton(string id)
	{
		if (id == "Battle") {
            if (nameFieldText.text == "")
                return;
            
			if (chosenWarriors.Count < 3) {
				return;
			}

            BattlePlayer player = new BattlePlayer(nameFieldText.text);
            foreach (WarriorType type in chosenWarriors)
                player.AddWarrior(type);
			controller.SetPlayer(player);
		}
		else if (id == "Back") {
			controller.ShowMainMenu ();
		}
	}
}
