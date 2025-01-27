using Godot;
using System;

public partial class Quicktime : CanvasLayer
{
    [Signal]
    public delegate void CompletedEventHandler();

    [Export]
    public Timer timeyBoi;

    [Export]
    public Timer t2;

    [Export]
    public ProgressBar pBar;

    [Export]
    public Label countdown;

    public float timeleft;

    [Export]
    public Label prompt;

    [Export]
    public float drainSpeed = 14;

    private int totalTime = 15;
    private Random rnd = new Random();

    private int currentKey = 0;

    public bool succeeded = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        timeleft = totalTime;
        StartQT(totalTime);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (totalTime != 0) {
            pBar.Value -= drainSpeed * delta;
        }

        if (pBar.Value == 0) {
            prompt.Text = "FAIL";
            succeeded = false;
            totalTime = 0;
            timeyBoi.Stop();
            t2.Stop();
            EmitSignal(SignalName.Completed);
            return;
        }

        if (Input.IsActionJustPressed("qt1")) {
           if (currentKey == 0){
                pBar.Value += 2;
           } 
        }

        if (Input.IsActionJustPressed("qt2")) {
           if (currentKey == 1){
                pBar.Value += 2;
           } 
        } 

        if (Input.IsActionJustPressed("qt3")) {
           if (currentKey == 2){
                pBar.Value += 2;
           } 
        }

	}

    public void StartQT(int seconds) {
        totalTime = seconds;
        t2.Start();
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
            t2.Stop();
            prompt.Text = "SUCCESS";
            succeeded = true;
            EmitSignal(SignalName.Completed);

        }
    }

    private void _on_timer_2_timeout(){
        timeleft -= 0.1f;
        countdown.Text = Math.Round(timeleft, 1).ToString();
    }
    

}
