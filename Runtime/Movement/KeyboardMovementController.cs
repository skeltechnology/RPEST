using SkelTech.RPEST.Input.Keyboard;
using SkelTech.RPEST.World;

using UnityEngine;

namespace SkelTech.RPEST.Movement {
    [RequireComponent(typeof(WalkableObject))]
    public class KeyboardMovementController : MonoBehaviour {
        #region Fields
        [SerializeField] private KeyboardInputController inputController;
        [SerializeField] private KeyCode upKey = KeyCode.W, downKey = KeyCode.S, leftKey = KeyCode.A, rightKey = KeyCode.D;

        private WalkableObject walkableObject;
        #endregion

        #region Unity
        private void Awake() {
            this.walkableObject = this.GetComponent<WalkableObject>();
        }

        private void Start() {
            this.StartListening();
        }

        private void OnDestroy() {
            this.EndListening();
        }
        #endregion

        #region Initialization
        private void StartListening() {
            this.inputController.SetListener(this, this.upKey, this.walkableObject.MoveUp);
            this.inputController.SetListener(this, this.downKey, this.walkableObject.MoveDown);
            this.inputController.SetListener(this, this.leftKey, this.walkableObject.MoveLeft);
            this.inputController.SetListener(this, this.rightKey, this.walkableObject.MoveRight);
        }

        private void EndListening() {
            this.inputController.RemoveListener(this);
        }
        #endregion
    }
}
