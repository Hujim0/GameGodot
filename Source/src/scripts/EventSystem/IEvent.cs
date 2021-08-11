using Godot;
using System;

namespace GodotGame.EventSystem
{
    public enum EVENT_TYPE { StartDialogue, SceneTransition, GiveItem };

    public interface IEvent
    {
        void Invoke();
    }
}