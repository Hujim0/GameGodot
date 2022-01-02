using Godot;

namespace GodotGame.Dialogues
{
	public class CharacterScript : TextureRect
	{
		const string nodePathToTween = "Tween";
		const string nodePathToAnim = "AnimationPlayer";

		const float highlight_anim_duration = 0.5f;

		public Vector2 standartScale = Vector2.Zero;

		public AnimationPlayer anim = null;

		Tween tween = null;

		bool isHighlighted = false;

		public bool IsHighlighted
		{
			get => isHighlighted;
			set
			{
				if (isHighlighted == value) return;

				isHighlighted = value;

				tween.StopAll();

				if (standartScale == Vector2.Zero)
					standartScale = RectScale;

				if (value)
				{
					tween.InterpolateProperty(this,
						"modulate",
						new Color(0.75f, 0.75f, 0.75f, 1f),
						new Color(1f, 1f, 1f, 1f),
						highlight_anim_duration,
						Tween.TransitionType.Cubic,
						Tween.EaseType.Out);
						
					tween.InterpolateProperty(this,
						"rect_scale",
						standartScale,
						standartScale * 1.25f,
						highlight_anim_duration,
						Tween.TransitionType.Cubic,
						Tween.EaseType.Out);
				}
				else
				{
					tween.InterpolateProperty(this,
						"modulate",
						new Color(1f, 1f, 1f, 1f),
						new Color(0.75f, 0.75f, 0.75f, 1f),
						highlight_anim_duration / 1.5f,
						Tween.TransitionType.Cubic,
						Tween.EaseType.Out);
					tween.InterpolateProperty(this,
						"rect_scale",
						standartScale * 1.25f,
						standartScale,
						highlight_anim_duration / 1.5f,
						Tween.TransitionType.Cubic,
						Tween.EaseType.Out);
				}
				
				tween.Start();
			}
		}

		public override void _Ready()
		{
			anim = GetNode<AnimationPlayer>(nodePathToAnim);
			tween = GetNode<Tween>(nodePathToTween);
			Modulate = new Color(0.75f, 0.75f, 0.75f, 1f);

			GD.Print("asdsad");
		}
	}
}
