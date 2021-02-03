
using Godot;
using GodotGame.Dialogues;
using System;

namespace GodotGame.PlayerBehaviour.Interaction
{
	public class Interactable : IInteractableHighLightable
	{
		public Action OnInteraction;

		public override void OnInteracted()
		{
			if (!IsInteractable) return;
			OnInteraction?.Invoke();
		}
	}
}