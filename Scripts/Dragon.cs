using Godot;
using System;

public partial class Dragon : StaticBody2D, IInteractable
{
    [Export] 
    public AnimatedSprite2D anim;

    [Export]
    public Resource dialogue;

    [Export]
    public String startNode;

    [Export]
    public bool Can_interact = true;

    public override void _Ready () {
        anim.Play("default");
    }

    public void interact() {
        GD.Print("fuck");
    }

    public bool canInteract() {
        return Can_interact;
    }
}
