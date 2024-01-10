using Godot;
using System;

namespace GodotGame.PlayerBehavior
{

	public partial class PlayerSpriteOffset : Sprite2D
	{
		public override void _Process(double delta)
		{
			GlobalPosition = new Vector2(Mathf.Floor(Player.Instance.GlobalPosition.X), 
			Mathf.Floor(Player.Instance.GlobalPosition.Y));
		}
	}
}
