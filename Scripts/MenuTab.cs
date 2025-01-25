using Godot;
using System;

public partial class MenuTab : MarginContainer
{
	private Menu mainMenu;

	public override void _Ready()
	{
		if(GetParent() is Menu){
			mainMenu = GetParent() as Menu;
		}
	}

	public void OnMenuSwapButtomPressed(int swapIndex){
		mainMenu.SwapMenu(swapIndex, GetIndex());
		Visible = false;
	}

	public void OnMenuReturnsButtonPressed(){
		mainMenu.SwapMenuToPrevious();
		Visible = false;
	}

	public void LoadSceneRequest(PackedScene loadScene){
		mainMenu.OnSwapScene(loadScene);
	}
}
