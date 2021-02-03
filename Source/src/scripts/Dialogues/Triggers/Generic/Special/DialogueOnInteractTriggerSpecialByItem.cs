using Godot;
using GodotGame.PlayerBehaviour.InventorySystem;
using System;

namespace GodotGame.Dialogues.Triggers
{

    public class DialogueOnInteractTriggerSpecialByItem : IDialogueOnInteractTriggerSpecial
    {
        [Export] public int itemId;

        protected override bool IsSpecial => InventorySystem.ContainsItem(itemId);
    }
}