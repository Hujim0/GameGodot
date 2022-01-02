using Godot;

namespace GodotGame.Dialogues.UI
{
	public class ResponceScript : Button
	{
		public const float ANIM_DURATION = 0.75f;
		public const float ANIM_HEIGHT = 30f;

		public DialogueResponce responce;
		
		Timer timer = null;
		Tween tween = null;

		public override void _Pressed() => DialogueResponceUI.ActivateResponce(responce);
		public override void _EnterTree()
		{
			Modulate = new Color(1, 1, 1, 0);
			timer = GetNode<Timer>("Timer");
			tween = GetNode<Tween>("Tween");
		}
		public void SetResponce(DialogueResponce responce)
		{
			this.responce = responce;

			Text = responce.responceText;
		}

		/// <param name="size"></param>
		/// <param name="position"></param>
		public void SetRect(Vector2 size, Vector2 position, int count = 0)
		{
			RectSize = size;
			RectPosition = position;

			if (count == 0) { OnTimerTimeOut(); return; }
			timer.Start((ANIM_DURATION / 4) * count);
		}

		public void OnTimerTimeOut()
		{
			tween.InterpolateProperty(
				this,
				"rect_position",
				null,
				RectPosition - new Vector2(0, ANIM_HEIGHT),
				ANIM_DURATION,
				Tween.TransitionType.Circ,
				Tween.EaseType.Out);
			tween.InterpolateProperty(
				this,
				"modulate",
				new Color(1, 1, 1, 0),
				new Color(1, 1, 1, 1),
				ANIM_DURATION,
				Tween.TransitionType.Circ,
				Tween.EaseType.Out);
			tween.Start();
		}
	}
}

