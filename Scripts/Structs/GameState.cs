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

    public async Task triggerQT(int whichOne){
        var qt = GD.Load<PackedScene>("res://Scenes/Tools/quicktime.tscn").Instantiate() as Quicktime;
        GetTree().Root.AddChild(qt);
        /*qt.StartQT(15);*/
        await ToSignal(qt, "Completed");
        if (!qt.succeeded){
            loadLevel(whichOne);
        }
        qt.QueueFree();
    }

    public async Task loadLevel(int whichOne){
        if (whichOne == 1){
            target = "res://Scenes/Levels/sadLevelTwoAlt.tscn"

        } else {
            target = "res://Scenes/Levels/sadLevelFourAlt.tscn"
        }
       Callable.From(() => {GetTree().ChangeSceneToFile(target);}).CallDeferred();
        /*var nameInputDialogue = GD.Load<PackedScene>("res://Scenes/Levels/sadLvlThree.tscn").Instantiate() as Node2D;*/
        /*GetTree().Root.AddChild(nameInputDialogue);*/

    }
}
