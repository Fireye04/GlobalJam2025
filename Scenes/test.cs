using Godot;
using System;

public partial class test : StaticBody2D, IInteractable
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void interact () {
        GD.Print("ive been interacted D:");
    }

    public bool canInteract() {
        return true;
    }

}
