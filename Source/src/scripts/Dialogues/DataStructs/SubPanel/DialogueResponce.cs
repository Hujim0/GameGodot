using Godot;

namespace GodotGame.Dialogues
{
    public partial class DialogueResponse
    {
        [Export] public string responseText;

        [Export] public DialoguePanel[] panels;

        /// <param name="responceText"></param>
        /// <param name="panels"></param>
        public DialogueResponse(string responceText, DialoguePanel[] panels)
        {
            this.responseText = responceText;
            this.panels = panels;
        }
    }
}