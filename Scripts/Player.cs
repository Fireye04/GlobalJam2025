using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export]
    public InteractionBox box;

    [Export]
	public float PlayerSpeed = 300.0f;

    [Export]
    public float PlayerAcceleration = 100.0f;

    [Export]
    public float PlayerFriction = 100.0f;

    // Should be negative!
    [Export]
	public float JumpVelocity = -500.0f;

    private float Speed;

    private float acceleration;

    private float friction;
    
	public override void _PhysicsProcess(double delta)
	{
        Speed = PlayerSpeed;
        acceleration = PlayerAcceleration;
        friction = PlayerFriction;
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		} else {

        }

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.

        // what the user is inputting 
		Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
        
        // what is needed to compute movement
		float direction = Input.GetAxis("move_left", "move_right");
		if (direction != 0)
		{
			velocity.X = Mathf.MoveToward(Velocity.X, direction * Speed, acceleration);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, friction);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

    public override void _Process(double delta) {
        if (Input.IsActionJustPressed("interact")) {
            Node2D target = box.find_nearest_interactable();

            if (target != null) {

                IInteractable iTarget = (IInteractable)target;
                if (iTarget.canInteract()) {

                    iTarget.interact();
                    return;
                }
            } else {
                GD.Print("None in Range");
            }
        }

    }

}
