using Godot;
using System;

namespace GodotGame
{
	public partial class SceneManager : Node
	{
		const string PathToSceneFolder = "res://src/Scenes";

		const string NodePathToCurrentScene = "Control/SubViewportContainer/SubViewport/CurrentScene";

		public static Node currentScene = null;

		public static System.Action OnSceneStartedLoading;

		public delegate void SceneInstanced(Vector2 pos);
		public static event SceneInstanced OnSceneInstance;

		public static System.Action FadeScreen;

		public static bool isInLoad = false;
		public static bool inTransition = false;

		public static PackedScene sceneToApply = null;

		public static Vector2 playerPosToApply = Vector2.Zero;

/*		static bool isPlayerReady = false;
*/
		public override void _Ready()
		{
			currentScene = GetNode(NodePathToCurrentScene);

			LoadSceneFromFileAsync("Puk", Vector2.Zero);
		}

		public static void HardSceneChange(string sceneName)
		{
			string path = $@"{PathToSceneFolder}/{sceneName}.tscn";

			Node scene = ResourceLoader.Load<PackedScene>(path).Instantiate();

			CleanScenes();

			currentScene.AddChild(scene, true);

			OnSceneStartedLoading?.Invoke();
		}

		public static void LoadSceneFromFileAsync(string sceneName, Vector2 playerPos)
		{
			if (inTransition) return;

			GD.Print($"--- Loading scene: \"{sceneName}\" -------------");

			isInLoad = true;
			inTransition = true;

			playerPosToApply = playerPos;

			OnSceneStartedLoading?.Invoke();

			string path = $@"{PathToSceneFolder}/{sceneName}.tscn";

			Error loader_error = ResourceLoader.LoadThreadedRequest(path, "PackedScene");

			if (loader_error != Error.Ok) {
				GD.Print(loader_error.ToString());
			}

			Godot.Collections.Array progress = new Godot.Collections.Array();

			while (true)
			{

				ResourceLoader.ThreadLoadStatus status = ResourceLoader.LoadThreadedGetStatus(path, progress);

				switch (status)
				{
					case ResourceLoader.ThreadLoadStatus.InProgress:
						
						GD.Print($"- {(float)progress[0] * 100f}%");
						
						break;

					case ResourceLoader.ThreadLoadStatus.Loaded:

						Resource res = ResourceLoader.LoadThreadedGet(path);

						GD.Print("- 100%");

						if (res is PackedScene scene)
							/*currentScene.AddChild(scene.Instance(), true);*/
							sceneToApply = scene;

						isInLoad = false;

						return;

					default:

						GD.PrintErr($"!!! Scene load failed! {status} !!!");

						return;
				}
			}
		}

		public static void ApplyChanges()
		{
			if (sceneToApply == null) return;

			OnSceneInstance?.Invoke(playerPosToApply);

			CleanScenes();
			currentScene.AddChild(sceneToApply.Instantiate(), true);

			GD.Print("--- Scene instantiated -------------");
		}

		public static void ScreenFade()
		{
			if (inTransition) return;

			GD.Print($"--- FadeScreen In -------------");

			inTransition = true;

			FadeScreen?.Invoke();
		}

		public static void InvokeOnSceneInstance()
		{
			OnSceneInstance?.Invoke(Vector2.Zero);
			GD.Print($"--- FadeScreen Out -------------");
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
