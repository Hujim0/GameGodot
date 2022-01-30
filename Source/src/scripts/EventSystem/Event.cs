using Godot;
using GodotGame.General;
using GodotGame.PlayerBehaviour.InventorySystem;
using GodotGame.Serialization;
using System;
using System.Collections.Generic;

namespace GodotGame.EventSystem
{
	public enum EVENT_TYPE { StartDialogue, InsertDialogue, SceneTransition, EditGameEvents, GiveItem, ScreenFade };

	public class Event : EventData
	{
		public Action OnEventStarted;

		public Event(EVENT_TYPE type, string data_path, Vector2 arg = default, string specialarg = "")
		{
			GD.Print("  --- Event construction ---");

			this.type = type;
			this.data_path = data_path;
			this.arg = arg;
			this.specialarg = specialarg;
			
			GD.Print($"Type: {type}");

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

				case EVENT_TYPE.EditGameEvents:

					if (string.IsNullOrEmpty(data_path))
					{ GD.PrintErr("!!!Event construction failed: data_path is null/empty!!!"); return; }


					string[] events = data_path.Split(new char[] { ',', ' ' });

					if ((arg == null || arg.x == 0))
                    {
                        foreach (string @event in events)
							GameManager.GameEvents.Remove(@event);

						GD.Print("Deleted game events: ");
						GD.Print(events);
					}
					else
                    {
						GameManager.GameEvents.AddRange(events);

						GD.Print("Added game events: ");
						GD.Print(events);
					}

					break;

				case EVENT_TYPE.GiveItem:

					OnEventStarted += GiveItem;

					break;

				case EVENT_TYPE.ScreenFade:

					OnEventStarted += SceneManager.ScreenFade;

					break;


				default:
					GD.PrintErr("!!!Event construction failed: type is undefined!!!"); 
					return;

			}

			GD.Print("  --- Event construction ended ---");
		}

		public Event(EventData eventData)
		{
			GD.Print("  --- Event construction ---");

			type = eventData.type;
			data_path = eventData.data_path;
			arg = eventData.arg;
			specialarg = eventData.specialarg;
			
			GD.Print($"Type: {type}");

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


				case EVENT_TYPE.EditGameEvents:

					if (string.IsNullOrEmpty(data_path))
					{ GD.PrintErr("!!!Event construction failed: data_path is null/empty!!!"); return; }


					string[] events = data_path.Split(new char[] { ',', ' ' });

					if ((arg == null || arg.x == 0))
					{
						foreach (string @event in events)
							GameManager.GameEvents.Remove(@event);

						GD.Print("Deleted game events: ");
						GD.Print(events);
					}
					else
					{
						GameManager.GameEvents.AddRange(events);

						GD.Print("Added game events: ");
						GD.Print(events);
					}

					break;

				case EVENT_TYPE.ScreenFade:

					OnEventStarted += SceneManager.ScreenFade;

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
			OnEventStarted?.Invoke();
		}
	}
}
