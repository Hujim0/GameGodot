using Godot;
using GodotGame.General;
using GodotGame.PlayerBehaviour.Interaction;
using GodotGame.Serialization;
using System.Collections.Generic;

namespace GodotGame.Dialogues
{
	public abstract class IDialogueOnInteractTrigger : IInteractableHighLightable
	{
		protected const string MainDialogueDirName = "Main";
		protected const string SecondaryDialogueDirName = "Secondary";

		protected int lastDialogueIdx = 0;

		protected Dialogue[] mainDialogues;
		protected Dialogue[] secondaryDialogues;

		protected RandomNumberGenerator rng = new RandomNumberGenerator();

        protected string pathGeneric;

        public override void _Ready()
		{
            DialogueSystem.OnToggled += OnDialogue;

            pathGeneric = $@"{GameManager.Preferences.language}\{SerializationSystem.PathToDialogues}{Owner.Name}\{GetParent().Name}";

            GetDialogues();
        }
        public override void _ExitTree() => DialogueSystem.OnToggled -= OnDialogue;


        public override abstract void OnInteracted();
		protected abstract Dialogue GetDialogue(IEnumerable<Dialogue> array, int idxOffset, bool isSecondary);
        protected virtual void OnDialogue(bool state)
        {
            if (secondaryDialogues == null && mainDialogues.Length == lastDialogueIdx) 
                IsInteractable = false;
        }
        
        protected virtual void GetDialogues()
        { 
            mainDialogues = SerializationSystem.LoadDialogues($@"{pathGeneric}\{MainDialogueDirName}");
            secondaryDialogues = SerializationSystem.LoadDialogues($@"{pathGeneric}\{SecondaryDialogueDirName}");

            GD.Print("kek");

            foreach (Dialogue dil in mainDialogues)
            {
                GD.Print(dil.panels[0].name);
                GD.Print(dil.panels[0].txt);
            }
        }
    }
}
