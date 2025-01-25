using Godot;
using System;
using DialogueManagerRuntime;

public partial class Cats : StaticBody2D, IInteractable
{

    public enum cats {
        Black,
        White,
        WhiteFirst,
        BlackFirst
    }

    [Export]
    public cats state;

    [Export] 
    public AnimatedSprite2D spriteAnim1;

    [Export] 
    public AnimatedSprite2D spriteAnim2;

    [Export] 
    public AnimationPlayer anim;

    [Export]
    public Resource dialogue;

    [Export]
    public String startNode;

    [Export]
    public bool Can_interact = true;

    public override void _Ready () {
        if (state == cats.Black) {
            spriteAnim1.Play("Black");
        } else if (state == cats.White) {
            spriteAnim1.Play("White");
        } else if (state == cats.WhiteFirst) {
            spriteAnim1.Play("White");
            spriteAnim2.Play("Black");
        } else if (state == cats.BlackFirst) {
            spriteAnim1.Play("Black");
            spriteAnim2.Play("White");
        }

    }

    public void interact() {
        DialogueManager.ShowExampleDialogueBalloon(dialogue, startNode);
        DialogueManager.DialogueEnded += (Resource dialogueResource) =>
        {
            anim.Play("fade_out");
        };
    }

    public bool canInteract() {
        return Can_interact;
    }
}
