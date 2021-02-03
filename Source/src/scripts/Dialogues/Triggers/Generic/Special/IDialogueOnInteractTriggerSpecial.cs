using Godot;
using GodotGame.Serialization;
using System.Collections.Generic;

namespace GodotGame.Dialogues.Triggers
{
	public abstract class IDialogueOnInteractTriggerSpecial : IDialogueOnInteractTrigger
	{
		const string SpecialDirName = "Special";

		protected abstract bool IsSpecial { get; }

		protected int lastSpecialDialogueIdx;
        
		Dialogue[] SpecialMain;
		Dialogue[] SpecialSecondary;

		bool isCurrentSpecial = false;

        public override void OnInteracted()
		{
			#region file creation
			/*
						Dialogue dialogue = new Dialogue(
							priority: 0,
							dialoguePanels: new DialoguePanel[]
							{
								new DialoguePanel(
									name: "",
									text: null,
									timeBetweenCharacters: 0.05f,

									characters: new CharacterExpression[]
									{
											new CharacterExpression("fr_default", "fr_default"),
									},

								#region Responces
									responces: null
									*//* new DialogueResponce[]
									{
											new DialogueResponce(
												responceText: "",
												panels: new DialoguePanel[]
												{
														new DialoguePanel(
															name: "",
															text: "",
															timeBetweenCharacters: 0.05f,
															characters: new CharacterExpression[]
															{
																new CharacterExpression("fr_default", "fr_default"),
															},

															responces: null
															),
												}),

											new DialogueResponce(
												responceText: "",
												panels: new DialoguePanel[]
												{
														new DialoguePanel(
															name: "",
															text: "",
															timeBetweenCharacters: 0.05f,
															characters: new CharacterExpression[]
															{
																new CharacterExpression("fr_default", "fr_default"),
															},

															responces: null
															),
												}),
									}*//*
						#endregion

									)
							}
						);

						SerializationSystem.SaveDataGeneric(dialogue, $@"ru\dialogues\{pathToSecondary}\data2.json");
			*/
			#endregion

			isCurrentSpecial = IsSpecial;

			if (isCurrentSpecial)
            {
				if (SpecialMain != null && SpecialMain.Length != lastSpecialDialogueIdx)
				{
					DialogueSystem.StartDialogue(GetDialogue(SpecialMain));
					return;
				}

				int SpecialMainDialogueCount = 0;
				if (SpecialMain != null) SpecialMainDialogueCount = SpecialMain.Length;

				DialogueSystem.StartDialogue(GetDialogue(SpecialSecondary, SpecialMainDialogueCount, true));
			}
			else
            {
				if (mainDialogues != null && mainDialogues.Length != lastDialogueIdx)
				{
					DialogueSystem.StartDialogue(GetDialogue(mainDialogues));
					return;
				}

				int mainDialogueCount = 0;
				if (mainDialogues != null) mainDialogueCount = mainDialogues.Length;

				DialogueSystem.StartDialogue(GetDialogue(secondaryDialogues, mainDialogueCount, true));
			}
		}
		protected override Dialogue GetDialogue(IEnumerable<Dialogue> array, int idxOffset = 0, bool isSecondary = false)
		{
			Queue<Dialogue> queue = new Queue<Dialogue>();

			foreach (Dialogue dialogue in array)
			{
				if (dialogue.prior != lastDialogueIdx - idxOffset) continue;

				queue.Enqueue(dialogue);
			}

			if (queue.Count != 0)
			{
				if (!isSecondary || (lastDialogueIdx != (secondaryDialogues.Length - 1) + idxOffset))
                {
					if (IsSpecial) lastSpecialDialogueIdx++;
					else lastDialogueIdx++;
                }

				Dialogue[] result = new Dialogue[queue.Count];
				queue.CopyTo(result, 0);

				return result[rng.RandiRange(0, result.Length - 1)];
			}

			return null;
		}

        protected override void GetDialogues()
        {
			mainDialogues = SerializationSystem.LoadDialogues($@"{pathGeneric}\{MainDialogueDirName}");
			secondaryDialogues = SerializationSystem.LoadDialogues($@"{pathGeneric}\{SecondaryDialogueDirName}");


			SpecialMain = SerializationSystem.LoadDialogues($@"{pathGeneric}\{SpecialDirName}\{MainDialogueDirName}");
            SpecialSecondary = SerializationSystem.LoadDialogues($@"{pathGeneric}\{SpecialDirName}\{SecondaryDialogueDirName}");
        }
    }
}
