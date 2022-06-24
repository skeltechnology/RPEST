using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners {
    public abstract class InputListener : MonoBehaviour {
        #region Fields
        private bool isListening = false;
        #endregion

        #region Unity
        private void OnEnable() {
            this.StartListening();
        }

        private void OnDisable() {
            this.EndListening();
        }
        #endregion

        #region Initialization
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
