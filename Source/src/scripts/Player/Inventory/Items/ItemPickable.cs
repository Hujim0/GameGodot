using Godot;
using GodotGame.General;
using GodotGame.PlayerBehavior.Interaction;
using GodotGame.Serialization;
using System;

namespace GodotGame.PlayerBehavior.InventorySystem
{
	public partial class ItemPickable : IIntractableHighlightable
	{
		const string PathToPrefab = "res://resrc/Prefabs/Item.scn";

		static PackedScene itemPrefab = GD.Load<PackedScene>(PathToPrefab);

		int id;
		public int Id
        {
			get => id;

			set
            {
				Texture = GD.Load<Texture2D>($"{InventoryUI.PathToItemsFolder}/{GameManager.GetItem(value).Sprite2D}");

				Offset = new Vector2(0, -Texture.GetSize().Y / 2);

				id = value;
			}
        }

		public override void OnInteracted()
		{
			InventorySystem.AddItem(id);
			GetParent().Free();
		}
		
		public static Node loadItem(int id)
		{
			Node node = itemPrefab.Instantiate();

			node.GetNode<ItemPickable>("Sprite2D").Id = id;

			return node;
		}

	}
}
