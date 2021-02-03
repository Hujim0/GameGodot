using Godot;

namespace GodotGame.Dialogues
{
    [System.Serializable]
    public class DialoguePanel
    {
        [Export] public string name;

        [Export] public string txt;

        [Export] public float time;

        [Export] public CharacterExpression[] chars;

        [Export] public DialogueResponce[] resps;

        /*        [Export] public Sound sound;*/

        /// <param name="name"></param>
        /// <param name="timeBetweenCharacters"></param>
        /// <param name="characters"></param>
        public DialoguePanel(string name, string text, float timeBetweenCharacters, CharacterExpression[] characters, DialogueResponce[] responces)
        {
            this.name = name;
            txt = text;
            time = timeBetweenCharacters;
            chars = characters;
            resps = responces;
        }
    }
}
