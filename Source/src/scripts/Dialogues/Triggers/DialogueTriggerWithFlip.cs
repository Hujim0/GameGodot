using Godot;
using GodotGame.PlayerBehaviour;

namespace GodotGame.Dialogues.Triggers
{
	public class DialogueTriggerWithFlip : DialogueOnInteractTriggerGeneric
	{
		[Export] public bool isFacingRightByDefault = true;

		protected override void OnDialogue(bool state)
		{
			if (state) { FlipH = Player.Instance.IsFacingRight; return;  }
			FlipH = !isFacingRightByDefault;
		}
	}
}
