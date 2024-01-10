using Godot;
using GodotGame.UI;
using System;

public partial class Pixelization : ColorRect
{
	const float MagicalNumber = 0.0055555555555556f; // 1 : 180
	/*const float MagicalNumber = 0.1f;*/

	public override void _Ready()
	{
		Visible = true;
		ViewportUI.OnSizeChange += Update;
		Update(GetViewportRect().Size);
	}

	public void Update(Vector2 viewport)
	{
		Material.Set("shader_param/size", new Vector2((viewport.Y / viewport.X) * MagicalNumber, MagicalNumber));

		GD.Print(new Vector2((viewport.Y / viewport.X) * MagicalNumber, MagicalNumber));

	}
}
