using Godot;

namespace GodotGame.Dialogues.UI
{
	public partial class ResponseScript : Button
	{
		public const float ANIM_DURATION = 0.75f;
		public const float ANIM_HEIGHT = 30f;

		public DialogueResponse response;
		
		Timer timer = null;
		Tween tween = null;

		public override void _Pressed() => DialogueResponseUI.ActivateResponse(response);
		public override void _EnterTree()
		{
			Modulate = new Color(1, 1, 1, 0);
			timer = GetNode<Timer>("Timer");
			tween = GetNode<Tween>("Tween");
		}
		public void SetResponse(DialogueResponse response)
		{
			this.response = response;

			Text = response.responseText;
		}

		/// <param name="size"></param>
		/// <param name="position"></param>
		public void SetRect(Vector2 size, Vector2 position, int count = 0)
		{
			Size = size;
			Position = position;

			if (count == 0) { OnTimerTimeOut(); return; }
			timer.Start((ANIM_DURATION / 4) * count);
		}

		public void OnTimerTimeOut()
		{
			tween.TweenProperty(
				this,
				"rect_position",
				Position - new Vector2(0, ANIM_HEIGHT),
				ANIM_DURATION).AsRelative().SetTrans(Tween.TransitionType.Circ);
				// Tween.EaseType.Out);
			tween.TweenProperty(
				this,
				"modulate",
				new Color(1, 1, 1, 1),
				ANIM_DURATION).AsRelative().SetTrans(Tween.TransitionType.Circ);
				// Tween.EaseType.Out);
			tween.Play();
		}
	}
}

