using Godot;
using System;

namespace GodotGame.UI
{
	public class AnchorUI : Control
	{
		[Export(PropertyHint.Enum)] public AnchorType anchorType;

		[Export] public Vector2 offset;

		public override void _Ready()
		{
			ViewportUI.OnSizeChange += Resize;
		}

		void Resize(Vector2 size)
		{
			switch (anchorType)
			{
				case AnchorType.Horizontal:
					RectPosition = new Vector2(offset.x, RectPosition.y);
					RectSize = new Vector2(size.x - offset.x, RectSize.y);
					return;
				case AnchorType.Vertical:
					RectSize = new Vector2(RectSize.x, size.y);
					return;
				case AnchorType.HorizontalBottom:
					RectSize = new Vector2(size.x, RectSize.y);
					GD.Print(MarginBottom);
					GD.Print(RectPosition);
					return;
				case AnchorType.HorizontalTop:
					RectSize = new Vector2(RectSize.x, size.y);
					GD.Print("Bottom: " + MarginBottom);
					GD.Print("Top: " + MarginTop);
					GD.Print(RectPosition);
					return;
				case AnchorType.Viewport:
					RectSize = new Vector2(size.x, size.y);
					return;
				case AnchorType.Center:
					RectPosition = new Vector2
					{
						x = size.x - ViewportUI.viewportSize.x,
						y = size.y - ViewportUI.viewportSize.y,
					};
					return;
				default:
					return;
			}
		}
	}
}
