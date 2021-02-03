using Godot;

namespace GodotGame.Dialogues
{
	public class CharacterScript : TextureRect
	{
		const string nodePathToTween = "Tween";
		const string nodePathToAnim = "AnimationPlayer";
		const string nodePathToModulate = "CanvasModulate";

		public AnimationPlayer anim = null;

		Tween tween = null;

		CanvasModulate modulate = null;

		public override void _Ready()
		{
			anim = GetNode<AnimationPlayer>(nodePathToAnim);
			tween = GetNode<Tween>(nodePathToTween);
			/*		modulate = GetNode<CanvasModulate>(nodePathToModulate);

					tween.InterpolateProperty(modulate, "color", new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), 1f, Tween.TransitionType.Linear, Tween.EaseType.In);
					tween.Start();*/
		}
		public void Appear()
		{

		}
	}
}
