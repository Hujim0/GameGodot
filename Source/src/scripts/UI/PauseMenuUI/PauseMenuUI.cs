using Godot;
using GodotGame.General;
using GodotGame.Serialization;
using GodotGame.Serialization.Localization;
using System;

namespace GodotGame.UI
{
	public partial class PauseMenuUI : Control
	{
		const string INPUT_PAUSE = "ui_pause";
		const string LOCALIZATIONFILE_NAME = "pauseMenu.json";

		static bool isPaused = false;

		public delegate void OnLocalizationChange(MenuLocalization localization);
		public static OnLocalizationChange LocalizationChange;

		public delegate void OnPauseStateChange(bool state);
		public static OnPauseStateChange PauseStateChanged;


		static CanvasItem OptionsNode = null;
/*        static CanvasItem particles = null;

		static Tween tween = null;*/

		const float AnimationDuration = 0.25f;

		public static bool IsPaused
		{
			get => isPaused;

			set
			{
				Instance.GetTree().Paused = value;
				Instance.Visible = value;

				isPaused = value;
/*
				HideParticles(!value);*/
			}
		}


		static PauseMenuUI Instance = null;

		public override void _EnterTree()
		{
			if (Instance != null) return;
			Instance = this;
/*
			particles = GetNode<CanvasItem>("GPUParticles2D");

			tween = GetNode<Tween>("Tween");*/

			IsPaused = false;

			GameManager.LanguageChanged += LoadLocalization;
		}

		public override void _Ready()
		{
			OptionsNode = GetNode<CanvasItem>("Options");

			LoadLocalization(GameManager.Preferences.language);

			//SerializationSystem.SaveDataGeneric<MenuLocalization>(new MenuLocalization(), @"ru\pauseMenu.json");
		}
		public override void _UnhandledInput(InputEvent @event)
		{
			if (@event.IsActionPressed(INPUT_PAUSE))
			{
				IsPaused = !IsPaused;
				if (!IsPaused) HideOptions();

				PauseStateChanged?.Invoke(IsPaused);
			}
		}

/*        public static void HideParticles(bool value)
		{
			if (value)
			{
				tween.InterpolateProperty(particles, "modulate",
					null,
					new Color(1, 1, 1, 0),
					AnimationDuration,
					Tween.TransitionType.Linear,
					Tween.EaseType.Out);
				tween.Start();
			}
			else
			{
				tween.InterpolateProperty(particles, "modulate",
					null,
					new Color(1, 1, 1, 1),
					AnimationDuration,
					Tween.TransitionType.Linear,
					Tween.EaseType.In);
				tween.Start();
			}
		}
*/
		static void LoadLocalization(string lang) => LocalizationChange?.Invoke(
			SerializationSystem.LoadLocalizationDataGeneric<MenuLocalization>
			($@"{lang}\{LOCALIZATIONFILE_NAME}"));
		public static void HideOptions() => OptionsNode.Visible = false;
		public static void Resume() => IsPaused = false;
	}
}
