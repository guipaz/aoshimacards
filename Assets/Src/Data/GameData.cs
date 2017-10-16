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

		// holy knight
		WarriorType warrior = new WarriorType("holy_knight");
        warrior.Name = "Holy Knight";
        warrior.Attack = 3;
        warrior.HP = 16;

        WarriorPattern pattern = new WarriorPattern ();
        pattern.SetFlagsAt (-1, 0, PatternFlags.Movement);
        pattern.SetFlagsAt (1, 0, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (0, -1, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (0, 1, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (1, -1, PatternFlags.Attack);
        pattern.SetFlagsAt (1, 1, PatternFlags.Attack);
        warrior.Pattern = pattern;
		WarriorTypes.Add (warrior.Identifier, warrior);

        // blood mage
        warrior = new WarriorType("blood_mage");
        warrior.Name = "Blood Mage";
        warrior.Attack = 4;
        warrior.HP = 8;

        pattern = new WarriorPattern ();
        pattern.SetFlagsAt (-1, 0, PatternFlags.Movement);
        pattern.SetFlagsAt (1, 0, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (0, -1, PatternFlags.Movement);
        pattern.SetFlagsAt (0, 1, PatternFlags.Movement);
        pattern.SetFlagsAt (0, -2, PatternFlags.Attack);
        pattern.SetFlagsAt (0, 2, PatternFlags.Attack);
        pattern.SetFlagsAt (1, -1, PatternFlags.Attack);
        pattern.SetFlagsAt (1, 1, PatternFlags.Attack);
        pattern.SetFlagsAt (2, -1, PatternFlags.Attack);
        pattern.SetFlagsAt (2, 0, PatternFlags.Attack);
        pattern.SetFlagsAt (2, 1, PatternFlags.Attack);
        pattern.SetFlagsAt (2, -2, PatternFlags.Attack);
        pattern.SetFlagsAt (2, 2, PatternFlags.Attack);

        warrior.Pattern = pattern;
        WarriorTypes.Add (warrior.Identifier, warrior);

        // dark cleric
        warrior = new WarriorType("shadow_cleric");
        warrior.Name = "Shadow Cleric";
        warrior.Attack = 5;
        warrior.HP = 8;

        pattern = new WarriorPattern ();
        pattern.SetFlagsAt (-1, 0, PatternFlags.Attack);
        pattern.SetFlagsAt (1, 0, PatternFlags.Attack);
        pattern.SetFlagsAt (0, -1, PatternFlags.Attack);
        pattern.SetFlagsAt (0, 1, PatternFlags.Attack);
        pattern.SetFlagsAt (-1, -1, PatternFlags.Attack);
        pattern.SetFlagsAt (-1, 1, PatternFlags.Attack);
        pattern.SetFlagsAt (1, 1, PatternFlags.Attack);
        pattern.SetFlagsAt (1, -1, PatternFlags.Attack);
        pattern.SetFlagsAt (0, -2, PatternFlags.Movement);
        pattern.SetFlagsAt (0, 2, PatternFlags.Movement);
        pattern.SetFlagsAt (-2, -1, PatternFlags.Movement);
        pattern.SetFlagsAt (-2, 1, PatternFlags.Movement);
        pattern.SetFlagsAt (2, -1, PatternFlags.Movement);
        pattern.SetFlagsAt (2, 1, PatternFlags.Movement);

        warrior.Pattern = pattern;
        WarriorTypes.Add (warrior.Identifier, warrior);

        // ninja
        warrior = new WarriorType("ninja");
        warrior.Name = "Ninja";
        warrior.Attack = 3;
        warrior.HP = 9;

        pattern = new WarriorPattern ();
        pattern.SetFlagsAt (0, -1, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (0, 1, PatternFlags.Movement  | PatternFlags.Attack);
        pattern.SetFlagsAt (0, -2, PatternFlags.Movement);
        pattern.SetFlagsAt (0, 2, PatternFlags.Movement);
        pattern.SetFlagsAt (-2, -2, PatternFlags.Movement);
        pattern.SetFlagsAt (-2, 2, PatternFlags.Movement);
        pattern.SetFlagsAt (2, -2, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (2, 2, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (-1, 0, PatternFlags.Attack);
        pattern.SetFlagsAt (1, 0, PatternFlags.Attack);
        pattern.SetFlagsAt (2, 0, PatternFlags.Attack);

        warrior.Pattern = pattern;
        WarriorTypes.Add (warrior.Identifier, warrior);

        // barbarian
        warrior = new WarriorType("barbarian");
        warrior.Name = "Barbarian";
        warrior.Attack = 4;
        warrior.HP = 12;

        pattern = new WarriorPattern ();
        pattern.SetFlagsAt (-1, 0, PatternFlags.Movement);
        pattern.SetFlagsAt (2, 0, PatternFlags.Movement);
        pattern.SetFlagsAt (0, -2, PatternFlags.Movement);
        pattern.SetFlagsAt (0, 2, PatternFlags.Movement);
        pattern.SetFlagsAt (0, -1, PatternFlags.Attack);
        pattern.SetFlagsAt (0, 1, PatternFlags.Attack);
        pattern.SetFlagsAt (1, 0, PatternFlags.Attack);
        pattern.SetFlagsAt (1, -1, PatternFlags.Attack);
        pattern.SetFlagsAt (1, 1, PatternFlags.Attack);
        pattern.SetFlagsAt (-1, -1, PatternFlags.Attack);
        pattern.SetFlagsAt (-1, 1, PatternFlags.Attack);
        warrior.Pattern = pattern;
        WarriorTypes.Add (warrior.Identifier, warrior);

        // wild hunter
        warrior = new WarriorType("wild_hunter");
        warrior.Name = "Wild Hunter";
        warrior.Attack = 3;
        warrior.HP = 10;

        pattern = new WarriorPattern ();
        pattern.SetFlagsAt (-2, 0, PatternFlags.Movement);
        pattern.SetFlagsAt (1, 0, PatternFlags.Movement);
        pattern.SetFlagsAt (0, -1, PatternFlags.Movement);
        pattern.SetFlagsAt (0, 1, PatternFlags.Movement);
        pattern.SetFlagsAt (0, -2, PatternFlags.Attack);
        pattern.SetFlagsAt (1, -2, PatternFlags.Attack);
        pattern.SetFlagsAt (2, -2, PatternFlags.Attack);
        pattern.SetFlagsAt (2, -1, PatternFlags.Attack);
        pattern.SetFlagsAt (2, 0, PatternFlags.Attack);
        pattern.SetFlagsAt (2, 1, PatternFlags.Attack);
        pattern.SetFlagsAt (2, 2, PatternFlags.Attack);
        pattern.SetFlagsAt (1, 2, PatternFlags.Attack);
        pattern.SetFlagsAt (0, 2, PatternFlags.Attack);
        warrior.Pattern = pattern;
        WarriorTypes.Add (warrior.Identifier, warrior);

        // sorcerer
        warrior = new WarriorType("sorcerer");
        warrior.Name = "Sorcerer";
        warrior.Attack = 4;
        warrior.HP = 8;

        pattern = new WarriorPattern ();
        pattern.SetFlagsAt (-1, 0, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (1, 0, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (0, -1, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (0, 1, PatternFlags.Movement | PatternFlags.Attack);
        pattern.SetFlagsAt (0, -2, PatternFlags.Attack);
        pattern.SetFlagsAt (0, 2, PatternFlags.Attack);
        pattern.SetFlagsAt (-2, 0, PatternFlags.Attack);
        pattern.SetFlagsAt (2, 0, PatternFlags.Attack);
        warrior.Pattern = pattern;
        WarriorTypes.Add (warrior.Identifier, warrior);

        // assassin
        warrior = new WarriorType("assassin");
        warrior.Name = "Assassin";
        warrior.Attack = 8;
        warrior.HP = 6;

        pattern = new WarriorPattern ();
        pattern.SetFlagsAt (0, -1, PatternFlags.Movement);
        pattern.SetFlagsAt (0, 1, PatternFlags.Movement);
        pattern.SetFlagsAt (-2, 0, PatternFlags.Movement);
        pattern.SetFlagsAt (2, 0, PatternFlags.Movement);
        pattern.SetFlagsAt (2, -1, PatternFlags.Movement);
        pattern.SetFlagsAt (2, -2, PatternFlags.Movement);
        pattern.SetFlagsAt (2, 1, PatternFlags.Movement);
        pattern.SetFlagsAt (2, 2, PatternFlags.Movement);
        pattern.SetFlagsAt (1, 0, PatternFlags.Attack);
        warrior.Pattern = pattern;
        WarriorTypes.Add (warrior.Identifier, warrior);

		Loaded = true;
	}

    public static void StartBattle(BattlePlayer playerA, BattlePlayer playerB)
	{
        CurrentBattle = new BattleState(playerA, playerB);
	}
}
