using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    /// <summary>
    /// <c>MonoBehaviour</c> that represents a world object.
    /// It must be a (sub-)child of a <c>World</c> component.
    /// </summary>
    [DisallowMultipleComponent]
    public class WorldObject : WorldElement {
        #region Unity
        protected void OnEnable() {
            if (this.world) this.InitializeWorldElement();
        }

        protected void OnDisable() {
            if (this.world) this.DisableWorldElement();
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the bounds of the world object.
        /// </summary>
        /// <returns>Bounds of the world object.</returns>
        public Bounds GetBounds() {
            return this.GetBounds(this.transform.position);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Indicates if this world object intersects the given world object.
        /// </summary>
        /// <param name="worldObject">Other world object.</param>
        /// <returns>Boolean indicating if this world object intersects the given world object.</returns>
        public bool Intersects(WorldObject worldObject) {
            return this.Intersects(this.GetBounds(worldObject.transform.position));
        }

        /// <summary>
        /// Indicates if this world object intersects the given position.
        /// </summary>
        /// <param name="centerPosition">Other center position.</param>
        /// <returns>Boolean indicating if this world object intersects the given position.</returns>
        public bool Intersects(Vector3 centerPosition) {
            return this.Intersects(this.GetBounds(centerPosition));
        }

        /// <summary>
        /// Indicates if this world object intersects the given bounds.
        /// </summary>
        /// <param name="bounds">Other bounds.</param>
        /// <returns>Boolean indicating if this world object intersects the given bounds.</returns>
        public bool Intersects(Bounds bounds) {
            return bounds.Intersects(this.GetBounds());
        }
        #endregion

        #region Convertion
        /// <summary>
        /// Clamps and converts the given position to the world position, by centering it with the cell.
        /// </summary>
        /// <param name="position">Position to be converted.</param>
        /// <returns>Converted world position.</returns>
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
        /// <summary>
        /// Gets the bounds at which the given position is the center and has the cell size.
        /// </summary>
        /// <param name="position">Center position.</param>
        /// <returns>Bounds at which the given position is the center and has the cell size.</returns>
        public Bounds GetBounds(Vector3 position) {
            return new Bounds(position, this.world.GetGrid().cellSize * 0.99f);  // 0.99f to avoid edges collision.
        }
        #endregion
    }
}
