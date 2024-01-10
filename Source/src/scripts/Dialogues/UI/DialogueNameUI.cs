using Godot;

namespace GodotGame.Dialogues.UI
{
	public partial class DialogueNameUI : RichTextLabel
	{
		CanvasItem ParentPanel;

		public override void _Ready() => ParentPanel = GetNode<CanvasItem>("../../");

		public void Hide(bool state) => ParentPanel.Visible = !state;
	}
}
