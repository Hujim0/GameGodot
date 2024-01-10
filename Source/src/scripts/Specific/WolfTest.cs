using Godot;
using GodotGame.General;
using System;

namespace GodotGame
{
	[Serializable]
	public partial class WolfTest : Node2D
	{
		[Export] public Vector2 defaultPos;

		[Export] public Vector2[] cords;

		[Export] public string[] eventNames;

		[Export] public string endEventName;

		Node2D parent;

		Sprite2D parentSprite;

		RandomNumberGenerator rng = new RandomNumberGenerator();

		public override void _Ready()
		{
			SceneManager.OnSceneInstance += UpdatePos;

			parent = GetNode<Node2D>("..");
			parentSprite = GetNode<Sprite2D>("../Sprite2D");

			rng = new RandomNumberGenerator();


		}

		private void UpdatePos(Vector2 _)
		{
			if (GameManager.GameEvents.Contains(endEventName))
			{
				ParentChange(defaultPos, true);

				return;
			}

			foreach (string @event in eventNames)
			{
				if (GameManager.GameEvents.Contains(@event))
				{
					ParentChange(cords[rng.RandiRange(0, cords.Length - 1)], false);

					return;

				}
			}


			ParentChange(defaultPos, true);
		}

		void ParentChange(Vector2 pos, bool facingRight)
		{
			parent.Position = pos;

			if (facingRight)
			{
				parentSprite.FlipH = true;
				parentSprite.Offset = new Vector2(-5f, parentSprite.Offset.Y);
			}
			else
			{
				parentSprite.FlipH = false;
				parentSprite.Offset = new Vector2(5f, parentSprite.Offset.Y);
			}
		}
	}

}
