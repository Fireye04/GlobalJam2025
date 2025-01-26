using Godot;
using System;

public partial class Quicktime : CanvasLayer
{

    [Export]
    public Timer timeyBoi;

    private int spamCount = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (Input.IsActionJustPressed("qt1")) {
            
        }

        if (Input.IsActionJustPressed("qt2")) {

        } 

        if (Input.IsActionJustPressed("qt3")) {

        }

	}

    public void StartQT() {
        spamCount = 0; 
    }

    

}
