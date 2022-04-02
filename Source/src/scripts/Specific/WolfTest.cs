using Godot;
using GodotGame.General;
using System;

namespace GodotGame
{
	[Serializable]
	public class WolfTest : Node2D
	{
		[Export] Vector2 defaultPos;

		[Export] Vector2[] cords;

		[Export] string[] eventNames;

		[Export] string endEventName;

		Node2D parent;

		Sprite parentSprite;

		RandomNumberGenerator rng = new RandomNumberGenerator();

		public override void _Ready()
		{
			SceneManager.OnSceneInstance += UpdatePos;

			parent = GetNode<Node2D>("..");
			parentSprite = GetNode<Sprite>("../Sprite");

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
				parentSprite.Offset = new Vector2(-5f, parentSprite.Offset.y);
			}
			else
			{
				parentSprite.FlipH = false;
				parentSprite.Offset = new Vector2(5f, parentSprite.Offset.y);
			}
		}
	}

}
