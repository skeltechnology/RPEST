using UnityEngine;

namespace SkelTech.RPEST.Utilities.Animations.Chasers {
    [RequireComponent(typeof(Renderer))]
    public class ObjectChaser : MonoBehaviour {
        #region Fields
        [SerializeField] private Transform target;
        [SerializeField] private float smoothness = 8f;
        [SerializeField] private bool x, y, z;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Renderer border;

        private new Renderer renderer;
        #endregion

        #region Unity
        private void Awake() {
            this.renderer = this.GetComponent<Renderer>();
        }

        private void Start() {
            this.transform.position = this.GetTargetPosition();
        }

        private void LateUpdate() {
            this.transform.position = Vector3.Lerp(this.transform.position, this.GetTargetPosition(), this.smoothness <= 0 ? 1 : 1 / (this.smoothness + 1) );
        }
        #endregion

        #region Helpers
        private Vector3 GetTargetPosition() {
            // TODO: TO INCREASE EFFICIENCY, ONLY NON FROZEN VALUES SHOULD BE CLAMPED
            Vector3 clampPosition = this.ClampPosition(this.target.position + this.offset);

            float x = this.x ? this.transform.position.x : clampPosition.x;
            float y = this.y ? this.transform.position.y : clampPosition.y;
            float z = this.z ? this.transform.position.z : clampPosition.z;

            return new Vector3(x, y, z);
        }

        private Vector3 ClampPosition(Vector3 position) {
            if (this.border == null) return position;

            Bounds bounds = this.border.bounds;
            Vector3 extents = this.renderer.bounds.extents;
            float x = this.ClampAxis(position.x, bounds.min.x, bounds.max.x, extents.x);
            float y = this.ClampAxis(position.y, bounds.min.y, bounds.max.y, extents.y);
            float z = this.ClampAxis(position.z, bounds.min.z, bounds.max.z, extents.z);

            return new Vector3(x, y, z);
        }

        private float ClampAxis(float position, float minPosition, float maxPosition, float extent) {
            return Mathf.Clamp(
                position,
                minPosition + extent,
                maxPosition - extent
            );
        }
        #endregion
    }
}
