using Godot;
using System;

namespace GodotGame.PlayerBehaviour.InventorySystem
{
    [Serializable]
    public struct Inventory
    {
        public const int Size = 10;

        public Item[] Items;

        public Inventory(Item[] items)
        {
            Items = items;
        }

        public Inventory(int size = Size)
        {
            Items = new Item[size];
        }
    }
}
