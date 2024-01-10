

using Godot;

namespace GodotGame.Serialization.Structs
{
    public partial class Sound
    {
/*        [Export] public AudioStreamWAV Clip;*/
        [Export] public float Volume;
        [Export] public float Pitch;

        public Sound (/*AudioStreamWAV Clip,*/ float Volume, float Pitch)
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
