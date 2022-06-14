using SkelTech.RPEST.World.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Movement {
    [RequireComponent(typeof(WalkableObject))]
    public abstract class MovementController<T> : MonoBehaviour {
        #region Fields
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
            if (this.IsInputInitialized())
                this.StartListening();
        }

        private void OnDisable() {
            this.EndListening();
        }
        #endregion

        #region Initialization
        protected abstract bool IsInputInitialized();
        protected abstract void SetListeners();
        protected abstract void RemoveListeners();

        private void StartListening() {
            if (!this.isListening) {
                this.isListening = true;
                this.SetListeners();
            }
        }

        private void EndListening() {
            if (this.isListening) {
                this.isListening = false;
                this.RemoveListeners();
            }
        }
        #endregion
    }
}