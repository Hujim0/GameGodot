using Godot;
using GodotGame.PlayerBehavior.InventorySystem;
using System;

[Serializable]
public struct ItemsStruct
{
    public Item[] items;

    /// <param name="items"></param>
    public ItemsStruct(Item[] items)
    {
        this.items = items;
    }
}
