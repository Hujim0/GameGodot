using Godot;
using System;

namespace GodotGame
{
	public class SceneManager : Node
	{
		const string PathToSceneFolder = "res://src/Scenes";

		const string NodePathToCurrentScene = "Control/ViewportContainer/Viewport/CurrentScene";

		public static Node currentScene = null;

		public static Action SceneStartedLoading;

		public  delegate void OnPlayerPosApply(Vector2 pos);
		public static event OnPlayerPosApply PlayerPosApply;

		public static bool isInLoad = false;
		public static bool inTransition = false;

		public static PackedScene sceneToApply = null;

		public static Vector2 playerPosToApply = Vector2.Zero;

/*		static bool isPlayerReady = false;
*/
		public override void _Ready()
		{
			currentScene = GetNode(NodePathToCurrentScene);

			ChangeScene("Puk", Vector2.Zero);
		}

		public static void HardSceneChange(string sceneName)
		{
			string path = $@"{PathToSceneFolder}/{sceneName}.tscn";

			Node scene = ResourceLoader.Load<PackedScene>(path).Instance();

			CleanScenes();

			currentScene.AddChild(scene, true);

			SceneStartedLoading?.Invoke();
		}

		public static void ChangeScene(string sceneName, Vector2 playerPos)
		{
			if (inTransition) return;

			GD.Print($"--- Loading scene: \"{sceneName}\" -------------");

			isInLoad = true;
			inTransition = true;

			playerPosToApply = playerPos;

			SceneStartedLoading?.Invoke();

			string path = $@"{PathToSceneFolder}/{sceneName}.tscn";

			ResourceInteractiveLoader loader = ResourceLoader.LoadInteractive(path, "PackedScene");

			while (true)
			{
				Error err = loader.Poll();

				switch (err)
				{
					case Error.Ok:
						
						GD.Print($"- {Mathf.Floor((float)loader.GetStage() / (float)loader.GetStageCount() * 100f)}%");
						
						break;

					case Error.FileEof:

						Resource res = loader.GetResource();

						loader.Dispose();

						GD.Print("- 100%");

						if (res is PackedScene scene)
							/*currentScene.AddChild(scene.Instance(), true);*/
							sceneToApply = scene;

						isInLoad = false;

						return;

					default:

						GD.PrintErr($"!!! Scene load failed! {err} !!!");

						return;
				}
			}
		}

		public static void ApplyChanges()
		{
			if (sceneToApply == null) return;

			PlayerPosApply?.Invoke(playerPosToApply);

			CleanScenes();
			currentScene.AddChild(sceneToApply.Instance(), true);

			GD.Print("--- Scene instantiated -------------");
		}

		public static void CleanScenes()
		{
			foreach (Node child in currentScene.GetChildren())
			{
				if (child.Name == "Player" || child.Name == "Camera2D") continue;

				child.QueueFree();
			}
		}

		public static void QuitApplication()
		{
			currentScene.GetTree().Quit();
		}
	}
}
