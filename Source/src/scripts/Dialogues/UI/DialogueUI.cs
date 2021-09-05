using Godot;
using GodotGame.EventSystem;
using GodotGame.UI;
using System;

namespace GodotGame.Dialogues.UI
{
	public class DialogueUI : Control
	{
		public static Action TalkEnded;

		public static float textPanelHeight = 50f;

		bool isTyping = false;

		DialogueNameUI nameText = null;

		CanvasModulate modulate = null;
		Tween tween = null;
		TextTyper typer = null;
		Timer timer = null;

		#region consts

		const float AnimationDuration = 0.25f;

		const string NodePathToTextTyper = "TextPanel/TextContainer/Text";
		const string NodePathToName = "NamePanel/NameContainer/Label";
		const string NodePathToModulate = "CanvasModulate";
		const string NodePathToTween = "Tween";

		const string INPUT_NEXT = "dl_next";

		#endregion

		public override void _Ready()
		{
			typer = GetNode<TextTyper>(NodePathToTextTyper);
			nameText = GetNode<DialogueNameUI>(NodePathToName);
			modulate = GetNode<CanvasModulate>(NodePathToModulate);
			tween = GetNode<Tween>(NodePathToTween);
			timer = GetNode<Timer>("Timer");

			DialogueSystem.OnToggled += SetVisibleUI;
			DialogueSystem.OnToggled += SetProcessInput;

			DialogueSystem.OnPanelChanged += ShowNextPanel;

			typer.StopedTyping += ResetTyping;

			modulate.Color = new Color(1, 1, 1, 0);

			SetProcessInput(false);
		}
        public override void _Input(InputEvent @event)
		{
			if (DialogueResponceUI.inChoose) return;

			if (Input.IsActionJustReleased(INPUT_NEXT))
			{
				if (isTyping) { typer.Stop(); return; }

				DialogueSystem.NextSentence();
			}
		}

		void ResetTyping()
		{
			isTyping = false;
			TalkEnded?.Invoke();
		}

		void ShowNextPanel(DialoguePanel panel)
        {
            if (panel.evnt != null)
            {
                new Event(panel.evnt.type, panel.evnt.data_path, panel.evnt.arg, panel.evnt.specialarg).Invoke();

                DialogueSystem.NextSentence();
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

            if (!string.IsNullOrEmpty(panel.txt))
            {
                isTyping = true;

                typer.TypeSentence(panel.txt, panel.time);
            }
            else
            {
                if (panel.resps == null) { GD.PrintErr("!!! Text and Responces are NULL !!!"); return; }

                typer.Reset();

                DialogueResponceUI.Instance.InstantiateButtons(panel.resps);
            }
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
