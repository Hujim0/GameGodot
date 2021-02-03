using Godot;
using GodotGame.PlayerBehaviour;
using GodotGame.PlayerBehaviour.Interaction;
using GodotGame.UI;

namespace GodotGame.Interation
{
	public class ChangeSceneOnInteraction : IInteractableHighLightable
	{
		[Export] public string SceneName = "Test";

		[Export] public Vector2 PlayerPos;

        public override void OnInteracted()
		{
			if (SceneManagerUI.isLoadingOrFading) return;
			if (string.IsNullOrEmpty(SceneName)) { GD.PrintErr($"!!! Scene Name is Empty or Null at { GetPath() }. !!!"); return; }

			SceneManagerUI.LoadSceneWithLoadScreen(SceneName);
			SceneManagerUI.LoadingStarted += OnLoadStarted;
			Player.Instance.SetPause(true);
		}

		void OnLoadStarted()
		{
			Player.Instance.SetPause(false);
			Player.Instance.Position = PlayerPos;
			SceneManagerUI.LoadingStarted -= OnLoadStarted;
		}
	}
}