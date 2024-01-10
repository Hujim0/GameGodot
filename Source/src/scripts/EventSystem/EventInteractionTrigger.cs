using Godot;
using GodotGame.PlayerBehavior.Interaction;
using System;

namespace GodotGame.EventSystem
{
	public partial class EventInteractionTrigger : IIntractableHighlightable
	{
		[Export] public EVENT_TYPE type;

		[Export] public string data_path;

		[Export] public Vector2 arg;

		public Action OnEventStarted;

		Event @event;

		public override void _Ready()
		{
			SceneManager.OnSceneInstance += ctx => UpdateEvent();

			UpdateEvent();
		}

        private void UpdateEvent()
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
