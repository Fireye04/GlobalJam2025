using Godot;
using System;

public partial class Background : CanvasLayer
{
    [Export]
    public ELevelType levelType;

    [Export]
    public Texture2D good;

    [Export]
    public Texture2D bad;

    [Export]
    public Texture2D weird;

    [Export]
    public TextureRect tr;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        if (levelType == ELevelType.Good) {
            tr.Texture = good;
        } else if (levelType == ELevelType.Bad) {
            tr.Texture = bad;
        } else if (levelType == ELevelType.Weird) {
            tr.Texture = weird;
        }

	}

}
