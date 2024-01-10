using Godot;
using System;

namespace GodotGame.UI
{
	public enum AnchorType { Horizontal, Vertical, HorizontalBottom, HorizontalTop, VerticalLeft, VerticalRight, SubViewport, Center }

	public partial class ViewportUI : Control
	{

		public static Viewport viewport = null;

		public delegate void OnResize(Vector2 size);
		public static OnResize OnSizeChange;

		public readonly static Vector2 viewportSize = new Vector2(320, 180);



		public override void _EnterTree()
		{
			// viewport = GetViewport();

			// viewport.Connect("size_changed", new Callable(this, nameof(SizeChange)));

			// Size = viewport.GetVisibleRect().Size;

			
		}

		public void SizeChange()
		{
			// OnSizeChange?.Invoke(Size);
			// Size = viewport.Size;
		}

		public static void ChangeResolution(Vector2 res)
		{
			// viewport.Size = new Vector2I((int)res.X, (int)res.Y);
		}

		public static void SetFullScreen(bool value)
		{
			// OS. = value;

			// if (value)
			// {
			// 	OS.WindowSize = OS.GetScreenSize();
			// 	return;
			// }

			// OS.WindowSize = new Vector2(1280, 720);
			// OS.CenterWindow();
		}

		public static void SetVsync(bool value)
		{
			// OS.VsyncEnabled = value;
		}
	}
}
