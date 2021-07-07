using Godot;
using GodotGame;
using GodotGame.PlayerBehaviour;
using System;

public class Camera2DScript : Camera2D
{
    public Node2D target;

    Tween tween;

    public override void _Ready()
    {
        target = Player.Instance;
        tween = GetNode<Tween>("Tween");

/*        Player.InputUpdated += UpdateTween;*/
    }

    public override void _PhysicsProcess(float delta)
    {
        /*        tween.Stop(this);

                tween.InterpolateProperty(this, 
                    "position", 
                    null, 
                    target.GlobalPosition, 
                    0.1f, 
                    Tween.TransitionType.Expo, 
                    Tween.EaseType.InOut);

                tween.Start();*/

        
    }

    public override void _Process(float delta)
    {
        Position = new Vector2(Mathf.Floor(target.GlobalPosition.x) + 0.5f, 
            Mathf.Floor(target.GlobalPosition.y) - 32.5f);
    }
}
