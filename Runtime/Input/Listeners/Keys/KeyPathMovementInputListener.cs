using SkelTech.RPEST.Input.Controllers.Keys;

using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners.Keys {
    public class KeyPathMovementInputListener : WalkableObjectInputListener {
        #region Fields
        [SerializeField] private new Camera camera;
        [SerializeField] protected KeyDownInputController inputController;
        [SerializeField] private KeyCode clickKey = KeyCode.Mouse0, stopKey = KeyCode.Mouse1;
        #endregion

        #region Initialization
        protected override void SetListeners() {
            this.inputController.SetListener(this, this.clickKey, this.MoveObject);
            this.inputController.SetListener(this, stopKey, this.walkableObject.StopMoving);
        }

        protected override void RemoveListeners() {
            this.inputController.RemoveListener(this);
        }
        #endregion

        #region Helpers
        private void MoveObject() {
            Vector3 mousePosition = this.camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            this.walkableObject.MoveTo(new Vector3(mousePosition.x, mousePosition.y, this.walkableObject.transform.position.z));
        }
        #endregion
    }
}
