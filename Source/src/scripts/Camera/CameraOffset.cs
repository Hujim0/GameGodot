using Godot;
using GodotGame.PlayerBehavior;

public partial class CameraOffset : Marker2D
{
	[Export] public Vector2 offset;

	public static Node2D Instance = null;

    public override void _Ready()
    {
		Instance = this;
    }

    public override void _Process(double delta)
	{
		if (Player.Instance == null) return;

		Position = new Vector2
		{
			X = Mathf.Abs(Player.Instance.velocity.X * offset.X),
			Y = Player.Instance.velocity.Y * offset.Y
		};
	}
}
