using GodotGame.Serialization.Localization;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public partial class BackButton : HideButton
    {
        protected override string pathToNode => "../../";
        protected override bool setVisible => false;

        public override void ApplyLocalization(MenuLocalization localization) => Text = localization.pause_back;
    }
}