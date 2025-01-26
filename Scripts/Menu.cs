using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Menu : Control
{
	List<int> goBoackList = new();

	public void SwapMenu(int menuIndex, int returnIndex){
		if(GetChild(menuIndex) is MenuTab menuTab){
			menuTab.Visible = true;
		}

		if(returnIndex < 0) return;
		goBoackList.Add(returnIndex);
	}

	public void SwapMenuToPrevious(){
		if(!goBoackList.Any()) return;
		SwapMenu(goBoackList[goBoackList.Count - 1], -1);
		goBoackList.RemoveAt(goBoackList.Count - 1);
	}

	public void OnSwapScene(PackedScene loadScene){
		GetTree().Root.AddChild(loadScene.Instantiate());
		QueueFree();
	}

	private void OnQuitGameBtnPressed(){
		GetTree().Quit();
	}
}
