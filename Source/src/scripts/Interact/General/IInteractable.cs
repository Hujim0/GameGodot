using Godot;
using System;

namespace GodotGame.PlayerBehaviour.Interaction
{
    /// <summary>
    ///	Note: put the Node with IInterctable component first on a Sprite node
    /// </summary>
    public abstract class IInteractable : Sprite
    {
        bool isInteractable = true;
        public bool IsInteractable
        {
            get => isInteractable;

            set
            {
                if (!value) IsHighLighted = false;
                isInteractable = value;
            }
        }
        protected bool isHighLighted = false;
        public abstract bool IsHighLighted { get; set; }

        public abstract void OnInteracted();
    }
}