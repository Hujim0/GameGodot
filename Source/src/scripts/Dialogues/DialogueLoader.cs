using Godot;
using GodotGame.Dialogues;
using GodotGame.EventSystem;
using GodotGame.General;
using GodotGame.Serialization;
using System;
using System.Collections.Generic;

public partial class DialogueLoader
{
    const string STANDARD_DIRECTORY_NAME = @"Standart\";

    Dialogue[] result_dialogues = null;
    readonly RandomNumberGenerator rng = new RandomNumberGenerator();

    /// <summary>
    ///     Class for loading Dialogues
    /// </summary>
    /// <param name="data_path">
    ///     It's saved as {SceneName}/{NPCName}
    ///     <code>
    ///         Example: "Main/Wolf"
    ///     </code>
    /// </param>
    /// <param name="dialogue_priority"></param>
    /// 
    /// <param name="force_eventName">
    ///     Use this if you want to load dialogue from specific event, regardless exists it in SaveFile or not.
    /// </param>
    public DialogueLoader(string data_path, int dialogue_priority = 0, string force_eventName = "")
    {
        string generic_path = $@"{GameManager.Preferences.language}\{SerializationSystem.PathToDialogues}{data_path}\";

        GD.Print($"- Targeted priority: {dialogue_priority}");

        List<Dialogue> dialogues = new List<Dialogue>();

        if (!string.IsNullOrEmpty(force_eventName))
        {
            dialogues.AddRange(SerializationSystem.LoadDialogues($"{generic_path}{force_eventName}"));
        }
        else
        {
            string special_event = string.Empty;

            string[] events = SerializationSystem.GetDirectoryNames(generic_path);

            foreach (string event_name in events)
            {
                string new_event_name = System.IO.Path.GetFileName(event_name);

                if (!GameManager.GameEvents.Contains(new_event_name)) continue;

                special_event = new_event_name;
            }

            string tempFileName = null;

            if (special_event == string.Empty)
            {

                GD.Print($"- No Special Event found, loading standard");
                tempFileName = STANDARD_DIRECTORY_NAME;
            }
            else
            {
                GD.Print($"- Special Event found: \"{special_event}\"");
                tempFileName = special_event;

            }

            dialogues.AddRange(SerializationSystem.LoadDialogues($"{generic_path}{tempFileName}"));
        }

        for (int i = 0; i < dialogues.Count; i++)
        {
            if (dialogues[i].prior == dialogue_priority) continue;

            dialogues.RemoveAt(i);

            i--;
        }

        if (dialogues.Count == 0) 
        { 
            GD.PrintErr
                ($"!!! Failed to find any dialogues in \"{data_path}\", arg: {dialogue_priority}, special_events: \"{force_eventName}\" !!!");
            return;
        }

        result_dialogues = dialogues.ToArray();
    }

    public void StartDialogue()
    {
        if (result_dialogues == null) return;

        if (result_dialogues.Length == 1) { DialogueSystem.StartDialogue(result_dialogues[0]); return; }

        DialogueSystem.StartDialogue(result_dialogues[rng.RandiRange(0, result_dialogues.Length - 1)]);
    }

    public void InsertDialogues()
    {
        if (result_dialogues == null) return;

        if (result_dialogues.Length == 1) { DialogueSystem.InsertDialoguePanels(result_dialogues[0].panels); return; }

        DialogueSystem.InsertDialoguePanels(result_dialogues[rng.RandiRange(0, result_dialogues.Length - 1)].panels);
    }
}
