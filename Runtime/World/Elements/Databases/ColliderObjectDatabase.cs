using SkelTech.RPEST.World.Elements.Objects;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Database {
    /// <summary>
    /// Class for managing <c>ColliderObject</c>s.
    /// </summary>
    public class ColliderObjectDatabase : WorldDatabase<ColliderObject> {
        #region Getters
        /// <summary>
        /// Indicates if there is a <c>ColliderObject</c> at the given position.
        /// </summary>
        /// <param name="globalPosition">Position of the collider (global coordinates).</param>
        /// <returns>Boolean indicating if there is a <c>ColliderObject</c> at the given position.</returns>
        public bool HasCollider(Vector3 globalPosition) {
            return this.GetCollider(globalPosition) != null;
        }
        
        /// <summary>
        /// Gets the collider (if any) at the correspondent position.
        /// If there are two or more colliders at that position, only the first one is returned.
        /// </summary>
        /// <param name="globalPosition">Position of the collider (global coordinates).</param>
        /// <returns>Collider at the correspondet position. <c>null</c> if there isn't one.</returns>
        public ColliderObject GetCollider(Vector3 globalPosition) {
            // TODO: GLOBAL POSITION NOT WORKING, REFACTOR POSITIONS IN WALKABLE
            foreach (ColliderObject worldObject in this.database) {
                if (worldObject.CollidesWith(globalPosition))
                    return worldObject;
            }
            return null;
        }

        /// <summary>
        /// Gets a collection of colliders that intersect the given bounds.
        /// </summary>
        /// <param name="bounds">Bounds of the search area.</param>
        /// <returns>Collection of colliders that intersect the given bounds.</returns>
        public ICollection<ColliderObject> GetColliders(Bounds bounds) {
            ICollection<ColliderObject> result = new LinkedList<ColliderObject>();

            foreach (ColliderObject worldObject in this.database) {
                if (worldObject.HasCollision() && worldObject.Intersects(bounds))
                    result.Add(worldObject);
            }
            return result;
        }
        #endregion
    }
}
