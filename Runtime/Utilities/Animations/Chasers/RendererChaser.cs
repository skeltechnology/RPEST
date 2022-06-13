using UnityEngine;

namespace SkelTech.RPEST.Utilities.MonoBehaviours {
    [RequireComponent(typeof(Renderer))]
    public class RendererChaser : ObjectChaser {
        #region Fields
        private new Renderer renderer;
        #endregion

        #region Unity
        private void Awake() {
            this.renderer = this.GetComponent<Renderer>();
        }
        #endregion

        #region Getters
        protected override Vector3 GetExtents() {
            return this.renderer.bounds.extents;
        }
        #endregion
    }
}
