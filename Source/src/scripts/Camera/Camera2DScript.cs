using Godot;
using GodotGame;
using System;

public class Camera2DScript : Camera2D
{
	public override void _Ready()
	{
		SceneManager.SceneLoaded += ResetCamera;
	}

	private void ResetCamera()
	{

		SmoothingEnabled = false;
	}
}
