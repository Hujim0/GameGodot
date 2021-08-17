using Godot;
using System;

namespace GodotGame
{
	public class SceneManager : Node
	{
		const string PathToSceneFolder = "res://data/Scenes";

		const string NodePathToCurrentScene = "Control/ViewportContainer/Viewport/CurrentScene";

		public static Node currentScene = null;

		public static Action SceneStartedLoading;

		public static bool isInLoad = false;

		public static PackedScene sceneToApply = null;

		static bool isPlayerReady = false;

		public override void _Ready()
		{
			currentScene = GetNode(NodePathToCurrentScene);

			ChangeScene("Puk");
		}

		public static void HardSceneChange(string sceneName)
		{
			string path = $@"{PathToSceneFolder}/{sceneName}.tscn";

			Node scene = ResourceLoader.Load<PackedScene>(path).Instance();

			CleanScenes();

			currentScene.AddChild(scene, true);

			SceneStartedLoading?.Invoke();
		}

		public static void ChangeScene(string sceneName)
		{
			GD.Print($"--- Loading scene: \"{sceneName}\" ---");

			isInLoad = true;

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
			currentScene.AddChild(sceneToApply.Instance(), true);
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
