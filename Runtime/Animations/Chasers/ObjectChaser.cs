using UnityEngine;

namespace SkelTech.RPEST.Animations.Chasers {
    /// <summary>
    /// Abstract MonoBehaviour that follows the target <c>Transform</c>.
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class ObjectChaser : MonoBehaviour {
        #region Fields
        /// <summary>
        /// Target that will be followed by the chaser.
        /// </summary>
        [SerializeField] private Transform target;

        /// <summary>
        /// Smoothness of the chaser movement.
        /// The greater the value, the longer it will take the chaser to keep up.
        /// </summary>
        [SerializeField] private float smoothness = 8f;

        /// <summary>
        /// Boolean indicating if the respective axis is frozen.
        /// </summary>
        [SerializeField] private bool xAxisFrozen, yAxisFrozen, zAxisFrozen;

        /// <summary>
        /// Offset of the chaser relatively to the target.
        /// </summary>
        [SerializeField] private Vector3 offset;

        /// <summary>
        /// Optional parameter, that is used to calculate the area that the chaser is allowed to move in.
        /// </summary>
        [SerializeField] protected Renderer border;
        #endregion

        #region Unity
        private void Start() {
            this.transform.position = this.GetTargetPosition();
        }

        private void LateUpdate() {
            this.transform.position = Vector3.Lerp(
                this.transform.position, 
                this.GetTargetPosition(), 
                this.smoothness <= 0 ? 1 : 1 / (this.smoothness + 1) 
            );
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the extents of the chaser.
        /// </summary>
        /// <returns>Extents of the chaser.</returns>
        protected abstract Vector3 GetExtents();
        
        /// <summary>
        /// Gets the position of the target, with the necessary clamp according to the frozen axes, offset and border.
        /// </summary>
        /// <returns>Clamp position of the target.</returns>
        private Vector3 GetTargetPosition() {
            float x = this.transform.position.x, 
                  y = this.transform.position.y, 
                  z = this.transform.position.z;
            
            if (!this.xAxisFrozen) x = OffsetPosition(x, this.offset.x);
            if (!this.yAxisFrozen) y = OffsetPosition(y, this.offset.y);
            if (!this.zAxisFrozen) z = OffsetPosition(z, this.offset.z);

            if (this.border != null) {
                Bounds bounds = this.border.bounds;
                Vector3 extents = this.GetExtents();
                if (!this.xAxisFrozen) x = ClampPosition(x, bounds.min.x, bounds.max.x, extents.x);
                if (!this.yAxisFrozen) y = ClampPosition(y, bounds.min.y, bounds.max.y, extents.y);
                if (!this.zAxisFrozen) z = ClampPosition(z, bounds.min.z, bounds.max.z, extents.z);
            }

            return new Vector3(x, y, z);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Offsets a position with a given value.
        /// </summary>
        /// <param name="position">Axis position.</param>
        /// <param name="offset">Position offset.</param>
        /// <returns>Offsetted position.</returns>
        private static float OffsetPosition(float position, float offset) {
            return position + offset;
        }

        /// <summary>
        /// Clamps an axis position according to the given parameters.
        /// </summary>
        /// <param name="position">Axis position.</param>
        /// <param name="minPosition">Minumum axis position.</param>
        /// <param name="maxPosition">Maxiumum axis position.</param>
        /// <param name="extent">Extent of the chaser.</param>
        /// <returns>Clam axis position.</returns>
        private static float ClampPosition(float position, float minPosition, float maxPosition, float extent) {
            return Mathf.Clamp(
                position,
                minPosition + extent,
                maxPosition - extent
            );
        }
        #endregion
    }
}
