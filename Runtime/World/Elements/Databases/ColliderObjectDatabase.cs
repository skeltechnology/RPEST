using SkelTech.RPEST.World.Elements.Objects;
using SkelTech.RPEST.World.Elements;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Database {
    public class ColliderObjectDatabase : WorldDatabase<ColliderObject> {
        #region Getters
        public bool HasCollider(Vector3 globalPosition) {
            return this.GetCollider(globalPosition) != null;
        }
        
        public ColliderObject GetCollider(Vector3 globalPosition) {
            // Gets only the first object
            // TODO: GLOBAL POSITION NOT WORKING, REFACTOR POSITIONS IN WALKABLE
            foreach (ColliderObject worldObject in this.database) {
                if (worldObject.CollidesWith(globalPosition))
                    return worldObject;
            }
            return null;
        }

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
