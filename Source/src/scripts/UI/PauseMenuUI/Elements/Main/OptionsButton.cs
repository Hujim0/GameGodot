using GodotGame.Serialization.Localization;


namespace GodotGame.UI.PauseMenu.Buttons
{
    public class OptionsButton : HideButton
    {
        protected override string pathToNode => "../../../../../../Options";
        protected override bool setVisible => true;

        public override void ApplyLocalization(MenuLocalization localization) => Text = localization.pause_options;
    }
}
