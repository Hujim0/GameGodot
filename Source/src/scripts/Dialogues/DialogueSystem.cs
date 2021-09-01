using Godot;
using System.Collections;
using System.Collections.Generic;

namespace GodotGame.Dialogues
{
	public static class DialogueSystem
	{
		#region VARIEBLES
		public static bool IsDialogue { get; private set; }

		public delegate void OnDialogueToggle(bool ctx);
		public static event OnDialogueToggle OnToggled;

		public delegate void OnPanelChange(DialoguePanel panel);
		public static event OnPanelChange OnPanelChanged;
		static Queue<DialoguePanel> dialoguePanels { get; set; }

        #endregion

        public static void InsertDialoguePanels(DialoguePanel[] panels)
        {
			GD.Print("!! Inserting dialogues");

			DialoguePanel[] currentPanels = new DialoguePanel[dialoguePanels.Count];
			dialoguePanels.CopyTo(currentPanels, 0);

			dialoguePanels.Clear();

			foreach(DialoguePanel panel in panels)
				dialoguePanels.Enqueue(panel);

			foreach (DialoguePanel panel in currentPanels)
				dialoguePanels.Enqueue(panel);
		}

		public static void StartDialogue(Dialogue _dialogue)
		{
			if (IsDialogue) return;

			if (_dialogue == null) { GD.PrintErr($"!!! Dialogue is Null !!!"); return; }

			if (_dialogue.panels == null || _dialogue.panels.Length == 0)
			{
				GD.PrintErr($"!!! Missing DialoguePanels !!!");
				return;
			}

			dialoguePanels = new Queue<DialoguePanel>();

			IsDialogue = true;

			foreach (DialoguePanel dialoguePanel in _dialogue.panels)
				dialoguePanels.Enqueue(dialoguePanel);

			GD.Print($"--- Starting Dialogue ---");

			OnToggled?.Invoke(true);

			NextSentence();
		}

		public static void NextSentence()
        {
            if (dialoguePanels.Count != 0)
            {
                DialoguePanel nextDialoguePanel = dialoguePanels.Dequeue();

                OnPanelChanged?.Invoke(nextDialoguePanel);

				return;
			}

            EndDialogue();
        }
        public static void EndDialogue()
		{
			OnToggled?.Invoke(false);

			GD.Print($"--- The dialogue is over. ---");

			IsDialogue = false;
		}
	}
}
