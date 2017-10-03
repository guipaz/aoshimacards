using System;
using System.Collections.Generic;

public class BattleConfig
{
	public List<WarriorType> ChosenWarriors { get; set; }
    public int SideWidth {get;set;}
    public int NeutralWidth {get;set;}
    public int Height {get;set;}
    public int BoardWidth
    {
        get
        {
            return SideWidth * 2 + NeutralWidth;
        }
    }

    public BattleConfig()
    {
        ChosenWarriors = new List<WarriorType>();
        SideWidth = 5;
        NeutralWidth = 3;
        Height = 7;
    }
}