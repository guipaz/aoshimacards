using System;

public class WarriorType
{
	public string Identifier { get; private set; }
	public string Name { get; set; }
	public WarriorPattern Pattern { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }
	//TODO skill
    //TODO modifiers

	public WarriorType (string id)
	{
		Identifier = id;
	}
}