using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners {
    [RequireComponent(typeof(WalkableObject))]
    public abstract class WalkableObjectInputListener : InputListener {
        #region Fields
        protected WalkableObject walkableObject;
        #endregion

        #region Unity
        private void Awake() {
            this.walkableObject = this.GetComponent<WalkableObject>();
        }
        #endregion
    }
}
