using Godot;
using System;
using System.Threading.Tasks;

public partial class GameState: Node
{
    public static GameState Instance { get; private set; }
    //If user beats qt even once we hit the bad ending
    public static bool hasBeatQuicktime = false;	
    
    public override void _Ready()
    {
        Instance = this;
    }

    public async Task loadLevel(int whichOne){
       Callable.From(() => {GetTree().ChangeSceneToFile("res://Scenes/Levels/sadLevelTwoAlt.tscn");}).CallDeferred();
        /*var nameInputDialogue = GD.Load<PackedScene>("res://Scenes/Levels/sadLvlThree.tscn").Instantiate() as Node2D;*/
        /*GetTree().Root.AddChild(nameInputDialogue);*/

    }
}
