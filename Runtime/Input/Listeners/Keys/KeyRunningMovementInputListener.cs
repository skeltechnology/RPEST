using SkelTech.RPEST.Input.Controllers.Keys;

using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners.Keys {
    public class KeyRunningMovementInputListener : WalkableObjectInputListener {
        #region Fields
        [SerializeField] protected KeyUpInputController upInputController;
        [SerializeField] protected KeyDownInputController downInputController;
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
        private void ChangeRunningState(bool isRunning) {
            this.walkableObject.IsRunning = isRunning;
        }
        #endregion
    }
}
