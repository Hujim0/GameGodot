using Godot;
using GodotGame.PlayerBehaviour.Interaction;
using System;

namespace GodotGame.EventSystem
{
    public class EventInteractionTrigger : IInteractableHighLightable
    {
        [Export] public EVENT_TYPE type;

        public string data_path;

        public int arg = 0;

        public Action OnEventStarted;

        IEvent @event;

        public override void _Ready()
        {
            @event = new Event(type, data_path, arg);
        }

        public override void OnInteracted()
        {
            @event.Invoke();
        }
    }
}