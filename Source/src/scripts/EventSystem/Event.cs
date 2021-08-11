using Godot;
using System;

namespace GodotGame.EventSystem
{

    [Serializable]
    public class Event : IEvent
    {
        public EVENT_TYPE type;

        public string data_path;


        public Event(EVENT_TYPE type, string data_path)
        {
            this.type = type;
            this.data_path = data_path;

            switch (type)
            {
                case EVENT_TYPE.StartDialogue:

                    break;

            }
        }

        public void Invoke()
        {
            switch (type)
            {
                case EVENT_TYPE.StartDialogue:

                    break;
               
            }   
        }
    }
}