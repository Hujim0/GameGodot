using Godot;
using GodotGame.UI;
using System;
using System.Collections.Generic;

namespace GodotGame.Dialogues.UI
{
	public class CharacterParentUI : Control
	{
		int lastCharacterCount = 0;

		const string PathToCharacterPrefab = "res://resrc/Prefabs/Character.tscn";

		PackedScene CharacterPrefab = GD.Load<PackedScene>(PathToCharacterPrefab);

		List<CharacterScript> list = new List<CharacterScript>();

		CharacterExpression[] currentExpressions;

		public override void _Ready()
		{
			DialogueSystem.OnPanelChanged += OnChildUpdate;
			DialogueUI.TalkEnded += StopCharactersTalk;

			ViewportUI.OnSizeChange += UpdatePositions;
		}

		void OnChildUpdate(DialoguePanel panel)
		{
			//safety checks
			if (panel.chars == null || panel.chars.Length == 0)
			{
				//delete existing
				foreach (CharacterScript character in list)
					character.QueueFree();

				list.Clear();

				return;
			}

			currentExpressions = panel.chars;

			if (panel.chars.Length == lastCharacterCount)
			{
				UpdateCharactersTalk(panel.chars);
				return;
			}


			//delete existing
			foreach (CharacterScript character in list)
				character.QueueFree();
			
			list.Clear();

			//instanciate new
			foreach (CharacterExpression expression in panel.chars)
			{
				Node Instance = CharacterPrefab.Instance();
				AddChild(Instance, true);

				CharacterScript newCharacter = GetNode<CharacterScript>(Instance.Name);
				list.Add(newCharacter);

				newCharacter.anim.Play(expression.talk);
			}

			UpdatePositions(ViewportUI.viewport.Size);

		}

		private void UpdatePositions(Vector2 viewportSize)
		{
			for (int i = 0; i < list.Count; i++)
			{
				Vector2 textureSize = list[i].Texture.GetSize();
				list[i].RectPosition = new Vector2(
					viewportSize.x * (i + 1) / (list.Count + 1),
					viewportSize.y - DialogueUI.textPanelHeight)
					//pivot offset
					- new Vector2(textureSize.x / 2, textureSize.y);
			}
		}

		void UpdateCharactersTalk(CharacterExpression[] expressions)
		{
			if (list.Count == 0) return;

			for (int i = 0; i < list.Count; i++)
				list[i].anim.Play(expressions[i].talk);
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