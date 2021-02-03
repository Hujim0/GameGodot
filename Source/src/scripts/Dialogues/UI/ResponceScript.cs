using Godot;

namespace GodotGame.Dialogues.UI
{
	public class ResponceScript : Button
	{
		public DialogueResponce responce;

		public override void _Pressed() => DialogueResponceUI.ActivateResponce(responce);

		public void SetResponce(DialogueResponce responce)
		{
			this.responce = responce;

			Text = responce.responceText;
		}

		/// <param name="size"></param>
		/// <param name="position"></param>
		public void SetRect(Vector2 size, Vector2 position)
		{
			RectSize = size;
			RectPosition = position;
		}
	}
}
