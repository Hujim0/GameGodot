using Godot;
using GodotGame.Serialization.Localization;
using System;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public abstract class LocalizedNodeRename : Control, ILocalizedElement
    {
        public override void _EnterTree() => PauseMenuUI.LocalizationChange += ApplyLocalization;
        public abstract void ApplyLocalization(MenuLocalization localization);     
    }
}