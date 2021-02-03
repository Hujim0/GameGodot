using Godot;
using GodotGame.Serialization.Localization;
using System;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public class ResolutionLabel : LocalizedLabel
    {
        public override void ApplyLocalization(MenuLocalization localization)
        {
            Text = localization.pause_resolution;
        }
    }
}