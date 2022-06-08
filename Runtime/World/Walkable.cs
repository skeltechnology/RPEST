using SkelTech.RPEST.Pathfinding;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World {
    public class Walkable {
        #region Fields
        private Tilemap tilemap;
        private Pathfinder pathfinder;
        #endregion

        #region Constructors
        public Walkable(Tilemap tilemap) {
            this.tilemap = tilemap;
            this.pathfinder = new Pathfinder(tilemap);
        }
        #endregion

        #region Getters
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
            return new Vector2Int(localPosition.y - this.tilemap.cellBounds.yMin, localPosition.x - this.tilemap.cellBounds.xMin);
        }

        private Vector2Int GridToLocal(in Vector2Int gridPosition) {
            return new Vector2Int(gridPosition.y + this.tilemap.cellBounds.xMin, gridPosition.x + this.tilemap.cellBounds.yMin);
        }
        #endregion
    }
}
