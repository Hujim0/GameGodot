using Godot;
using GodotGame.Dialogues;
using GodotGame.PlayerBehaviour;
using System;
using System.Collections.Generic;

namespace GodotGame.PlayerBehaviour.Interaction
{
	public class PlayerInteraction : Area2D
	{
		List<IInteractable> list = new List<IInteractable>();

		int lastIndexInList = 0;

        public override void _Ready() => DialogueSystem.OnToggled += ctx => SetProcessUnhandledInput(!ctx);

        public override void _UnhandledInput(InputEvent @event)
		{
			if (!@event.IsActionPressed(Player.INPUT_INTERACT) || list.Count == 0) return;

			if (list[lastIndexInList].IsInteractable) list[lastIndexInList].OnInteracted();

			if (list.Count - 1 != lastIndexInList) { lastIndexInList++; return; }

			lastIndexInList = 0;

			GD.Print("a");
		}

		public void OnAreaEntered(PhysicsBody2D node)
        {
			if (!(node.GetChildOrNull<IInteractable>(0) is IInteractable interactable)
				||	!interactable.IsInteractable) return;

            interactable.IsHighLighted = true;

            list.Add(interactable);

            lastIndexInList = 0;
        }

        public void OnAreaExited(PhysicsBody2D node)
		{
			if (!(node.GetChildOrNull<IInteractable>(0) is IInteractable interactable)
				|| !interactable.IsInteractable) return;

			interactable.IsHighLighted = false;

			list.Remove(interactable);

			lastIndexInList = 0;
		}
	}
}