using Godot;
using System;

namespace GodotGame.PlayerBehaviour
{

	public class PlayerSpriteOffset : Sprite
	{


		public override void _Process(float delta)
		{
			GlobalPosition = new Vector2(Mathf.Floor(Player.Instance.GlobalPosition.x), Mathf.Floor(Player.Instance.GlobalPosition.y));
		}
	}
}
