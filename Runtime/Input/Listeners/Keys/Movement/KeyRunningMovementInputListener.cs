using SkelTech.RPEST.Input.Controllers.Keys;

using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners.Keys.Movement {
    /// <summary>
    /// <c>MonoBehaviour</c> responsible for handling input event to make the <c>WalkableObject</c> run.
    /// </summary>
    public class KeyRunningMovementInputListener : WalkableObjectInputListener {
        #region Fields
        /// <summary>
        /// Reference to the input controller.
        /// </summary>
        [SerializeField] protected KeyUpInputController upInputController;

        /// <summary>
        /// Reference to the input controller.
        /// </summary>
        [SerializeField] protected KeyDownInputController downInputController;

        /// <summary>
        /// Key associated with the movement.
        /// </summary>
        [SerializeField] private KeyCode runningKey = KeyCode.LeftShift;
        #endregion

        #region Initialization
        protected override void SetListeners() {
            this.upInputController.SetListener(this, this.runningKey, () => ChangeRunningState(false));
            this.downInputController.SetListener(this, this.runningKey, () => ChangeRunningState(true));
        }

        protected override void RemoveListeners() {
            this.upInputController.RemoveListener(this);
            this.downInputController.RemoveListener(this);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Callback method to change the running state of the <c>WalkableObject</c>.
        /// </summary>
        /// <param name="isRunning"></param>
        private void ChangeRunningState(bool isRunning) {
            this.walkableObject.IsRunning = isRunning;
        }
        #endregion
    }
}
