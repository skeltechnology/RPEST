using SkelTech.RPEST.World.Elements.Objects;
using SkelTech.RPEST.Pathfinding;

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
        private Vector3Int LocalToGrid(in Vector3 localPosition) {
            return this.LocalToGrid(Vector3Int.FloorToInt(localPosition));
        }

        private Vector3Int LocalToGrid(in Vector3Int localPosition) {
            return localPosition - this.tilemap.cellBounds.min;
        }

        private Vector3Int GridToLocal(in Vector3Int gridPosition) {
            return gridPosition + this.tilemap.cellBounds.min;
        }

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
        public bool IsWalkable(Vector3 worldPosition) {
            return this.IsWalkable(Vector3Int.FloorToInt(worldPosition));
        }

        public bool IsWalkable(Vector3Int localPosition) {
            return this.tilemap.HasTile(localPosition);
        }

        public bool IsInsideTilemap(Vector3 localPosition) {
            Bounds bounds = this.tilemap.localBounds;
            return bounds.Contains(localPosition);
        }
        #endregion
    }
}
