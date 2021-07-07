using Godot;
using GodotGame.UI;
using System;

public class Pixelization : ColorRect
{
	const float MagicalNumber = 0.0055555555555556f; // 1 : 180
	/*const float MagicalNumber = 0.1f;*/

	public override void _Ready()
	{
		Visible = true;
		ViewportUI.OnSizeChange += Update;
		Update(ViewportUI.viewport.Size);
	}

	public void Update(Vector2 viewport)
	{

	/*	Material.Set("shader_param/size", new Vector2(0.0032f, 0.0032f) * new Vector2(1f, viewport.x/viewport.y));
*/
		Material.Set("shader_param/size", new Vector2((viewport.y / viewport.x) * MagicalNumber, MagicalNumber));

		GD.Print(new Vector2((viewport.y / viewport.x) * MagicalNumber, MagicalNumber));

	}
}
