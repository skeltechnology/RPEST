using UnityEngine;

namespace SkelTech.RPEST.Utilities.MonoBehaviours {
    [DisallowMultipleComponent]
    public abstract class ObjectChaser : MonoBehaviour {
        #region Fields
        [SerializeField] private Transform target;
        [SerializeField] private float smoothness = 8f;
        [SerializeField] private bool x, y, z;  // Frozen axes
        [SerializeField] private Vector3 offset;
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
        protected abstract Vector3 GetExtents();
        
        private Vector3 GetTargetPosition() {
            // TODO: TO INCREASE EFFICIENCY, ONLY NON FROZEN VALUES SHOULD BE CLAMPED
            Vector3 clampPosition = this.ClampPosition(this.target.position + this.offset);

            float x = this.x ? this.transform.position.x : clampPosition.x;
            float y = this.y ? this.transform.position.y : clampPosition.y;
            float z = this.z ? this.transform.position.z : clampPosition.z;

            return new Vector3(x, y, z);
        }
        #endregion

        #region Helpers
        private Vector3 ClampPosition(Vector3 position) {
            if (this.border == null) return position;

            Bounds bounds = this.border.bounds;
            Vector3 extents = this.GetExtents();
            float x = ClampAxis(position.x, bounds.min.x, bounds.max.x, extents.x);
            float y = ClampAxis(position.y, bounds.min.y, bounds.max.y, extents.y);
            float z = ClampAxis(position.z, bounds.min.z, bounds.max.z, extents.z);

            return new Vector3(x, y, z);
        }

        private static float ClampAxis(float position, float minPosition, float maxPosition, float extent) {
            return Mathf.Clamp(
                position,
                minPosition + extent,
                maxPosition - extent
            );
        }
        #endregion
    }
}
