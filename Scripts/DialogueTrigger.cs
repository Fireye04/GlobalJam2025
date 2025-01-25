using Godot;
using System;
using DialogueManagerRuntime;

public partial class DialogueTrigger : Node2D
{
	[Export]
	public Resource test;
	[Export]
	public bool startWithScene = false;

	private bool hasTriggered = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(startWithScene) {
			DialogueManager.ShowExampleDialogueBalloon(test, "start");
		}
	}
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_area_2d_body_entered(Node2D body) {
		if(body is Player && !startWithScene && !hasTriggered){
			DialogueManager.ShowExampleDialogueBalloon(test, "start");
			Player pbody = (Player) body;
			pbody.inputDirection = new Vector2();

			hasTriggered = true;
		}

	}
}
