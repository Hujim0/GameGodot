using Godot;

namespace GodotGame.Dialogues
{
    [System.Serializable]
    public class Dialogue
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
    }
}