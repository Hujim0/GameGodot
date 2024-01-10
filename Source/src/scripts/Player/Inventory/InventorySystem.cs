using Godot;
using GodotGame.General;
using System;

namespace GodotGame.PlayerBehavior.InventorySystem
{
    public partial class InventorySystem
    {
        public static Inventory inventory = new Inventory(Inventory.Size);

        public static void AddItem(int id)
        {
            for (int i = 0; i < Inventory.Size; i++)
            {
                if (inventory.Items[i] == null) continue;

                inventory.Items[i] = GameManager.GetItem(id);
                break;
            }
        }

        public static bool ContainsItem(int id)
        {
            Item item = GameManager.GetItem(id);

            foreach (Item it in inventory.Items)
            {
                if (it == null || it != item) continue;

                return true;
            }

            return false;
        }
    }
}