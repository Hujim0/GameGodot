using Godot;
using GodotGame;
using GodotGame.Inputs;
using System;

namespace GodotGame.UI
{
	public class SceneManagerUI : ColorRect
	{
		public static bool isLoadingOrFading = false;

		public static Action LoadingStarted;

		public static SceneManagerUI Instance = null;

		const float AnimDuration = 0.5f;

		static string sceneNameToLoad = string.Empty;

		static Tween tween = null;

		public override void _Ready()
		{
			if (Instance == null) Instance = this;

			tween = GetNode<Tween>("Tween");

			SceneManager.SceneLoaded += FadeOut;

			Color = new Color(0, 0, 0, 0);
		}

		public static void LoadSceneWithLoadScreen(string sceneName)
		{
			sceneNameToLoad = sceneName;
			isLoadingOrFading = true;

			Instance.FadeIn();
		}

		public void FadeOut()
		{
			tween.InterpolateProperty(this, "color",
				new Color(0, 0, 0, 1),
				new Color(0, 0, 0, 0),
				AnimDuration,
				Tween.TransitionType.Linear,
				Tween.EaseType.Out);
			tween.Start();
		}
		public void FadeIn()
		{
			tween.InterpolateProperty(this, "color",
				null,
				new Color(0, 0, 0, 1),
				AnimDuration,
				Tween.TransitionType.Linear,
				Tween.EaseType.In);
			tween.Start();
		}

		private void _on_Tween_tween_completed(Godot.Object _, NodePath __)
		{
			if (!isLoadingOrFading) return;

			isLoadingOrFading = false;

			LoadingStarted?.Invoke();

			SceneManager.HardSceneChange(sceneNameToLoad);
		}
	}

}
