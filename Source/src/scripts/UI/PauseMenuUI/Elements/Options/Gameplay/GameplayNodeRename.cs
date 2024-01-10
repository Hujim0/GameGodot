using Godot;
using GodotGame.Serialization.Localization;
using System;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public partial class GameplayNodeRename : LocalizedNodeRename
    {
        public override void ApplyLocalization(MenuLocalization localization)
        {
            Name = localization.pause_gameplay;
        }
    }
}
