using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    [DisallowMultipleComponent]
    public class WorldObject : WorldElement {
        #region Fields
        [SerializeField] protected bool isObstacle = true;
        #endregion

        #region Getters
        public bool IsObstacle() {
            return this.gameObject.activeInHierarchy && this.isObstacle;
        }

        public Bounds GetBounds() {
            return this.GetBounds(this.transform.position);
        }
        #endregion

        #region Operators
        public bool CollidesWith(WorldObject worldObject) {
            if (worldObject.IsObstacle()) {
                return this.CollidesWith(worldObject.transform.position);
            }
            return false;
        }

        public bool CollidesWith(Vector3 position) {
            return this.CollidesWith(this.GetBounds(position));
        }

        public bool CollidesWith(Bounds bounds) {
            if (this.IsObstacle()) {
                return bounds.Intersects(this.GetBounds());
            }
            return false;
        }
        #endregion

        #region Initialization
        protected override void InitializeWorldElement() {
            this.world.WorldObjectDatabase.Add(this);
            this.transform.localPosition = Vector3Int.FloorToInt(this.transform.localPosition) + this.world.GetGrid().cellSize / 2;
        }

        protected override void DisableWorldElement() {
            this.world.WorldObjectDatabase.Remove(this);
        }
        #endregion

        #region Helpers
        public Bounds GetBounds(Vector3 position) {
            return new Bounds(position, this.world.GetGrid().cellSize * 0.99f);  // Avoid edges collision
        }
        #endregion
    }
}
