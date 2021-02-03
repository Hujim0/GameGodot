using GodotGame.Serialization.Localization;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public class ResumeButton : LocalizedButton
    {
        public override void _Ready() => PauseMenuUI.PauseStateChanged += ctx => GrabFocus();
        public override void _Pressed() => PauseMenuUI.Resume();

        public override void ApplyLocalization(MenuLocalization localization)
        {
            Text = localization.pause_resume;
        }
    }
}