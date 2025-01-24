using Godot;
using System;

public partial class test : StaticBody2D, IInteractable
{
    public void interact () {
        GD.Print("ive been interacted D: hello");
    }

    public bool canInteract() {
        return true;
    }

}
