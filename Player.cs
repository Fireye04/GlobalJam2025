using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
	public float Speed = 300.0f;

    // Should be negative!
    [Export]
	public float JumpVelocity = -400.0f;
    
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.

        // what the user is inputting 
		Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        
        // what is needed to compute movement
		Vector2 direction = new Vector2(Input.GetAxis("move_left", "move_right"), 0);
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
