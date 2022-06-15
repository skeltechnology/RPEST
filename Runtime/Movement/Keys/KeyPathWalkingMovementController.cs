using SkelTech.RPEST.Input.Keys;

using UnityEngine;

namespace SkelTech.RPEST.Movement.Keys {
    public class KeyPathWalkingMovementController : MovementController<KeyCode> {
        #region Fields
        [SerializeField] private new Camera camera;
        [SerializeField] protected KeyDownInputController inputController;
        [SerializeField] private KeyCode clickKey = KeyCode.Mouse0;
        #endregion

        #region Initialization
        protected override bool IsInputInitialized() {
            return this.inputController.IsInitialized();
        }

        protected override void SetListeners() {
            this.inputController.SetListener(this, this.clickKey, this.MoveObject);
        }

        protected override void RemoveListeners() {
            this.inputController.RemoveListener(this);
        }
        #endregion

        #region Helpers
        private void MoveObject() {
            Vector3 mousePosition = this.camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            System.DateTime before = System.DateTime.Now;
            this.walkableObject.MoveTo(new Vector3(mousePosition.x, mousePosition.y, this.walkableObject.transform.position.z));
            System.DateTime after = System.DateTime.Now;
            System.TimeSpan duration = after.Subtract(before);
            Debug.Log("Duration: " + duration.Milliseconds + "ms");
        }
        #endregion
    }
}
