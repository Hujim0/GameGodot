using Godot;
using GodotGame.Dialogues;
using GodotGame.EventSystem;
using GodotGame.General;
using GodotGame.Serialization;
using System;
using System.Collections.Generic;

public class DialogueLoader
{
    const string STANDARTDIRECTORYNAME = @"Standart\";

    Dialogue[] resultdialogues = null;
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
        string genericpath = $@"{GameManager.Preferences.language}\{SerializationSystem.PathToDialogues}{data_path}\";

  /*      SerializationSystem.SaveDataGeneric
            (new NPCdata() { npc_name = "null" }, $@"{SerializationSystem.PathToLanguages}{genericpath}\{NPCDATAFILENAME}");
*/
        GD.Print($"- Targeted priority: {dialogue_priority}");

        List<Dialogue> dialogues = new List<Dialogue>();

        if (!string.IsNullOrEmpty(force_eventName))
        {
            dialogues.AddRange(SerializationSystem.LoadDialogues($"{genericpath}{force_eventName}"));
        }
        else
        {
            string specialevent = string.Empty;

            IEnumerable<string> events = SerializationSystem.GetDirectoryNames(genericpath);

            foreach (string eventname in events)
            {
                string neweventname = System.IO.Path.GetFileName(eventname);

                if (!GameManager.GameEvents.Contains(neweventname)) continue;

                specialevent = neweventname;
            }

            string tempFileName = null;

            if (specialevent == string.Empty)
            {

                GD.Print($"- No Special Event found, loading standart");
                tempFileName = STANDARTDIRECTORYNAME;
            }
            else
            {
                GD.Print($"- Special Event found: \"{specialevent}\"");
                tempFileName = specialevent;

            }

            dialogues.AddRange(SerializationSystem.LoadDialogues($"{genericpath}{tempFileName}"));
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

        resultdialogues = dialogues.ToArray();
    }

    public void StartDialogue()
    {
        if (resultdialogues == null) return;

        if (resultdialogues.Length == 1) { DialogueSystem.StartDialogue(resultdialogues[0]); return; }

        DialogueSystem.StartDialogue(resultdialogues[rng.RandiRange(0, resultdialogues.Length - 1)]);
    }

    public void InsertDialogues()
    {
        if (resultdialogues == null) return;

        if (resultdialogues.Length == 1) { DialogueSystem.InsertDialoguePanels(resultdialogues[0].panels); return; }

        DialogueSystem.InsertDialoguePanels(resultdialogues[rng.RandiRange(0, resultdialogues.Length - 1)].panels);
    }
}
