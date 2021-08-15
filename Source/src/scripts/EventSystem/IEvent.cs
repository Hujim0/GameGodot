using Godot;
using System;

namespace GodotGame.EventSystem
{

    public interface IEvent
    {
        void Invoke();
    }
}