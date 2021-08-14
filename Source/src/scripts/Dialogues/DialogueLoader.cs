using Godot;
using GodotGame.Dialogues;
using GodotGame.General;
using GodotGame.Serialization;
using System;
using System.Collections.Generic;

public class DialogueLoader
{
    const string NPCDATAFILENAME = "npcdata.json";

    const string STANDARTDIRECTORYNAME = @"Standart\";

    Dialogue[] resultdialogues;
    RandomNumberGenerator rng = new RandomNumberGenerator();

    public DialogueLoader(string data_path, int arg = 0)
    {
        string genericpath = $@"{GameManager.Preferences.language}\{SerializationSystem.PathToDialogues}{data_path}";

        NPCdata npc = SerializationSystem.LoadDataGeneric<NPCdata>
            ($@"{genericpath}\{NPCDATAFILENAME}");
        
        int relationship = GameManager.GetRelationship(npc.npc_name);

        IEnumerable<string> events = SerializationSystem.GetDirectoryNames(genericpath);

        string specialevent = string.Empty;

        foreach (string eventname in events)
        {
            if (!GameManager.GameEvents.Contains(eventname)) continue;

            specialevent = eventname;
        }

        List<Dialogue> dialogues = new List<Dialogue>();

        if (specialevent == string.Empty)
        {
            dialogues.AddRange(SerializationSystem.LoadDialogues($@"{genericpath}\{STANDARTDIRECTORYNAME}\{relationship}"));
        }
        else
        {
            dialogues.AddRange(SerializationSystem.LoadDialogues($@"{genericpath}\{specialevent}"));
        }

        for (int i = 0; i < dialogues.Count; i++)
        {
            if (dialogues[i].prior == arg) continue;

            dialogues.RemoveAt(i);

            i--;
        }

        resultdialogues = dialogues.ToArray();
    }

    public void StartDialogue()
    {
        if (resultdialogues.Length == 1) { DialogueSystem.StartDialogue(resultdialogues[0]); return; }

        DialogueSystem.StartDialogue(resultdialogues[rng.RandiRange(0, resultdialogues.Length - 1)]);
    }
}
