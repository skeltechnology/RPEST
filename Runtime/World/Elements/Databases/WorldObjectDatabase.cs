using SkelTech.RPEST.World.Elements.Objects;
using SkelTech.RPEST.World.Elements;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Database {
    public class WorldObjectDatabase : WorldDatabase<WorldObject> {
        #region Getters
            public WorldObject GetObstacle(Vector3 globalPosition) {
                // Gets only the first object
                // TODO: GLOBAL POSITION NOT WORKING, REFACTOR POSITIONS IN WALKABLE
                foreach (WorldObject worldObject in this.database) {
                    if (worldObject.CollidesWith(globalPosition))
                        return worldObject;
                }
                return null;
            }

            public ICollection<WorldObject> GetObstacles(Bounds bounds) {
                ICollection<WorldObject> result = new LinkedList<WorldObject>();

                foreach (WorldObject worldObject in this.database) {
                    if (worldObject.CollidesWith(bounds))
                        result.Add(worldObject);
                }

                return result;
            }
            #endregion
    }
}
