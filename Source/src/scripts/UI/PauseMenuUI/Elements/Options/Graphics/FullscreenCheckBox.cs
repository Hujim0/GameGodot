using Godot;
using GodotGame.Serialization.Localization;
using System;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public partial class FullscreenCheckBox : LocalizedButton
    {
        public override void ApplyLocalization(MenuLocalization localization)
        {
            Text = localization.pause_fullscreen;
        }

        public override void _Toggled(bool buttonPressed)
        {
            ViewportUI.SetFullScreen(buttonPressed);
        }
    }
}