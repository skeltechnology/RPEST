using SkelTech.RPEST.World.Objects;
using SkelTech.RPEST.Pathfinding;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Tilemap))]
    public class WalkableTilemap : MonoBehaviour {
        #region Fields
        private World world;
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
        public ICollection<Vector3Int> GetObstacles() {
            ICollection<WorldObject> worldObjects = this.world.GetObjects(this.tilemap.localBounds);

            ICollection<Vector3Int> obstacles = new LinkedList<Vector3Int>();
            foreach (WorldObject worldObject in worldObjects) {
                obstacles.Add(this.LocalToGrid(worldObject.transform.position));
            }
            return obstacles;
        }
        #endregion

        #region Setters
        public void SetWorld(World world) {
            this.world = world;
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
            bool hasNoObstacle = (this.world.GetObject(Vector3Int.FloorToInt(localPosition)) == null);
            return hasTile && hasNoObstacle;
        }

        public bool IsInsideTilemap(Vector3 localPosition) {
            Bounds bounds = this.tilemap.localBounds;
            return bounds.Contains(localPosition);
        }
        #endregion
    }
}
