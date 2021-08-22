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

    public DialogueLoader(string data_path, int arg = 0, string specialarg = "")
    {
        string genericpath = $@"{GameManager.Preferences.language}\{SerializationSystem.PathToDialogues}{data_path}\";

  /*      SerializationSystem.SaveDataGeneric
            (new NPCdata() { npc_name = "null" }, $@"{SerializationSystem.PathToLanguages}{genericpath}\{NPCDATAFILENAME}");
*/
        NPCdata npc = SerializationSystem.LoadDataGeneric<NPCdata>
            ($"{SerializationSystem.PathToLanguages}{genericpath}{NPCDATAFILENAME}");

        int relationship = GameManager.GetRelationship(npc.npc_name);

        GD.Print($"- NPC name: {npc.npc_name}, relationship: {relationship}");

        List<Dialogue> dialogues = new List<Dialogue>();

        if (!string.IsNullOrEmpty(specialarg))
        {
            dialogues.AddRange(SerializationSystem.LoadDialogues($"{genericpath}{specialarg}"));
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

            if (specialevent == string.Empty)
            {
                dialogues.AddRange(SerializationSystem.LoadDialogues($"{genericpath}{STANDARTDIRECTORYNAME}{relationship}"));
            }
            else
            {
                dialogues.AddRange(SerializationSystem.LoadDialogues($"{genericpath}{specialevent}"));
            }
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

    public void InsertDialogues()
    {
        if (resultdialogues.Length == 1) { DialogueSystem.InsertDialoguePanels(resultdialogues[0].panels); return; }

        DialogueSystem.InsertDialoguePanels(resultdialogues[rng.RandiRange(0, resultdialogues.Length - 1)].panels);
    }
}
