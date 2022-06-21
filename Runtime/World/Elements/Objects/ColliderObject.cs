using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    public class ColliderObject : WorldObject {
        #region Fields
        [SerializeField] protected bool hasCollision = true;
        #endregion

        #region Getters
        public bool HasCollision() {
            return this.gameObject.activeInHierarchy && this.hasCollision;
        }
        #endregion

        #region Operators
        public bool CollidesWith(ColliderObject colliderObject) {
            if (colliderObject.HasCollision())  // Both need collision enabled
                return this.CollidesWith(colliderObject.transform.position);
            return false;
        }

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
