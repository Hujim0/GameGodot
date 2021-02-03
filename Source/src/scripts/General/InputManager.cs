using Godot;
using Godot.Collections;

public static class InputManager
{
    public static void ChangeBind(string actionName, InputEvent newEvent)
    {
        InputMap.ActionEraseEvents(actionName);
        InputMap.ActionAddEvent(actionName, newEvent);
    }
    public static void ChangeBindSpecific(string actionName, InputEvent oldEvent, InputEvent newEvent)
    {
        InputMap.ActionEraseEvent(actionName, oldEvent);
        InputMap.ActionAddEvent(actionName, newEvent);
    }

    public static Array GetControls() => InputMap.GetActions();
    public static void ResetControls() => InputMap.LoadFromGlobals();
}
