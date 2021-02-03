using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GodotGame.Inputs
{
    [Serializable]
    public struct InputKey
    {
        public string actionName;

        public IEnumerable events;

        /// <param name="actionName"></param>
        /// <param name="events"></param>
        public InputKey(string actionName, IEnumerable events)
        {
            this.actionName = actionName;
            this.events = events;
        }
    }
}
