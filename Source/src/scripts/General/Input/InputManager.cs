using Godot;
using System.Collections.Generic;

namespace GodotGame.Inputs
{
    public static class InputManager
    {
        public static void PrintCurrentControls()
        {
            foreach (InputKey key in InputManager.GetControls())
            {
                GD.Print($"--- {key.actionName}: --- [");
                foreach (InputEvent @event in key.events)
                {
                    GD.Print($"{@event.AsText()},");
                }
                GD.Print($"]");
            }
        }

        public static void SetControls(IEnumerable<InputKey> inputKeys)
        {
            foreach (InputKey key in inputKeys)
            {
                InputMap.ActionEraseEvents(key.actionName);

                foreach (InputEvent @event in key.events)
                    InputMap.ActionAddEvent(key.actionName, @event);
            }
        }

        public static InputKey[] GetControls()
        {
            Queue<InputKey> result = new Queue<InputKey>();

            foreach (string action in InputMap.GetActions())
            {
                result.Enqueue(new InputKey(
                
                    actionName: action,

                    events: InputMap.ActionGetEvents(action)
                ));
            }

            return result.ToArray();
        }

        public static void ChangeBind(string actionName, InputEvent @event)
        {
            InputMap.ActionEraseEvents(actionName);

            InputMap.ActionAddEvent(actionName, @event);
        }

        public static void ResetControls() => InputMap.LoadFromProjectSettings();
    }
}