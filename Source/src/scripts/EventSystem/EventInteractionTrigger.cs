using Godot;
using GodotGame.PlayerBehaviour.Interaction;
using System;

namespace GodotGame.EventSystem
{
    public class EventInteractionTrigger : IInteractableHighLightable
    {
        [Export] EVENT_TYPE type;

        IEvent @event;

        public override void OnInteracted()
        {
            @event.Invoke();
        }
    }
}