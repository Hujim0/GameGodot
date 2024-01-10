using Godot;
using GodotGame.UI;
using System;
using System.Collections.Generic;

namespace GodotGame.Dialogues.UI
{
	public partial class CharacterParentUI : Control
	{
		int lastCharacterCount = 0;

		const string PathToCharacterPrefab = "res://src/prefabs/Character.tscn";

		readonly PackedScene CharacterPrefab = GD.Load<PackedScene>(PathToCharacterPrefab);

		List<CharacterScript> list = new List<CharacterScript>();

		List<CharacterExpression> currentExpressions = new List<CharacterExpression>();

		public override void _Ready()
		{
			DialogueSystem.OnPanelChanged += OnPanelChanged;
			DialogueUI.TalkEnded += StopCharactersTalk;

			DialogueSystem.OnToggled += _ => ClearCharactersListFully();
			ViewportUI.OnSizeChange += UpdatePositions;
		}

		void OnPanelChanged(DialoguePanel panel)
		{
			//safety checks
			if (panel.evnt != null) return;

			if (panel.chars == null || panel.chars.Length == 0)
			{
				//delete existing
				ClearCharactersList();

				return;
			}

			currentExpressions.Clear();
			currentExpressions.AddRange(panel.chars);

			foreach (CharacterExpression exp in panel.chars)
            {
				if (string.IsNullOrEmpty(exp.talk))
					currentExpressions.Remove(exp);
			}

			if (currentExpressions.Count == 0)
            {
				lastCharacterCount = 0;
				return;
			}

			if (currentExpressions.Count == lastCharacterCount)
			{
				UpdateCharactersTalk();
				return;
			}

			//delete existing
			ClearCharactersList();

            //instantiate new
            for (int i = 0; i < currentExpressions.Count; i++)
            {
				Node Instance = CharacterPrefab.Instantiate();
				AddChild(Instance, true);

				CharacterScript newCharacter = GetNode<CharacterScript>(new NodePath(Instance.Name));
				list.Add(newCharacter);
			}

			lastCharacterCount = currentExpressions.Count;

			UpdatePositions(GetViewportRect().Size);
			UpdateCharactersTalk();
		}

		void ClearCharactersList()
        {
			foreach (CharacterScript character in list)
				character.QueueFree();

			list.Clear();
		}

		void ClearCharactersListFully()
		{
			foreach (CharacterScript character in list)
				character.QueueFree();

			list.Clear();

			lastCharacterCount = 0;

			currentExpressions.Clear();
		}

		void UpdatePositions(Vector2 viewportSize)
		{
			for (int i = 0; i < list.Count; i++)
			{
				Vector2 textureSize = list[i].Texture.GetSize();
				Vector2 pivotOffset = new Vector2(textureSize.X / 2, textureSize.Y);
				float Scale = (textureSize.Y * 0.075f) * (Size.Y / Size.X);

                list[i].PivotOffset = pivotOffset;

				list[i].Scale = new Vector2(Scale, Scale) * 0.8f; // is unhighlighted
                list[i].Position = new Vector2(
					Size.X * (i + 1) / (list.Count + 1),
					Mathf.Floor(Size.Y))
					//pivot offset
					- pivotOffset;
            }
		}

		void UpdateCharactersTalk()
		{
			if (list.Count == 0) return;

			for (int i = 0; i < list.Count; i++)
            {
				list[i].FlipH = currentExpressions[i].flip;
				list[i].anim.Play(currentExpressions[i].talk);
				list[i].IsHighlighted = currentExpressions[i].hl;
            }
		}

		void StopCharactersTalk()
		{
			if (list.Count == 0) return;

			for (int i = 0; i < list.Count; i++)
            {
				if (string.IsNullOrEmpty(currentExpressions[i].end)) continue;
				list[i].anim.Play(currentExpressions[i].end);
            }
		}
	}
}