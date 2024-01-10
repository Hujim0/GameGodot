using Godot;
using System;

namespace GodotGame.PlayerBehavior.Interaction
{
    /// <summary>
    ///	Note: put the Node with IIntractable component first on a Sprite node
    /// </summary>
    public abstract partial class IIntractable : Sprite2D
    {
        bool isIntractable = true;
        public bool IsIntractable
        {
            get => isIntractable;

            set
            {
                if (!value) IsHighLighted = false;
                isIntractable = value;
            }
        }
        protected bool isHighLighted = false;
        public abstract bool IsHighLighted { get; set; }

        public abstract void OnInteracted();
    }
}