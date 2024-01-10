using Godot;
// using GodotGame;
using GodotGame.PlayerBehavior;
// using System;

public partial class Camera2DScript : Camera2D
{
    public Node2D target;

    Tween tween;

    public override void _Ready()
    {
        target = Player.Instance;
        tween = GetNode<Tween>("Tween");

    }
    public override void _Process(double delta)
    {
        Position = new Vector2(Mathf.Floor(target.GlobalPosition.X) + 0.5f, 
            Mathf.Floor(target.GlobalPosition.Y) - 32.5f);
    }
}
