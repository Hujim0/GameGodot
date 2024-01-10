using Godot;
using GodotGame.EventSystem;

namespace GodotGame.Dialogues
{
    [System.Serializable]
    public partial class DialoguePanel
    {
        [Export] public string name;

        [Export] public string txt;

        [Export] public float time;

        [Export] public CharacterExpression[] chars;

        [Export] public DialogueResponse[] resps;

        [Export] public EventData evnt = null;

        /*        [Export] public Sound sound;*/

        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="timeBetweenCharacters"></param>
        /// <param name="characters"></param>
        /// <param name="responces"></param>
        /// <param name="event"></param>
        public DialoguePanel(string name, string text, float timeBetweenCharacters = 0.05f, CharacterExpression[] characters = null, DialogueResponse[] responces = null, EventData @event = null)
        {
            this.name = name;
            txt = text;
            time = timeBetweenCharacters;
            chars = characters;
            resps = responces;
            evnt = @event;
        }
    }
}
