using UnityEngine;

namespace SkelTech.RPEST.Animations.Chasers {
    /// <summary>
    /// MonoBehaviour that, when attached to a renderer, follows the target <c>Transform</c>.
    /// </summary>
    [RequireComponent(typeof(Renderer))]
    public class RendererChaser : ObjectChaser {
        #region Fields
        /// <summary>
        /// Renderer component that will follow the target.
        /// </summary>
        private new Renderer renderer;
        #endregion

        #region Unity
        private void Awake() {
            this.renderer = this.GetComponent<Renderer>();
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the extents of the renderer.
        /// </summary>
        /// <returns>Extents of the renderer.</returns>
        protected override Vector3 GetExtents() {
            return this.renderer.bounds.extents;
        }
        #endregion
    }
}
