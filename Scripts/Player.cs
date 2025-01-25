using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public enum LevelType {
		Good,
		Bad,
		Weird
	}

	[Export]
	public Control prompt;

	[Export]
	public LevelType Variant = LevelType.Good;

	[Export]
	public AnimatedSprite2D anim;

	[Export]
	public InteractionBox box;

	// Differentiating exported values from used ones! 
	// Why? if I want to change a variable mid-run I want 
	// to have access to the original value to reset!
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
	
	private bool pointingRight = true;
	
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
			acceleration = PlayerAcceleration/2;
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
			pointingRight = direction > 0 ? true : false;

			anim.Play(GetCurrentAnim("move"));
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, friction);
			anim.Play(GetCurrentAnim("idle"));
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Process(double delta) {
		//Interaction System
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

	//Animation handler
	private String GetCurrentAnim(String anim) {
		// Animation names are formatted:
		// animName_direction_levelVariant
		String vr = "_good";
		String lr = "_left";

		if (Variant == LevelType.Bad) {
			vr = "_bad";
		} else if (Variant == LevelType.Weird) {
			vr = "_weird";
		}
		if (pointingRight) {
			lr = "_right";
		}
		return anim + lr + vr;
	}

	public void in_range (){
		prompt.Visible = true;
	}

		public void out_of_range (){
		prompt.Visible = false;
	}

}
