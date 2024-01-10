using Godot;
using GodotGame.Dialogues;
using GodotGame.PlayerBehavior;
using System;
using System.Collections.Generic;

namespace GodotGame.PlayerBehavior.Interaction
{
	public partial class PlayerInteraction : Area2D
	{
		List<IIntractable> list = new List<IIntractable>();

		int lastIndexInList = 0;

        public override void _Ready() => DialogueSystem.OnToggled += ctx => SetProcessUnhandledInput(!ctx);

        public override void _UnhandledInput(InputEvent @event)
		{
			if (!@event.IsActionPressed(Player.INPUT_INTERACT) || list.Count == 0) return;

			if (list[lastIndexInList].IsIntractable) list[lastIndexInList].OnInteracted();

			if (list.Count - 1 != lastIndexInList) { lastIndexInList++; return; }

			lastIndexInList = 0;
		}

		public void OnAreaEntered(PhysicsBody2D node)
        {
			if (!(node.GetChildOrNull<IIntractable>(0) is IIntractable intractable)
				||	!intractable.IsIntractable) return;

            intractable.IsHighLighted = true;

            list.Add(intractable);

            lastIndexInList = 0;
        }

        public void OnAreaExited(PhysicsBody2D node)
		{
			if (!(node.GetChildOrNull<IIntractable>(0) is IIntractable intractable)
				|| !intractable.IsIntractable) return;

			intractable.IsHighLighted = false;

			list.Remove(intractable);

			lastIndexInList = 0;
		}
	}
}