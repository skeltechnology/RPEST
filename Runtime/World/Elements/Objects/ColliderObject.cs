using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    /// <summary>
    /// <c>MonoBehaviour</c> that represents a collider object.
    /// It must be a (sub-)child of a <c>World</c> component.
    /// </summary>
    public class ColliderObject : WorldObject {
        #region Fields
        /// <summary>
        /// Indicates if collision is enabled.
        /// </summary>
        [SerializeField] protected bool hasCollision = true;
        #endregion

        #region Getters
        /// <summary>
        /// Indicates if collision is enabled.
        /// </summary>
        /// <returns>Boolean indicating if collision is enabled.</returns>
        public bool HasCollision() {
            return this.gameObject.activeInHierarchy && this.hasCollision;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Indicates if this collider collides with the given collider object.
        /// </summary>
        /// <param name="colliderObject">Other collider object.</param>
        /// <returns>Boolean indicating if this collider collides with the given collider object.</returns>
        public bool CollidesWith(ColliderObject colliderObject) {
            if (colliderObject.HasCollision())  // Both need collision enabled
                return this.CollidesWith(colliderObject.transform.position);
            return false;
        }

        /// <summary>
        /// Indicates if this collider collides with the given position.
        /// </summary>
        /// <param name="colliderObjectPosition">Other collider object position.</param>
        /// <returns>Boolean indicating if this collider collides with the given position.</returns>
        public bool CollidesWith(Vector3 colliderObjectPosition) {
            return this.HasCollision() && this.Intersects(colliderObjectPosition);
        }
        #endregion

        #region Initialization
        protected override void InitializeWorldElement() {
            base.InitializeWorldElement();
            this.world.ColliderObjectDatabase.Add(this);
        }

        protected override void DisableWorldElement() {
            this.world.ColliderObjectDatabase.Remove(this);
            base.DisableWorldElement();
        }
        #endregion
    }
}
