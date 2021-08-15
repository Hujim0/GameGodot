using Godot;
using GodotGame.PlayerBehaviour.InventorySystem;
using GodotGame.Serialization;
using System;

namespace GodotGame.EventSystem
{
    public enum EVENT_TYPE { StartDialogue, InsertDialogue, SceneTransition, GiveItem, SelfDestroy };

    [Serializable]
    public class Event : IEvent
    {

        [Export] public EVENT_TYPE type;

        [Export] public string data_path = string.Empty;

        [Export] public int arg = 0;

        [Export] public string specialarg = string.Empty;

        public Action OnEventStarted;

        public Event(EVENT_TYPE type, string data_path, int arg = 0, string specialarg = "")
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

                    DialogueLoader loader = new DialogueLoader(data_path, arg, specialarg);

                    OnEventStarted += loader.StartDialogue;

                    break;

                case EVENT_TYPE.InsertDialogue:
                    //to do
                    break;

                case EVENT_TYPE.SceneTransition:

                    if (string.IsNullOrEmpty(data_path))
                    { GD.PrintErr("!!!Event construction failed: data_path is null/empty!!!"); return; }


                    SceneManager.HardSceneChange(data_path);

                    break;

                case EVENT_TYPE.GiveItem:
                    
                    InventorySystem.AddItem(arg);

                    break;


                default:
                    GD.PrintErr("!!!Event construction failed: type is undefined!!!"); 
                    return;

            }

            GD.Print("  --- Event construction ended ---");
        }

        public void Invoke()
        {
            OnEventStarted.Invoke();
        }
    }
}