using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{

	[Export]
	public Control prompt;

	[Export]
	public ELevelType Variant = ELevelType.Good;

	[Export]
	public AnimatedSprite2D anim;

	public InteractionBox box;

	[Export]
	public Node2D respawnLocation;

	private Node2D currentNpc;

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

	[Export]
	public float BubbleStrength = -1500f;

	[Export]
	public Timer timeBoi;

	[Export]
	public Sprite2D bubble;

	private float Speed;

	private float acceleration;

	private float friction;
	
	private bool pointingRight = true;

	private bool jumping = false;

	public Vector2 inputDirection;

	private bool doingthing = false;

	public bool bubblesJump = false;

	public bool bubble_on = false;

	private bool cooldown = false;

    public bool interacting = false;

    public override void _Ready() {
        GameState.pboi = this;
        box = GetNode<InteractionBox>("%Interaction Box");
    }

    
    
	public override void _UnhandledInput(InputEvent @event){

		//Interaction System
		if (@event.IsActionPressed("interact")) {
			Node2D target = box.find_nearest_interactable();

			if (target != null) {

				IInteractable iTarget = (IInteractable)target;
				if (iTarget.canInteract()) {

                    interacting = true;
					iTarget.interact();
					inputDirection = new Vector2();
					return;
				}
			} else {
				GD.Print("None in Range");
			}
		}

		if (@event.IsActionPressed("jump") && IsOnFloor()) {
			jumping = true;
		}

		if (@event.IsActionPressed("bubble") && !IsOnFloor() && timeBoi.IsStopped()) {
			Velocity= new Vector2();
			bubble_on = true;
			bubble.Visible = true;
			cooldown = true;
			timeBoi.Start();
		}

		// what the user is inputting 
		inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Speed = PlayerSpeed;
		acceleration = PlayerAcceleration;
		friction = PlayerFriction;
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			if (!bubble_on) {
				velocity += GetGravity() * (float)delta;
			} else {
				/*velocity.Y = 0;*/
			}
			acceleration = PlayerAcceleration/2;
		} 

		// Handle Jump.
		if (jumping)
		{
			velocity.Y = JumpVelocity;
			jumping = false;
		}

		// Handle bubble velocity
		if(bubblesJump) {
			velocity.Y += BubbleStrength * (float)delta;
		}

		// Get the input direction and handle the movement/deceleration.

		// what is needed to compute movement
		float direction = inputDirection.X;
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



	//Animation handler
	private String GetCurrentAnim(String anim) {
		// Animation names are formatted:
		// animName_direction_levelVariant
		String vr = "_good";
		String lr = "_left";

		if (Variant == ELevelType.Bad) {
			vr = "_bad";
		} else if (Variant == ELevelType.Weird) {
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

	
	private void _on_area_2d_body_shape_entered(Rid body_rid, Node2D body, int body_shape_index, int local_shape_index) {
		if (body is TileMapLayer){
			this.Transform = respawnLocation.Transform;
		}
	}

	private void _on_timer_timeout() {
		if (cooldown) {
			timeBoi.Start(1);
		}
		bubble_on = false;
		bubble.Visible = false;
		cooldown = false;
	}
}
