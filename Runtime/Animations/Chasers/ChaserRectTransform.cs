using UnityEngine;

namespace SkelTech.RPEST.Animations.Chasers {
    /// <summary>
    /// MonoBehaviour that, when attached to an image, follows the target <c>Transform</c>.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class ChaserRectTransform : ObjectChaser {
        #region Fields
        /// <summary>
        /// <c>RectTransform</c> component that will follow the target.
        /// </summary>
        private RectTransform rectTransform;
        #endregion

        #region Unity
        private void Awake() {
            this.rectTransform = this.GetComponent<RectTransform>();
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the extents of the RectTransform.
        /// </summary>
        /// <returns>Extents of the renderer.</returns>
        protected override Vector3 GetExtents() {
            return (Vector3) this.rectTransform.sizeDelta / 2;
            
        }
        #endregion
    }
}
