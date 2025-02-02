using Godot;
using System;
using System.Threading.Tasks;
using DialogueManagerRuntime;

public partial class GameState: Node
{

    public static Player pboi;
    public static GameState Instance { get; private set; }
    //If user beats qt even once we hit the bad ending
    public static bool hasBeatQuicktime = false;	

    private String target;
    
    public override void _Ready()
    {
        Instance = this;
    }

    public async Task triggerQT(int whichOne){
        var qt = GD.Load<PackedScene>("res://Scenes/Tools/quicktime.tscn").Instantiate() as Quicktime;
        GetTree().Root.AddChild(qt);
        await ToSignal(qt, "Completed");
        if (qt.succeeded){
            hasBeatQuicktime = true;
        } else  {
            loadLevel(whichOne);
        }
        qt.QueueFree();
    }

    public async Task loadLevel(int whichOne){
        if (whichOne == 1){
            target = "res://Scenes/Levels/sadLevelTwoAlt.tscn";

        } else if (whichOne == 2){
            target = "res://Scenes/Levels/sadLevelFourAlt.tscn";
        } else {
            target = "res://Scenes/Levels/EndScreen.tscn";
        }

        Callable.From(() => {GetTree().ChangeSceneToFile(target);}).CallDeferred();

    }
    
    public override void _Process(double delta){
        DialogueManager.DialogueEnded += (Resource dialogueResource) =>
        {
            if (pboi.interacting){
                InteractionBox box = pboi.GetNode<InteractionBox>("%Interaction Box");
                box.find_nearest_interactable().GetNode<AnimationPlayer>("%AnimationPlayer").Play("fade_out");
                pboi.interacting = false;
            }
        };
    }
}
