using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
	public static bool Loaded { get; private set; }
	public static Dictionary<string, WarriorType> WarriorTypes { get; private set; }
	public static BattleState CurrentBattle { get; private set; }

	public static void Load()
	{
		WarriorTypes = new Dictionary<string, WarriorType> (8);

		// creating warriors
		WarriorType holyKnight = new WarriorType("holy_knight");
		holyKnight.Name = "Holy Knight";
        holyKnight.HP = 9;
        holyKnight.Attack = 3;

		WarriorType darkCleric = new WarriorType("dark_cleric");
		darkCleric.Name = "Dark Cleric";
        darkCleric.HP = 4;
        darkCleric.Attack = 5;

		// setting patterns
		WarriorPattern pattern = new WarriorPattern ();
        pattern.SetFlagsAt (0, -1, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (0, 1, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (-1, 0, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (3, 0, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (2, 2, PatternFlags.Movement | PatternFlags.Attack);
		holyKnight.Pattern = pattern;

		pattern = new WarriorPattern ();
        pattern.SetFlagsAt (0, -1, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (0, 2, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (-1, 0, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (1, 0, PatternFlags.Movement | PatternFlags.Attack);
		darkCleric.Pattern = pattern;

		// adds warriors
		WarriorTypes.Add (holyKnight.Identifier, holyKnight);
		WarriorTypes.Add (darkCleric.Identifier, darkCleric);

		Loaded = true;
	}

	public static void StartBattle(BattleConfig config)
	{
		CurrentBattle = new BattleState (config);
	}
}
