using UnityEngine;

namespace SkelTech.RPEST.Input.Listeners {
    /// <summary>
    /// Base class for listening to input actions.
    /// </summary>
    public abstract class InputListener : MonoBehaviour {
        #region Fields
        /// <summary>
        /// Boolean indicating if the class is currently listening.
        /// </summary>
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
        /// <summary>
        /// Initializes the necessary listening operations.
        /// </summary>
        protected abstract void SetListeners();

        /// <summary>
        /// Disables the necessary listening operations.
        /// </summary>
        protected abstract void RemoveListeners();

        /// <summary>
        /// Wrapper to initialize the listening operations.
        /// </summary>
        private void StartListening() {
            if (!this.isListening) {
                this.isListening = true;
                this.SetListeners();
            }
        }

        /// <summary>
        /// Wrapper to disable the listening operations.
        /// </summary>
        private void EndListening() {
            if (this.isListening) {
                this.isListening = false;
                this.RemoveListeners();
            }
        }
        #endregion
    }
}
