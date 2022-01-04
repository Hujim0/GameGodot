using Godot;
using GodotGame.UI;
using System;
using System.Collections.Generic;

namespace GodotGame.Dialogues.UI
{
	public class CharacterParentUI : Control
	{
		int lastCharacterCount = 0;

		const string PathToCharacterPrefab = "res://src/prefabs/Character.tscn";

		readonly PackedScene CharacterPrefab = GD.Load<PackedScene>(PathToCharacterPrefab);

		List<CharacterScript> list = new List<CharacterScript>();

		CharacterExpression[] currentExpressions;

		public override void _Ready()
		{
			DialogueSystem.OnPanelChanged += OnPanelChanged;
			DialogueUI.TalkEnded += StopCharactersTalk;

			DialogueSystem.OnToggled += _ => ClearCharactersList();
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

			currentExpressions = panel.chars;

			if (currentExpressions.Length == lastCharacterCount)
			{
				UpdateCharactersTalk(currentExpressions);
				return;
			}

			//delete existing
			ClearCharactersList();
			
			list.Clear();

            //instanciate new
            for (int i = 0; i < currentExpressions.Length; i++)
            {
				Node Instance = CharacterPrefab.Instance();
				AddChild(Instance, true);

				CharacterScript newCharacter = GetNode<CharacterScript>(Instance.Name);
				list.Add(newCharacter);
			}

			lastCharacterCount = currentExpressions.Length;

			UpdatePositions(ViewportUI.viewport.Size);
			UpdateCharactersTalk(currentExpressions);
		}

		void ClearCharactersList()
        {
			foreach (CharacterScript character in list)
				character.QueueFree();

			list.Clear();

			lastCharacterCount = 0;

		}

		void UpdatePositions(Vector2 viewportSize)
		{
			for (int i = 0; i < list.Count; i++)
			{
				Vector2 textureSize = list[i].Texture.GetSize();
				Vector2 pivotOffset = new Vector2(textureSize.x / 2, textureSize.y);
				float Scale = (textureSize.y * 0.075f) * (RectSize.y / RectSize.x);

                list[i].RectPivotOffset = pivotOffset;

				list[i].RectScale = new Vector2(Scale, Scale) * 0.8f; // is unhighlighted
                list[i].RectPosition = new Vector2(
					x: RectSize.x * (i + 1) / (list.Count + 1),
					y: Mathf.Floor(RectSize.y))
					//pivot offset
					- pivotOffset;
            }
		}

		void UpdateCharactersTalk(CharacterExpression[] expressions)
		{
			if (list.Count == 0) return;

			for (int i = 0; i < list.Count; i++)
            {
				list[i].FlipH = expressions[i].flip;
				list[i].anim.Play(expressions[i].talk);
				list[i].IsHighlighted = expressions[i].hl;
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