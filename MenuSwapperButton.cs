using Godot;
using System;

public partial class MenuSwapperButton : Button
{
	[Export] Node SwitchToMenu;

	public override void _Ready()
	{
		Pressed += OnMenuSwapperButtonPressed;
	}

	private void OnMenuSwapperButtonPressed()
	{
		if(GetParent().GetParent() is MenuTab menuTab){
			menuTab.OnMenuSwapButtomPressed(SwitchToMenu.GetIndex());
		}
	}
}
