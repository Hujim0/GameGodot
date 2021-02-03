using Godot;
using GodotGame.UI;

namespace GodotGame.Camera
{
	public class UICameraFollow : Position2D
	{
		Camera2D cam = null;

		public override void _Ready()
		{
			cam = GetNode<Camera2D>($"{PlayerBehaviour.Player.Instance.GetPath()}/CenterPivot/CameraPivot/Camera2D");
		}

		public override void _Process(float delta)
		{
			Position = cam.GetCameraScreenCenter() - (GetViewportRect().Size / 2);
		}
	}
}
