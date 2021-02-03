using Godot;
using GodotGame.General;
using GodotGame.PlayerBehaviour.Interaction;
using GodotGame.Serialization;
using System.Collections.Generic;

namespace GodotGame.Dialogues.Triggers
{
    public abstract class IDialogueTrigger : Node
    {
        const string MainDialogueDirName = "Main";
        const string SecondaryDialogueDirName = "Secondary";

        protected int lastDialogueIdx = 0;

        protected Dialogue[] mainDialogues;
        protected Dialogue[] secondaryDialogues;

        protected RandomNumberGenerator rng = new RandomNumberGenerator();

        public override void _Ready()
        {
            DialogueSystem.OnToggled += OnDialogue;

            GetDialogues();
        }
        public override void _ExitTree() => DialogueSystem.OnToggled -= OnDialogue;

        public abstract void StartDialogue();
        protected abstract void OnDialogue(bool state);
        protected abstract Dialogue GetDialogue(IEnumerable<Dialogue> array, int idxOffset, bool isSecondary);

        protected void GetDialogues()
        {
            string pathGeneric = $@"{GameManager.Preferences.language}\{SerializationSystem.PathToDialogues}{Owner.Name}\{GetParent().Name}";

            mainDialogues = SerializationSystem.LoadDialogues($@"{pathGeneric}\{MainDialogueDirName}");

            secondaryDialogues = SerializationSystem.LoadDialogues($@"{pathGeneric}\{SecondaryDialogueDirName}");
        }
    }
}

