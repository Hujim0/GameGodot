using Godot;
using System;

namespace GodotGame.UI
{
	public partial class AnchorUI : Control
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
					Position = new Vector2(offset.X, Position.Y);
					Size = new Vector2(size.X - offset.X, Size.Y);
					return;
				case AnchorType.Vertical:
					Size = new Vector2(Size.X, size.Y);
					return;
				case AnchorType.HorizontalBottom:
					Size = new Vector2(size.X, Size.Y);
					GD.Print(OffsetBottom);
					GD.Print(Position);
					return;
				case AnchorType.HorizontalTop:
					Size = new Vector2(Size.X, size.Y);
					GD.Print("Bottom: " + OffsetBottom);
					GD.Print("Top: " + OffsetTop);
					GD.Print(Position);
					return;
				case AnchorType.SubViewport:
					Size = new Vector2(size.X, size.Y);
					return;
				case AnchorType.Center:
					Position = new Vector2
					{
						X = size.X - ViewportUI.viewportSize.X,
						Y = size.Y - ViewportUI.viewportSize.Y,
					};
					return;
				default:
					return;
			}
		}
	}
}
