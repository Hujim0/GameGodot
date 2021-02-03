using Godot;
using GodotGame.General;
using GodotGame.PlayerBehaviour.Interaction;
using GodotGame.Serialization;
using System;

namespace GodotGame.PlayerBehaviour.InventorySystem
{
	public class ItemPickable : IInteractableHighLightable
	{
		const string PathToPrefab = "res://resrc/Prefabs/Item.scn";

		static PackedScene itemPrefab = GD.Load<PackedScene>(PathToPrefab);

		int id;
		public int Id
        {
			get => id;

			set
            {
				Texture = GD.Load<Texture>($"{InventoryUI.PathToItemsFolder}/{GameManager.GetItem(value).Sprite}");

				Offset = new Vector2(0, -Texture.GetSize().y / 2);

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
			Node node = itemPrefab.Instance();

			node.GetNode<ItemPickable>("Sprite").Id = id;

			return node;
		}

	}
}
