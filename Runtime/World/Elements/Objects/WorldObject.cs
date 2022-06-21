using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    [DisallowMultipleComponent]
    public class WorldObject : WorldElement {
        #region Getters
        public Bounds GetBounds() {
            return this.GetBounds(this.transform.position);
        }
        #endregion

        #region Operators
        public bool Intersects(WorldObject worldObject) {
            return this.Intersects(this.GetBounds(worldObject.transform.position));
        }

        public bool Intersects(Vector3 centerPosition) {
            return this.Intersects(this.GetBounds(centerPosition));
        }

        public bool Intersects(Bounds bounds) {
            return bounds.Intersects(this.GetBounds());
        }
        #endregion

        #region Convertion
        public Vector3 LocalToWorld(Vector3 position) {
            return Vector3Int.FloorToInt(position) + this.world.GetGrid().cellSize / 2;
        }
        #endregion

        #region Initialization
        protected override void InitializeWorldElement() {
            this.transform.localPosition = this.LocalToWorld(this.transform.localPosition);
        }
        #endregion

        #region Helpers
        public Bounds GetBounds(Vector3 position) {
            return new Bounds(position, this.world.GetGrid().cellSize * 0.99f);  // Avoid edges collision
        }
        #endregion
    }
}
