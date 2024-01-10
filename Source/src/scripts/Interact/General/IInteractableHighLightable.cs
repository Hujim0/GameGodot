using Godot;
using System;

namespace GodotGame.PlayerBehavior.Interaction
{
	public abstract partial class IIntractableHighlightable : IIntractable
	{
		static readonly string pathToHighlightMaterial = @"res://src/visuals/Shaders/OutlineShader/OutlineMaterial.material";

		static readonly ShaderMaterial HighLightedMat = ResourceLoader.Load<ShaderMaterial>(pathToHighlightMaterial);
		public override bool IsHighLighted
		{
			get => isHighLighted;

			set
			{
				if (!IsIntractable) return;
				Material = value ? HighLightedMat : null;

				isHighLighted = value;
			}
		}

		public override abstract void OnInteracted();
	}
}
