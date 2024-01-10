using Godot;
using GodotGame.Serialization.Localization;
using System;

namespace GodotGame.UI.PauseMenu.Buttons
{
	public abstract partial class LocalizedButton : Button, ILocalizedElement
	{
		public abstract void ApplyLocalization(MenuLocalization localization);
		public override void _EnterTree() => PauseMenuUI.LocalizationChange += ApplyLocalization;

        public override void _Ready() => Connect("mouse_entered", new Callable(this, nameof(GrabFocus)));
	}
}
