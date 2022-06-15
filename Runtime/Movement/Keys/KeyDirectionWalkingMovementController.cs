using SkelTech.RPEST.Input.Keys;

using UnityEngine;

namespace SkelTech.RPEST.Movement.Keys {
    public class KeyDirectionWalkingMovementController : MovementController<KeyCode> {
        #region Fields
        [SerializeField] protected KeyHoldInputController inputController;
        [SerializeField] private KeyCode upKey = KeyCode.W, downKey = KeyCode.S, leftKey = KeyCode.A, rightKey = KeyCode.D;
        #endregion

        #region Initialization
        protected override bool IsInputInitialized() {
            return this.inputController.IsInitialized();
        }

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
