using Godot;
using GodotGame.General;
using GodotGame.PlayerBehavior.InventorySystem;
using System;

public partial class ItemInstancer : Node2D
{
	[Export] public int itemID;

	public override void _Ready()
	{
        Node node = ItemPickable.loadItem(itemID);

        AddChild(node);
    }
}
