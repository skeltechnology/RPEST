using SkelTech.RPEST.Pathfinding;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World {
    public class WalkableObject : MonoBehaviour {
        #region Fields
        [SerializeField] private Tilemap walkable;

        private World world;
        private Pathfinder pathfinder;
        #endregion

        #region Unity
        private void Awake() {
            this.world = GameObject.Find("World").GetComponent<World>();
            if (this.world) this.pathfinder = new Pathfinder(this.walkable);
        }
        #endregion

        #region Getters
        public Tilemap GetWalkable() {
            return this.walkable;
        }
        #endregion

        #region Operators
        public Path FindShortestPath(Vector2Int localStartPosition, Vector2Int localEndPosition, int maxIterations) {
            Vector2Int gridStartPosition = this.LocalToGrid(localStartPosition);
            Vector2Int gridEndPosition = this.LocalToGrid(localEndPosition);

            Path gridPath = this.pathfinder.FindShortestPath(gridStartPosition, gridEndPosition, maxIterations);
            Path localPath = new Path();
            foreach (Vector2Int gridPosition in gridPath.GetPositions()) {
                localPath.AddPosition(this.GridToLocal(gridPosition));
            }
            return localPath;
        }
        #endregion

        #region Convertion
        private Vector2Int LocalToGrid(in Vector2Int localPosition) {
            return new Vector2Int(localPosition.y - this.walkable.cellBounds.yMin, localPosition.x - this.walkable.cellBounds.xMin);
        }

        private Vector2Int GridToLocal(in Vector2Int gridPosition) {
            return new Vector2Int(gridPosition.y + this.walkable.cellBounds.xMin, gridPosition.x + this.walkable.cellBounds.yMin);
        }
        #endregion
    }
}
