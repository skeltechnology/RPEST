using UnityEngine;

namespace SkelTech.RPEST.Movement.Keyboard {
    public class KeyboardMovementController : MovementController<KeyCode> {
        #region Fields
        [SerializeField] private KeyCode upKey = KeyCode.W, downKey = KeyCode.S, leftKey = KeyCode.A, rightKey = KeyCode.D;
        #endregion

        #region Initialization
        protected override void SetListeners() {
            this.inputController.SetListener(this, this.upKey, this.walkableObject.MoveUp);
            this.inputController.SetListener(this, this.downKey, this.walkableObject.MoveDown);
            this.inputController.SetListener(this, this.leftKey, this.walkableObject.MoveLeft);
            this.inputController.SetListener(this, this.rightKey, this.walkableObject.MoveRight);
        }
        #endregion
    }
}
