using Godot;
using System;

namespace GodotGame.UI.PauseMenu.Buttons
{
    public abstract partial class HideButton : LocalizedButton
    {
        abstract protected string pathToNode { get; }
        abstract protected bool setVisible { get; }

        protected CanvasItem nodeToHide = null;

        public override void _Ready() => nodeToHide = GetNode<CanvasItem>(pathToNode);
        public override void _Pressed() => nodeToHide.Visible = setVisible;
    }
}