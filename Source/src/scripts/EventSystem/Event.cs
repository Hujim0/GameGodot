using Godot;
using GodotGame.PlayerBehaviour.InventorySystem;
using GodotGame.Serialization;
using System;

namespace GodotGame.EventSystem
{
	public enum EVENT_TYPE { StartDialogue, InsertDialogue, SceneTransition, GiveItem, SelfDestroy };

	public class Event : EventData
	{
		public Action OnEventStarted;

		public Event(EVENT_TYPE type, string data_path, Vector2 arg = default, string specialarg = "")
		{
			GD.Print("  --- Event construction ---");
			GD.Print($"Type: {type}");

			this.type = type;
			this.data_path = data_path;
			this.arg = arg;
			this.specialarg = specialarg;

			switch (type)
			{
				case EVENT_TYPE.StartDialogue:

					if (string.IsNullOrEmpty(data_path))
					{ GD.PrintErr("!!!Event construction failed: data_path is null/empty!!!"); return; }

					DialogueLoader loader = new DialogueLoader(data_path, Mathf.FloorToInt(arg.x), specialarg);

					OnEventStarted += loader.StartDialogue;

					break;

				case EVENT_TYPE.InsertDialogue:

					if (string.IsNullOrEmpty(data_path))
					{ GD.PrintErr("!!!Event construction failed: data_path is null/empty!!!"); return; }

					DialogueLoader insertloader = new DialogueLoader(data_path, Mathf.FloorToInt(arg.x), specialarg);

					OnEventStarted += insertloader.InsertDialogues;

					break;

				case EVENT_TYPE.SceneTransition:

					if (string.IsNullOrEmpty(data_path))
					{ GD.PrintErr("!!!Event construction failed: data_path is null/empty!!!"); return; }

					OnEventStarted += ChangeScene;

					GD.Print($"Scene name: \"{data_path}\"");
					GD.Print($"Spawn at: {arg}");


					break;

				case EVENT_TYPE.GiveItem:

					OnEventStarted += GiveItem;

					break;


				default:
					GD.PrintErr("!!!Event construction failed: type is undefined!!!"); 
					return;

			}

			GD.Print("  --- Event construction ended ---");
		}

		void ChangeScene() => SceneManager.ChangeScene(data_path, arg);
		void GiveItem() => InventorySystem.AddItem(Mathf.FloorToInt(arg.x));

		public void Invoke()
		{
			OnEventStarted.Invoke();
		}
	}
}
