using Godot;
using System;

namespace GodotGame.PlayerBehaviour.Interaction
{
	public abstract class IInteractableHighLightable : IInteractable
	{
		const string pathToHighlightMaterial = @"res://resrc/Shaders/OutlineShader/OutlineMaterial.material";

		static readonly ShaderMaterial HighLightedMat = ResourceLoader.Load<ShaderMaterial>(pathToHighlightMaterial);

		public override bool IsHighLighted
		{
			get => isHighLighted;

			set
			{
				if (!IsInteractable) return;
				Material = value ? HighLightedMat : null;

				isHighLighted = value;
			}
		}

		public override abstract void OnInteracted();
	}
}
