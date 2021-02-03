using Godot;
using GodotGame.Serialization.Localization;
using System;

namespace GodotGame.UI
{
    public interface ILocalizedElement
    {
        void ApplyLocalization(MenuLocalization localization);
    }
}
