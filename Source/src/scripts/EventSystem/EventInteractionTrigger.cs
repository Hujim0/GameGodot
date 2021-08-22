using Godot;
using GodotGame.PlayerBehaviour.Interaction;
using System;

namespace GodotGame.EventSystem
{
	public class EventInteractionTrigger : IInteractableHighLightable
	{
		[Export] public EVENT_TYPE type;

		[Export] public string data_path;

		[Export] public Vector2 arg;



		public Action OnEventStarted;

		IEvent @event;

		public override void _Ready()
		{
			if (data_path == "Null") GD.PrintErr($"At {this} data_path is null!");

			@event = new Event(type, data_path, arg);
		}

		public override void OnInteracted()
		{
			@event.Invoke();
		}
	}
}
