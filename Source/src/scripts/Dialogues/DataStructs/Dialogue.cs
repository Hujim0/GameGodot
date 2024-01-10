using Godot;

namespace GodotGame.Dialogues
{
    [System.Serializable]
    public partial class Dialogue
    {
        /// <summary>
        /// Higher the priority is, later it will apear.
        /// </summary>
        [Export] public int prior;

        [Export] public DialoguePanel[] panels;

        /// <param name="type"></param>
        /// <param name="priority"></param>
        /// <param name="dialoguePanels"></param>
        public Dialogue(int priority, DialoguePanel[] dialoguePanels)
        {
            prior = priority;
            panels = dialoguePanels;
        }

        public void DebugThisDialogue()
        {
            if (panels[0].txt == null)
            {
                string responces = string.Empty;

                foreach (DialogueResponse resp in panels[0].resps)
                {
                    responces += $"/{resp.responseText}";
                }

                GD.Print($"Choice: {responces}");
            }
            else
            {
                GD.Print($"Text: \"{panels[0].txt}\"");
            }
        }

        public static void DebugDialogue(Dialogue dialogue)
        {
            Dialogue dil = dialogue;

            if (dil.panels[0].txt == null)
            {
                string responces = string.Empty;

                foreach (DialogueResponse resp in dil.panels[0].resps)
                {
                    responces += $"/{resp.responseText}";
                }

                GD.Print($"Choice: {responces}");
            }
            else
            {
                GD.Print($"Text: \"{dil.panels[0].txt}\"");
            }
        }
    }
}