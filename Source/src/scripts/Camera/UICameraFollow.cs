using Godot;
using GodotGame.UI;

namespace GodotGame.Camera3D
{
	public partial class UICameraFollow : Marker2D
	{
		Camera2D cam = null;

		public override void _Ready()
		{
			cam = GetNode<Camera2D>($"{PlayerBehavior.Player.Instance.GetPath()}/CenterPivot/CameraPivot/Camera2D");
		}

		public override void _Process(double delta)
		{
			Position = cam.GetScreenCenterPosition() - (GetViewportRect().Size / 2);
		}
	}
}
