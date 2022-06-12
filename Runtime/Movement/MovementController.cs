using SkelTech.RPEST.Input;
using SkelTech.RPEST.World;

using UnityEngine;

namespace SkelTech.RPEST.Movement {
    [RequireComponent(typeof(WalkableObject))]
    public abstract class MovementController<T> : MonoBehaviour {
        #region Fields
        [SerializeField] protected InputController<T> inputController;
        protected WalkableObject walkableObject;

        private bool isListening = false;
        #endregion

        #region Unity
        private void Awake() {
            this.walkableObject = this.GetComponent<WalkableObject>();
        }

        private void Start() {
            this.StartListening();
        }

        private void OnEnable() {
            if (this.inputController.IsInitialized())
                this.StartListening();
        }

        private void OnDisable() {
            this.EndListening();
        }
        #endregion

        #region Initialization
        protected abstract void SetListeners();

        private void StartListening() {
            if (!this.isListening) {
                this.isListening = true;
                this.SetListeners();
            }
        }

        private void EndListening() {
            if (this.isListening) {
                this.isListening = false;
                this.inputController.RemoveListener(this);
            }
        }
        #endregion
    }
}
