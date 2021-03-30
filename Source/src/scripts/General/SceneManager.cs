using Godot;
using System;

namespace GodotGame
{
	public class SceneManager : Node
	{
		const string PathToSceneFolder = "res://data/Scenes";

		const string NodePathToCurrentScene = "CurrentScene";

		public static Node currentScene = null;

		public static Action SceneLoaded;

		static bool isPlayerReady = false;

		public override void _Ready()
		{
			currentScene = GetNode(NodePathToCurrentScene);

			HardSceneChange("Puk");
		}

		public static void HardSceneChange(string sceneName)
		{
			string path = $@"{PathToSceneFolder}/{sceneName}.tscn";

			Node scene = ResourceLoader.Load<PackedScene>(path).Instance();

			CleanScenes();

			currentScene.AddChild(scene, true);

			SceneLoaded?.Invoke();
		}

/*		public static void ChangeScene(string sceneName)
		{
			string path = $@"{PathToSceneFolder}/{sceneName}.tscn";

			ResourceInteractiveLoader loader = ResourceLoader.LoadInteractive(path, "PackedScene");

			Resource res = new Resource();
			res.

			CleanScenes();

			currentScene.AddChild(scene, true);

			SceneLoaded?.Invoke();
		}*/

		public static void CleanScenes()
		{
			foreach (Node child in currentScene.GetChildren())
			{
				if (child.Name == "Player") continue;

				child.QueueFree();
			}
		}

		public static void QuitApplication()
		{
			currentScene.GetTree().Quit();
		}

		public static void InstantiatePlayer()
		{

		}
	}
}
