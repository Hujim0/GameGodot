using Godot;
using GodotGame.Serialization.Localization;
using System;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public partial class VSyncToggle : LocalizedButton
    {
        public override void ApplyLocalization(MenuLocalization localization)
        {
            Text = localization.pause_vsync;
        }

        public override void _Toggled(bool buttonPressed)
        {
            ViewportUI.SetVsync(buttonPressed);
        }
    }
}