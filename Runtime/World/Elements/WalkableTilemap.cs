using SkelTech.RPEST.World.Elements.Objects;
using SkelTech.RPEST.Pathfinding;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World.Elements {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Tilemap))]
    public class WalkableTilemap : WorldElement {
        #region Fields
        private Tilemap tilemap;
        private Pathfinder pathfinder;
        #endregion;

        #region Unity
        private void Awake() {
            this.tilemap = this.GetComponent<Tilemap>();
            this.pathfinder = new Pathfinder(this.tilemap);
        }
        #endregion

        #region Getters
        public Tilemap GetTilemap() {
            return this.tilemap;
        }
        
        public ICollection<Vector3Int> GetObstacles() {
            ICollection<WorldObject> worldObjects = this.world.WorldObjectDatabase.GetObstacles(this.tilemap.localBounds);

            ICollection<Vector3Int> obstacles = new LinkedList<Vector3Int>();
            Vector3 center, up, down, left, right, extents;
            Vector3Int centerGridPosition, gridPosition;
            foreach (WorldObject worldObject in worldObjects) {
                extents = worldObject.GetBounds().extents;
                center = worldObject.transform.position;

                up = new Vector3(center.x, center.y + extents.y, center.z);
                down = new Vector3(center.x, center.y - extents.y, center.z);
                left = new Vector3(center.x + extents.x, center.y, center.z);
                right = new Vector3(center.x - extents.x, center.y, center.z);

                centerGridPosition = this.LocalToGrid(center);
                obstacles.Add(centerGridPosition);
                foreach (Vector3 position in new Vector3[] {up, down, left, right}) {
                    gridPosition = this.LocalToGrid(position);
                    if (!centerGridPosition.Equals(gridPosition))
                        obstacles.Add(gridPosition);
                }
            }
            return obstacles;
        }
        #endregion

        #region Operators
        public Path FindShortestPath(Vector3 localStartPosition, Vector3 localEndPosition, int maxIterations) {
            Vector3Int gridStartPosition = this.LocalToGrid(localStartPosition);
            Vector3Int gridEndPosition = this.LocalToGrid(localEndPosition);

            Path gridPath = this.pathfinder.FindShortestPath(
                gridStartPosition, 
                gridEndPosition, 
                this.GetObstacles(),
                maxIterations);
            if (gridPath == null) return null;
            return this.GridToLocal(gridPath);
        }
        #endregion

        #region Convertion
        private Vector3Int LocalToGrid(in Vector3 localPosition) {
            return Vector3Int.FloorToInt(localPosition) - this.tilemap.cellBounds.min;
        }

        private Vector3 GridToLocal(in Vector3Int gridPosition) {
            return gridPosition + this.tilemap.cellBounds.min + this.tilemap.layoutGrid.cellSize / 2;
        }

        private Path GridToLocal(in Path gridPath) {
            Path localPath = new Path();
            foreach (Vector3 gridPosition in gridPath.GetPositions()) {
                localPath.AddPosition(this.GridToLocal(Vector3Int.FloorToInt(gridPosition)));
            }
            return localPath;
        }
        #endregion
        
        #region Helpers
        public bool IsWalkable(Vector3 localPosition) {
            Vector3Int floorPosition = Vector3Int.FloorToInt(localPosition);
            bool hasTile = this.tilemap.HasTile(floorPosition);
            bool hasNoObstacle = (this.world.WorldObjectDatabase.GetObstacle(Vector3Int.FloorToInt(localPosition)) == null);
            return hasTile && hasNoObstacle;
        }

        public bool IsInsideTilemap(Vector3 localPosition) {
            Bounds bounds = this.tilemap.localBounds;
            return bounds.Contains(localPosition);
        }
        #endregion
    }
}
