using Godot;
using System;

public partial class EndScreen : Node2D
{

    [Export]
    public Label ending;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        if (GameState.failCount > 1){
            ending.Text = "Ending 1: Escape";
        } else if (GameState.failCount > 0) {
            ending.Text = "Ending 2: A glimmer of hope";
        } else {
            ending.Text = "Ending 3: There is no mercy. There is no escape.";
        }

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private void _on_button_2_pressed() {
       Callable.From(() => {GetTree().ChangeSceneToFile("res://Scenes/Levels/goodLevelOne.tscn");}).CallDeferred();
    }

    private void _on_button_pressed() {
       Callable.From(() => {GetTree().ChangeSceneToFile("res://Scenes/Levels/Misc/Menu.tscn");}).CallDeferred();
    }
}
