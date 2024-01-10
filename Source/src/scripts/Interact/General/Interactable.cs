
using Godot;
using GodotGame.Dialogues;
using System;

namespace GodotGame.PlayerBehavior.Interaction
{
	public partial class Interactable : IIntractableHighlightable
	{
		public Action OnInteraction;

		public override void OnInteracted()
		{
			if (!IsIntractable) return;
			OnInteraction?.Invoke();
		}
	}
}