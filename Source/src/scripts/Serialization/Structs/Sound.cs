

using Godot;

namespace GodotGame.Serialization.Structs
{
    public class Sound
    {
/*        [Export] public AudioStreamSample Clip;*/
        [Export] public float Volume;
        [Export] public float Pitch;

        public Sound (/*AudioStreamSample Clip,*/ float Volume, float Pitch)
        {
/*            this.Clip = Clip;*/
            this.Volume = Volume;
            this.Pitch = Pitch;
        }

/*        const Sound Default = new Sound
        {
            Clip = null,
            Pitch = 1f,
            Volume = 1f
        };*/
    }
}
