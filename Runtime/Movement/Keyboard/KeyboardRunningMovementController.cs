using SkelTech.RPEST.Input.Keyboard;

using UnityEngine;

namespace SkelTech.RPEST.Movement.Keyboard {
    public class KeyboardRunningMovementController : MovementController<KeyCode> {
        #region Fields
        [SerializeField] protected KeyboardUpInputController upInputController;
        [SerializeField] protected KeyboardDownInputController downInputController;
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
