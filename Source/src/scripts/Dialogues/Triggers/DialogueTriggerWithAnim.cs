using Godot;
using GodotGame.PlayerBehaviour;

namespace GodotGame.Dialogues.Triggers
{
	public class DialogueTriggerWithAnim : DialogueOnInteractTriggerGeneric
	{
		[Export] public string IdleAnimName;

		[Export] public string OnInteractAnimName;

		AnimationPlayer anim = null;

        public override void _Ready() => anim = GetNode<AnimationPlayer>("AnimationPlayer");

        protected override void OnDialogue(bool state)
		{
			if (state) { anim.Play(OnInteractAnimName); }
			else { anim.Play(IdleAnimName); }

			if (secondaryDialogues != null || mainDialogues.Length != lastDialogueIdx) return;

			IsInteractable = false;
		}
    }
}
