using Godot;
using GodotGame.Serialization.Localization;
using System;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public class ControlsNodeRename : LocalizedNodeRename
    {
        public override void ApplyLocalization(MenuLocalization localization)
        {
            Name = localization.pause_controls;
        }
    }
}