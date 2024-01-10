using GodotGame.Serialization.Localization;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public partial class QuitButton : LocalizedButton
    {
        public override void _Pressed() => SceneManager.QuitApplication();

        public override void ApplyLocalization(MenuLocalization localization)
        {
            Text = localization.pause_quit;
        }
    }
}
