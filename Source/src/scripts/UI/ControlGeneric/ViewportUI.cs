using Godot;
using System;

namespace GodotGame.UI
{
	public enum AnchorType { Horizontal, Vertical, HorizontalBottom, HorizontalTop, VerticalLeft, VerticalRight, Viewport, Center }

	public class ViewportUI : Control
	{

		public static Viewport viewport = null;

		public delegate void OnResize(Vector2 size);
		public static OnResize OnSizeChange;

		public readonly static Vector2 viewportSize = new Vector2(320, 180);

		public override void _EnterTree()
		{
			viewport = GetViewport();

			viewport.Connect("size_changed", this, nameof(SizeChange));
		}

		public void SizeChange()
		{
			OnSizeChange?.Invoke(viewport.Size);
		}

		public static void ChangeResolution(Vector2 res)
		{
			viewport.Size = res;
		}

		public static void SetFullScreen(bool value)
		{
			OS.WindowFullscreen = value;

			if (value)
			{
				OS.WindowSize = OS.GetScreenSize();
				return;
			}

			OS.WindowSize = new Vector2(1280, 720);
			OS.CenterWindow();
		}

		public static void SetVsync(bool value)
		{
			OS.VsyncEnabled = value;
		}
	}
}
