using SkelTech.RPEST.Input.Controllers.Keys;

using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners.Keys.Movement {
    /// <summary>
    /// <c>MonoBehaviour</c> responsible for handling input event to move the <c>WalkableObject</c> in the correspondent direction.
    /// </summary>
    public class KeyDirectionMovementInputListener : WalkableObjectInputListener {
        #region Fields
        /// <summary>
        /// Reference to the input controller.
        /// </summary>
        [SerializeField] protected KeyHoldInputController inputController;

        /// <summary>
        /// Key associated with the movement.
        /// </summary>
        [SerializeField] private KeyCode upKey = KeyCode.W, downKey = KeyCode.S, leftKey = KeyCode.A, rightKey = KeyCode.D;
        #endregion

        #region Initialization
        protected override void SetListeners() {
            this.inputController.SetListener(this, this.upKey, this.walkableObject.MoveUp);
            this.inputController.SetListener(this, this.downKey, this.walkableObject.MoveDown);
            this.inputController.SetListener(this, this.leftKey, this.walkableObject.MoveLeft);
            this.inputController.SetListener(this, this.rightKey, this.walkableObject.MoveRight);
        }

        protected override void RemoveListeners() {
            this.inputController.RemoveListener(this);
        }
        #endregion
    }
}
