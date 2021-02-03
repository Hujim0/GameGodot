using Godot;
using System;

namespace GodotGame.PlayerBehaviour.Interaction
{
	public class InteractionPoint : Position2D
	{
		[Export] public Vector2 offset = new Vector2(20f, 17.5f);

		public override void _Ready()
		{
			Player.InputUpdated += UpdatePosition;
		}

		void UpdatePosition(Vector2 input)
		{
			if (input == Vector2.Zero) return;

			Position = offset * new Vector2
			{
				x = Mathf.Abs(input.x),
				y = -input.y
			};
		}
	}
}
