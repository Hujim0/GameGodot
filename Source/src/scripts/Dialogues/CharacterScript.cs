using Godot;

namespace GodotGame.Dialogues
{
	public partial class CharacterScript : TextureRect
	{
		const string nodePathToTween = "Tween";
		const string nodePathToAnim = "AnimationPlayer";

		const float highlight_anim_duration = 0.5f;

		public Vector2 standardScale = Vector2.Zero;

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

				tween.Stop();

				if (standardScale == Vector2.Zero)
					standardScale = Scale;

				if (value)
				{
					tween.TweenProperty(this,
						"modulate",
						// new Color(0.75f, 0.75f, 0.75f, 1f),
						new Color(1f, 1f, 1f, 1f),
						highlight_anim_duration).AsRelative().SetTrans(Tween.TransitionType.Cubic);
						// Tween.TransitionType.Cubic,
						// Tween.EaseType.Out);
						
					tween.TweenProperty(this,
						"rect_scale",
						// standardScale,
						standardScale * 1.25f,
						highlight_anim_duration).AsRelative().SetTrans(Tween.TransitionType.Cubic);
						// Tween.TransitionType.Cubic,
						// Tween.EaseType.Out);
				}
				else
				{
					tween.TweenProperty(this,
						"modulate",
						// new Color(1f, 1f, 1f, 1f),
						new Color(0.75f, 0.75f, 0.75f, 1f),
						highlight_anim_duration / 1.5d).AsRelative().SetTrans(Tween.TransitionType.Cubic);
						// ,
						// Tween.EaseType.Out);
					tween.TweenProperty(this,
						"rect_scale",
						// standardScale * 1.25f,
						standardScale,
						highlight_anim_duration / 1.5d).AsRelative().SetTrans(Tween.TransitionType.Cubic);
						// Tween.TransitionType.Cubic,
						// Tween.EaseType.Out);
				}
				
				tween.Play();
			}
		}

		public override void _Ready()
		{
			anim = GetNode<AnimationPlayer>(nodePathToAnim);
			tween = GetNode<Tween>(nodePathToTween);
			Modulate = new Color(0.75f, 0.75f, 0.75f, 1f);
		}
	}
}
