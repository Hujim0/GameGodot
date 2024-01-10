using Godot;
using GodotGame.Serialization.Localization;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public abstract partial class LocalizedLabel : Label, ILocalizedElement
    {
        public abstract void ApplyLocalization(MenuLocalization localization);
        public override void _EnterTree() => PauseMenuUI.LocalizationChange += ApplyLocalization;
    }
}