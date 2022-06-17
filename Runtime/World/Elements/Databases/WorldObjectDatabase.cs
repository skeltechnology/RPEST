using SkelTech.RPEST.World.Elements.Objects;
using SkelTech.RPEST.World.Elements;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World.Database {
    public class WorldObjectDatabase : WorldDatabase<WorldObject> {
        #region Getters
            public WorldObject GetObstacle(Vector3Int globalPosition) {
                // Gets only the first object
                // TODO: GLOBAL POSITION NOT WORKING, REFACTOR POSITIONS IN WALKABLE
                //TODO:Vector3 target = globalPosition + this.grid.cellSize / 2;
                Vector3 target = globalPosition + new Vector3(1, 1, 0) / 2;
                foreach (WorldObject worldObject in this.database) {
                    if (this.HasCollision(target, worldObject))
                        return worldObject;
                }
                return null;
            }

            public ICollection<WorldObject> GetObstacles(Bounds bounds) {
                ICollection<WorldObject> result = new LinkedList<WorldObject>();

                foreach (WorldObject worldObject in this.database) {
                    if (worldObject.IsObstacle() && bounds.Intersects(worldObject.GetBounds()))
                        result.Add(worldObject);
                }

                return result;
            }

            // TODO: MOVE FUNCTION
            public Bounds GetBounds(Vector3 position) {
                return new Bounds(position, new Vector3(1, 1, 0) * 0.99f);  // Avoid edges collision
            }
            #endregion

            #region Helpers
            // TODO: MOVE TO UTILS FILE
            private bool HasCollision(Vector3 position, WorldObject worldObject) {
                if (worldObject.IsObstacle()) {
                    Bounds positionBounds = this.GetBounds(position);
                    Bounds worldObjectBounds = worldObject.GetBounds();
                    return positionBounds.Intersects(worldObjectBounds);
                }
                return false;
            }
            #endregion
    }
}
