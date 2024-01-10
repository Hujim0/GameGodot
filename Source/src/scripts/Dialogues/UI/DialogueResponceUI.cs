using Godot;
using System.Collections.Generic;

namespace GodotGame.Dialogues.UI
{
	public partial class DialogueResponseUI : Control
	{
		const string PathToPrefab = "res://src/prefabs/ResponceButton.tscn";

		readonly float buttonHeight = 15f;

		public static DialogueResponseUI Instance = null;

		public static bool inChoose = false;

		static List<ResponseScript> responsesList;

		[Export] public Vector2 offset = new Vector2(20f, 20f);

		static readonly PackedScene prefab = GD.Load<PackedScene>(PathToPrefab);


		public override void _Ready()
		{
			if (Instance == null) Instance = this;

			responsesList = new List<ResponseScript>();
		}

		public void InstantiateButtons(DialogueResponse[] responses)
		{
			inChoose = true;

			foreach (DialogueResponse response in responses)
			{
				Node newInstance = prefab.Instantiate();
				AddChild(newInstance, true);

				ResponseScript responseNode = GetNode<ResponseScript>(new NodePath(newInstance.Name));

				responseNode.SetResponse(response);

				responsesList.Add(responseNode);
			}
			
			//focus set
			for (int i = 0; i < responsesList.Count; i++)
            {
				if (i != responsesList.Count - 1) responsesList[i].FocusNeighborRight = responsesList[i + 1].GetPath();

				if (i != 0) responsesList[i].FocusNeighborLeft = responsesList[i - 1].GetPath();

				FocusMode = FocusModeEnum.All;
			}

			responsesList[0].GrabFocus();

			UpdatePositions(Size);
		}
		
		void UpdatePositions(Vector2 marginSize)
		{
			float buttonWidth = (marginSize.X - (offset.X * (responsesList.Count + 1))) / responsesList.Count;

			for (int i = 0; i < responsesList.Count; i++)
			{
				responsesList[i].SetRect(
					size: new Vector2
					{
						X = buttonWidth,
						Y = buttonHeight
					},

					position: new Vector2
					{
						X = (buttonWidth * i) + (offset.X * (i + 1)),
						Y = ((marginSize.Y - buttonHeight) / 2) + ResponseScript.ANIM_HEIGHT,
					},
					/*<summary>
						|--|---------|--|---------|--|
						  ^       ^
						offset  button	|	|	offsetCount = responsesCount + 1
					</summary>*/
					i
				);
			}
		}

		public static void ActivateResponse(DialogueResponse response)
		{
			inChoose = false;

			foreach (Node node in responsesList)
            {
				if (node.Name == "Tween") continue;
				node.QueueFree();
            }

			responsesList.Clear();
			
			if (response.panels != null) DialogueSystem.InsertDialoguePanels(response.panels);
			DialogueSystem.NextSentence();
		}
	}
}
