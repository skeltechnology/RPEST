using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners {
    /// <summary>
    /// Base class for interacting with a <c>WalkableObject</c> with input listening.
    /// </summary>
    [RequireComponent(typeof(WalkableObject))]
    public abstract class WalkableObjectInputListener : InputListener {
        #region Fields
        /// <summary>
        /// Reference to the walkable object.
        /// </summary>
        protected WalkableObject walkableObject;
        #endregion

        #region Unity
        private void Awake() {
            this.walkableObject = this.GetComponent<WalkableObject>();
        }
        #endregion
    }
}
