using Godot;
using GodotGame.UI;
using System;

namespace GodotGame.Dialogues.UI
{
	public class DialogueUI : DialogueSystem
	{
		public static Action TalkEnded;

		public static float textPanelHeight = 50f;

		bool isTyping = false;

		DialogueNameUI nameText = null;

		CanvasModulate modulate = null;
		Timer timerNode = null;
		Tween tween = null;
		TextTyper typer = null;

		#region consts

		const float AnimationDuration = 0.25f;

		const string NodePathToTextTyper = "TextPanel/TextContainer/Text";
		const string NodePathToName = "NamePanel/NameContainer/Label";
		const string NodePathToTimer = "Timer";
		const string NodePathToModulate = "CanvasModulate";
		const string NodePathToTween = "Tween";

		const string INPUT_NEXT = "dl_next";

		#endregion

		public override void _Ready()
		{
			typer = GetNode<TextTyper>(NodePathToTextTyper);
			nameText = GetNode<DialogueNameUI>(NodePathToName);
			timerNode = GetNode<Timer>(NodePathToTimer);
			modulate = GetNode<CanvasModulate>(NodePathToModulate);
			tween = GetNode<Tween>(NodePathToTween);

			OnToggled += SetVisibleUI;
            OnToggled += SetProcessInput;
		
			OnPanelChanged += ShowNextPanel;

			typer.StopedTyping += ResetTyping;

			modulate.Color = new Color(1, 1, 1, 0);

			SetProcessInput(false);
		}
        public override void _Input(InputEvent @event)
		{
			if (DialogueResponceUI.inChoose) return;

			if (Input.IsActionJustPressed(INPUT_NEXT))
			{
				if (isTyping) { typer.Stop(); return; }

				NextSentence();
			}
		}

		void ResetTyping()
		{
			isTyping = false;
			TalkEnded?.Invoke();
		}

		void ShowNextPanel(DialoguePanel panel)
		{
			if (string.IsNullOrEmpty(panel.txt))
            {
				if (panel.resps == null) { GD.PrintErr("!!! Text and Responces are NULL !!!"); return; }

				typer.Reset();

				GD.Print(DialogueResponceUI.Instance);

				DialogueResponceUI.Instance.InstantiateButtons(panel.resps);

				return;
            }

			if (string.IsNullOrEmpty(panel.name))
            {
				nameText.Hide(true);
            }
			else
            {
				nameText.Text = panel.name;
				nameText.Hide(false);
			}

			isTyping = true;

			typer.TypeSentence(panel.txt, panel.time);
		} 
		void SetVisibleUI(bool visibility)
		{
			if (visibility)
			{
				tween.InterpolateProperty(modulate, "color", 
					null,
					new Color(1, 1, 1, 1), 
					AnimationDuration, 
					Tween.TransitionType.Linear, 
					Tween.EaseType.In);
				tween.Start();
			}
			else
            {
				tween.InterpolateProperty(modulate, "color",
					null,
					new Color(1, 1, 1, 0),
					AnimationDuration,
					Tween.TransitionType.Linear,
					Tween.EaseType.Out);
				tween.Start();
			}
		}
	}
}
