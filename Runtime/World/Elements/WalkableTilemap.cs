using SkelTech.RPEST.World.Elements.Objects;
using SkelTech.RPEST.Pathfinding;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World.Elements {
    /// <summary>
    /// Class that represents a walkable tilemap.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Tilemap))]
    public class WalkableTilemap : WorldElement {
        #region Fields
        /// <summary>
        /// Reference to the tilemap component.
        /// </summary>
        private Tilemap tilemap;

        /// <summary>
        /// Reference to the pathfinder.
        /// </summary>
        private Pathfinder pathfinder;
        #endregion;

        #region Unity
        private void Awake() {
            this.tilemap = this.GetComponent<Tilemap>();
            this.pathfinder = new Pathfinder(this.tilemap);
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the tilemap reference.
        /// </summary>
        /// <returns></returns>
        public Tilemap GetTilemap() {
            return this.tilemap;
        }
        
        /// <summary>
        /// Gets the obstacles contained inside of this tilemap.
        /// </summary>
        /// <returns>Collection of positions that ara occupied by an obstacle.</returns>
        public ICollection<Vector3Int> GetObstacles() {
            ICollection<ColliderObject> worldObjects = this.world.ColliderObjectDatabase.GetColliders(this.tilemap.localBounds);

            ICollection<Vector3Int> obstacles = new LinkedList<Vector3Int>();
            Vector3 center, up, down, left, right, extents;
            Vector3Int centerGridPosition, gridPosition;
            foreach (ColliderObject worldObject in worldObjects) {
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
        /// <summary>
        /// Finds the shortest path of the two given positions, taking into account the given obstacles.
        /// </summary>
        /// <param name="startPosition">Start position of the path (local coordinates).</param>
        /// <param name="endPosition">End position of the path (local coordinates).</param>
        /// <param name="useObstacles">Boolean indicating if obstacles should be taken into account.</param>
        /// <returns>Shortest path.</returns>
        public Path FindShortestPath(Vector3Int startPosition, Vector3Int endPosition, bool useObstacles) {
            Vector3Int gridStartPosition = this.LocalToGrid(startPosition);
            Vector3Int gridEndPosition = this.LocalToGrid(endPosition);

            Path gridPath = this.pathfinder.FindShortestPath(
                gridStartPosition, 
                gridEndPosition, 
                useObstacles ? this.GetObstacles() : null,
                1000);
            if (gridPath == null) return null;
            return this.GridToLocal(gridPath);
        }
        #endregion

        #region Convertion
        /// <summary>
        /// Converts the given position to a grid position.
        /// </summary>
        /// <param name="localPosition">Local position.</param>
        /// <returns>Correspondent grid position.</returns>
        private Vector3Int LocalToGrid(in Vector3 localPosition) {
            return this.LocalToGrid(Vector3Int.FloorToInt(localPosition));
        }

        /// <summary>
        /// Converts the given position to a grid position.
        /// </summary>
        /// <param name="localPosition">Local position.</param>
        /// <returns>Correspondent grid position.</returns>
        private Vector3Int LocalToGrid(in Vector3Int localPosition) {
            return localPosition - this.tilemap.cellBounds.min;
        }

        /// <summary>
        /// Converts the given position to a local position.
        /// </summary>
        /// <param name="gridPosition">Grid position.</param>
        /// <returns>Correspondent local position.</returns>
        private Vector3Int GridToLocal(in Vector3Int gridPosition) {
            return gridPosition + this.tilemap.cellBounds.min;
        }

        /// <summary>
        /// Converts the given path to a local position.
        /// </summary>
        /// <param name="gridPath">Path with grid positions.</param>
        /// <returns>Correspondent path with local positions.</returns>
        private Path GridToLocal(in Path gridPath) {
            Path localPath = new Path();
            foreach (Vector3 gridPosition in gridPath.GetPositions()) {
                localPath.AddPosition(this.GridToLocal(Vector3Int.FloorToInt(gridPosition)));
            }
            return localPath;
        }
        #endregion

        #region Initialization
        protected override void InitializeWorldElement() {
            this.world.WalkableTilemapDatabase.Add(this);
        }

        protected override void DisableWorldElement() {
            this.world.WalkableTilemapDatabase.Remove(this);
            base.DisableWorldElement();
        }
        #endregion
        
        #region Helpers
        /// <summary>
        /// Indicates if the given position is walkable (has a tile).
        /// </summary>
        /// <param name="worldPosition">World position.</param>
        /// <returns>Boolean indicating if the given position is walkable.</returns>
        public bool IsWalkable(Vector3 worldPosition) {
            return this.IsWalkable(Vector3Int.FloorToInt(worldPosition));
        }

        /// <summary>
        /// Indicates if the given position is walkable (has a tile).
        /// </summary>
        /// <param name="localPosition">World position.</param>
        /// <returns>Boolean indicating if the given position is walkable.</returns>
        public bool IsWalkable(Vector3Int localPosition) {
            return this.tilemap.HasTile(localPosition);
        }

        /// <summary>
        /// Indicates if the given position is contained in the tilemap bounds.
        /// </summary>
        /// <param name="localPosition">Loca position.</param>
        /// <returns>Boolean indicating if the given position is contained in the tilemap bounds.</returns>
        public bool IsInsideTilemap(Vector3 localPosition) {
            Bounds bounds = this.tilemap.localBounds;
            return bounds.Contains(localPosition);
        }
        #endregion
    }
}
