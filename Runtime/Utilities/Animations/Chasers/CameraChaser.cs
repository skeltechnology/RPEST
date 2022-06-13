using UnityEngine;

namespace SkelTech.RPEST.Utilities.MonoBehaviours {
    [RequireComponent(typeof(Camera))]
    public class CameraChaser : ObjectChaser {
        #region Fields
        private new Camera camera;
        #endregion

        #region Unity
        private void Awake() {
            this.camera = this.GetComponent<Camera>();
        }
        #endregion

        #region Getters
        protected override Vector3 GetExtents() {
            return new Vector3(this.camera.orthographicSize * this.camera.aspect, this.camera.orthographicSize, 0);
        }
        #endregion
    }
}
