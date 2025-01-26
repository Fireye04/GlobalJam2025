using Godot;
using System;

public partial class Quicktime : CanvasLayer
{

    [Export]
    public Timer timeyBoi;

    [Export]
    public ProgressBar pBar;

    [Export]
    public Label countdown;

    [Export]
    public Label prompt;

    [Export]
    public float drainSpeed = 10;

    private int totalTime = 15;
    private Random rnd = new Random();

    private int currentKey = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        StartQT(15);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (totalTime != 0) {
            pBar.Value -= drainSpeed * delta;
        }

        if (pBar.Value == 0) {
            prompt.Text = "FAIL";
            totalTime = 0;
            timeyBoi.Stop();
            return;
        }

        if (Input.IsActionJustPressed("qt1")) {
           if (currentKey == 0){
                pBar.Value += 1;
           } 
        }

        if (Input.IsActionJustPressed("qt2")) {
           if (currentKey == 1){
                pBar.Value += 1;
           } 
        } 

        if (Input.IsActionJustPressed("qt3")) {
           if (currentKey == 2){
                pBar.Value += 1;
           } 
        }

	}

    public void StartQT(int seconds) {
        totalTime = seconds;
        startSection();

    }

    private void startSection() {
        if (totalTime == 0) {
            return;
        }
        int length = rnd.Next(1, ((int)(totalTime/2))+1);
        timeyBoi.Start(length);
        totalTime -= length;
        int key = rnd.Next(3);
        if (key == 0) {
            prompt.Text = "SPAM Z";
        } else if (key == 1) {
            prompt.Text = "SPAM X";
        } else {
            prompt.Text = "SPAM C";
        }
        currentKey = key;

    }

    private void _on_timer_timeout(){
        startSection();
        if (totalTime == 0) {
            prompt.Text = "SUCCESS";
        }
    }
    

}
