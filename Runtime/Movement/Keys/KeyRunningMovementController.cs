using SkelTech.RPEST.Input.Keys;

using UnityEngine;

namespace SkelTech.RPEST.Movement.Keys {
    public class KeyRunningMovementController : MovementController<KeyCode> {
        #region Fields
        [SerializeField] protected KeyUpInputController upInputController;
        [SerializeField] protected KeyDownInputController downInputController;
        [SerializeField] private KeyCode runningKey = KeyCode.LeftShift;
        #endregion

        #region Initialization
        protected override bool IsInputInitialized() {
            return this.upInputController.IsInitialized() && this.downInputController.IsInitialized();
        }
        protected override void SetListeners() {
            this.upInputController.SetListener(this, this.runningKey, () => this.walkableObject.IsRunning = false);
            this.downInputController.SetListener(this, this.runningKey, () => this.walkableObject.IsRunning = true);
        }

        protected override void RemoveListeners() {
            this.upInputController.RemoveListener(this);
            this.downInputController.RemoveListener(this);
        }
        #endregion
    }
}
