using Godot;
using System.Collections.Generic;

namespace GodotGame.Dialogues.UI
{
	public class DialogueResponceUI : Control
	{
		const string PathToPrefab = "res://resrc/Prefabs/ResponceButton.tscn";

		readonly float buttonHeight = 15f;

		public static DialogueResponceUI Instance = null;

		public static bool inChoose = false;

		static List<ResponceScript> responcesList;

		[Export] public Vector2 offset = new Vector2(20f, 20f);

		static PackedScene prefab = GD.Load<PackedScene>(PathToPrefab);


		public override void _Ready()
		{
			if (Instance == null) Instance = this;

			responcesList = new List<ResponceScript>();
		}

		public void InstantiateButtons(DialogueResponce[] responces)
		{
			inChoose = true;

			foreach (DialogueResponce responce in responces)
			{
				Node newInstance = prefab.Instance();
				AddChild(newInstance, true);

				ResponceScript responceNode = GetNode<ResponceScript>(newInstance.Name);

				responceNode.SetResponce(responce);

				responcesList.Add(responceNode);
			}
			
			//focus set
			for (int i = 0; i < responcesList.Count; i++)
            {
				if (i != responcesList.Count - 1) responcesList[i].FocusNeighbourRight = responcesList[i + 1].GetPath();

				if (i != 0) responcesList[i].FocusNeighbourLeft = responcesList[i - 1].GetPath();

				FocusMode = FocusModeEnum.All;
			}

			responcesList[0].GrabFocus();

			UpdatePositions(RectSize);
		}
		
		void UpdatePositions(Vector2 marginSize)
		{
			float buttonWidth = (marginSize.x - (offset.x * (responcesList.Count + 1))) / responcesList.Count;

			for (int i = 0; i < responcesList.Count; i++)
			{
				responcesList[i].SetRect(
					size: new Vector2
					{
						x = buttonWidth,
						y = buttonHeight
					},

					position: new Vector2
					{
						x = (buttonWidth * i) + (offset.x * (i + 1)),
						y = (marginSize.y - buttonHeight) / 2,
					}
					/*<summary>
						|--|---------|--|---------|--|
							^       ^
						offset button	|	|	offsetCount = responcesCount + 1
					</summary>*/
				);
			}
		}

		public static void ActivateResponce(DialogueResponce responce)
		{
			inChoose = false;

			foreach (Node node in responcesList)
				node.QueueFree();

			responcesList.Clear();
			
			if (responce.panels != null) DialogueSystem.InsertDialoguePanels(responce.panels);
			DialogueSystem.NextSentence();
		}
	}
}
