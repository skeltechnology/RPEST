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

        public Bounds GetBounds() {
            return this.GetBounds(this.transform.position);
        }
        #endregion

        #region Operators
        public bool CollidesWith(ColliderObject colliderObject) {
            if (colliderObject.HasCollision())  // Both need collision enabled
                return this.CollidesWith(colliderObject.transform.position);
            return false;
        }

        public bool CollidesWith(Vector3 colliderObjectPosition) {
            return this.HasCollision() && this.Intersects(this.GetBounds(colliderObjectPosition));
        }

        public bool Intersects(ColliderObject colliderObject) {
            return this.Intersects(this.GetBounds(colliderObject.transform.position));
        }

        public bool Intersects(Bounds bounds) {
            return bounds.Intersects(this.GetBounds());
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

        #region Helpers
        public Bounds GetBounds(Vector3 position) {
            return new Bounds(position, this.world.GetGrid().cellSize * 0.99f);  // Avoid edges collision
        }
        #endregion
    }
}
