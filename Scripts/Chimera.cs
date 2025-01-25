using Godot;
using System;
using DialogueManagerRuntime;

public partial class Chimera: StaticBody2D, IInteractable
{
    [Export] 
    public AnimatedSprite2D spriteAnim;

    [Export] 
    public AnimationPlayer anim;

    [Export]
    public Resource dialogue;

    [Export]
    public String startNode;

    [Export]
    public bool Can_interact = true;

    public override void _Ready () {
        spriteAnim.Play("default");
    }

    public void interact() {
		DialogueManager.ShowExampleDialogueBalloon(dialogue, "start");
        anim.Play("fade_out");
    }

    public bool canInteract() {
        return Can_interact;
    }

}
