using Godot;

namespace GodotGame.Dialogues
{
    [System.Serializable]
    public partial class CharacterExpression
    {
        public string talk;

        public string end;

        public bool hl = false;

        public bool flip = false;

        public CharacterExpression(string animation_talk, string animation_onEnd, bool highlighted, bool flipped)
        {
            talk = animation_talk;
            end = animation_onEnd;
            hl = highlighted;
            flip = flipped;
        }

        public static CharacterExpression Empty => new CharacterExpression(string.Empty, string.Empty, false, false);

    }

}
