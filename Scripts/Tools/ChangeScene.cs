using Godot;
using System;

public partial class ChangeScene : Node2D
{

    [Export]
    public PackedScene target;

    [Export]
    public PackedScene doNotUse;

    [Export]
    public bool ending = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_area_2d_body_entered(Node2D body) {
		if(body is Player) {
            if (ending && !GameState.hasBeatQuicktime){
                target = doNotUse;
            }
            Callable.From(() => { GetTree().ChangeSceneToPacked(target); }).CallDeferred();
		}
	}
	
	
}
