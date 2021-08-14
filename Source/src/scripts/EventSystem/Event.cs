using Godot;
using GodotGame.Serialization;
using System;

namespace GodotGame.EventSystem
{

    [Serializable]
    public class Event : IEvent
    {

        public EVENT_TYPE type;

        public string data_path;

        public int arg = 0;

        public Action OnEventStarted;


        public Event(EVENT_TYPE type, string data_path, int arg = 0)
        {
            this.type = type;
            this.data_path = data_path;
            this.arg = arg;

            switch (type)
            {
                case EVENT_TYPE.StartDialogue:

                    DialogueLoader loader = new DialogueLoader(data_path, arg);

                    OnEventStarted += loader.StartDialogue;

                    break;
            }
        }

        public void Invoke()
        {
            OnEventStarted.Invoke();
        }
    }
}