using Godot;

namespace GodotGame.Dialogues
{
    [System.Serializable]
    public class CharacterExpression
    {
        public string talk;

        public string end;

        public CharacterExpression(string animation_talk, string animation_onEnd)
        {
            talk = animation_talk;
            end = animation_onEnd;
        }

        public static CharacterExpression Empty => new CharacterExpression(string.Empty, string.Empty);

    }

}
