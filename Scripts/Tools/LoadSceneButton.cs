using Godot;
using System;

public partial class LoadSceneButton : Button
{
	[Export] PackedScene sceneToSwitchTo;

	public override void _Ready()
	{
		Pressed += OnSwitchSceneButtonPressed;
	}

	private void OnSwitchSceneButtonPressed()
	{
		if(GetParent().GetParent() is MenuTab menuTab){
            Callable.From(() => { GetTree().ChangeSceneToPacked(sceneToSwitchTo); }).CallDeferred();
		}
	}
}
