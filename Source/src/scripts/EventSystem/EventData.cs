using Godot;
using System;

namespace GodotGame.EventSystem
{
	[Serializable]
	public partial class EventData
	{
		[Export] public EVENT_TYPE type;

		[Export] public string data_path = string.Empty;

		[Export] public Vector2 arg = Vector2.Zero;

		[Export] public string specialarg = string.Empty;
	}
}