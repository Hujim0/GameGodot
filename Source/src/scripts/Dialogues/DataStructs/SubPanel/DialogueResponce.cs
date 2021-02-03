using Godot;

namespace GodotGame.Dialogues
{
    public class DialogueResponce
    {
        [Export] public string responceText;

        [Export] public DialoguePanel[] panels;

        /// <param name="responceText"></param>
        /// <param name="panels"></param>
        public DialogueResponce(string responceText, DialoguePanel[] panels)
        {
            this.responceText = responceText;
            this.panels = panels;
        }
    }
}