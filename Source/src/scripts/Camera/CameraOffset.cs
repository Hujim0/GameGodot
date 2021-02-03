using Godot;
using GodotGame.PlayerBehaviour;

public class CameraOffset : Position2D
{
	[Export] public Vector2 offset;

	public override void _Process(float delta)
	{
		if (Player.Instance == null) return;

		Position = new Vector2
		{
			x = Mathf.Abs(Player.Instance.velocity.x * offset.x),
			y = Player.Instance.velocity.y * offset.y
		};
	}
}
