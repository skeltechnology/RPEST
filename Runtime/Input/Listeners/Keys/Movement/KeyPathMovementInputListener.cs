using SkelTech.RPEST.Input.Controllers.Keys;

using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners.Keys.Movement {
    /// <summary>
    /// <c>MonoBehaviour</c> responsible for handling input event to move the <c>WalkableObject</c> to the correspondent position.
    /// </summary>
    public class KeyPathMovementInputListener : WalkableObjectInputListener {
        #region Fields
        /// <summary>
        /// Reference to the camera component.
        /// </summary>
        [SerializeField] private new Camera camera;

        /// <summary>
        /// Reference to the input controller.
        /// </summary>
        [SerializeField] protected KeyDownInputController inputController;

        /// <summary>
        /// Key associated with the movement.
        /// </summary>
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
        /// <summary>
        /// Callback method to move the <c>WalkableObject</c> to the selected position.
        /// </summary>
        private void MoveObject() {
            Vector3 mousePosition = this.camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            this.walkableObject.MoveTo(new Vector3(mousePosition.x, mousePosition.y, this.walkableObject.transform.position.z));
        }
        #endregion
    }
}
