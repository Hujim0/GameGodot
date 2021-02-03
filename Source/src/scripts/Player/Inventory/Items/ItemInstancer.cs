using Godot;
using GodotGame.General;
using GodotGame.PlayerBehaviour.InventorySystem;
using System;

public class ItemInstancer : Node2D
{
	[Export] public int itemID;

	public override void _Ready()
	{
        Node node = ItemPickable.loadItem(itemID);

        AddChild(node);
    }
}
