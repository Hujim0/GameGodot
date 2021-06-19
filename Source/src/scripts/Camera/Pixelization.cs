using Godot;
using GodotGame.UI;
using System;

public class Pixelization : ColorRect
{
	CanvasItem canvasitem = null;

	public override void _Ready()
	{
		ViewportUI.OnSizeChange += Update;
	}

	public void Update(Vector2 viewport)
	{
		Material.Set("shader_param/size_x", 3.2f / viewport.x);
		Material.Set("shader_param/size_y", 1.8f / viewport.y);

		GD.Print(viewport);
	}
}
