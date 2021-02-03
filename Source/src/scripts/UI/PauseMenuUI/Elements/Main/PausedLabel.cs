using GodotGame.Serialization.Localization;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public class PausedLabel : LocalizedLabel
    {
        public override void ApplyLocalization(MenuLocalization localization)
        {
            Text = localization.pause_paused;
        }
    }
}
