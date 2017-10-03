using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWarriorsPanelButton : MonoBehaviour
{
	public bool ReadOnly {get;set;}

	string identifier;
	UIWarriorsPanelControl parent;

	public void Setup(string identifier, UIWarriorsPanelControl parent)
	{
		ReadOnly = false;
		this.identifier = identifier;
		this.parent = parent;
	}

	public void OnClick()
	{
		if (!ReadOnly)
			parent.ToggleWarrior(GameData.WarriorTypes[identifier]);
	}
}
