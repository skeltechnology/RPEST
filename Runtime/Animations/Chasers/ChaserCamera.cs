using UnityEngine;

namespace SkelTech.RPEST.Animations.Chasers {
    /// <summary>
    /// MonoBehaviour that, when attached to a camera, follows the target <c>Transform</c>.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class ChaserCamera : ObjectChaser {
        #region Fields
        /// <summary>
        /// Camera component that will follow the target.
        /// </summary>
        private new Camera camera;
        #endregion

        #region Unity
        private void Awake() {
            this.camera = this.GetComponent<Camera>();
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the extents of the camera.
        /// </summary>
        /// <returns>Extents of the camera.</returns>
        protected override Vector3 GetExtents() {
            return new Vector3(this.camera.orthographicSize * this.camera.aspect, this.camera.orthographicSize, 0);
        }
        #endregion
    }
}
