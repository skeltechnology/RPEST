using SkelTech.RPEST.World.Objects;

using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.World {
    [DisallowMultipleComponent]
    public class World : MonoBehaviour {
        #region Fields
        private Grid grid;
        private ICollection<WalkableTilemap> walkableTilemaps;
        private ICollection<WorldObject> objects;
        #endregion
        
        #region Unity
        private void Awake() {
            this.grid = this.GetComponent<Grid>();
            this.InitializeWorld();
        }
        #endregion

        #region Getters
        public Grid GetGrid() {
            return this.grid;
        }
        
        public ICollection<WorldObject> GetWalkableObjects() {
            return this.objects;
        }

        public WorldObject GetObject(Vector3Int globalPosition) {
            // Gets only the first object
            // TODO: CHECK FOR MOVING OBJECTS
            // TODO: GLOBAL POSITION NOT WORKING, REFACTOR POSITIONS IN WALKABLE
            float epsilon = this.grid.cellSize.x / 2, sqrMagnitude;
            Vector3 target = globalPosition + this.grid.cellSize / 2;
            foreach (WorldObject worldObject in this.objects) {
                if (worldObject.gameObject.activeSelf && worldObject.IsObstacle()) {
                    sqrMagnitude = (target - worldObject.transform.position).sqrMagnitude;
                    if (sqrMagnitude < epsilon)
                        return worldObject;
                }
            }
            return null;
        }
        #endregion

        #region Setters
        public void AddObject(WorldObject worldObject) {
            this.objects.Add(worldObject);
        }

        public void RemoveObject(WorldObject worldObject) {
            this.objects.Remove(worldObject);
        }
        #endregion

        #region Initialization
        private void InitializeWorld() {
            this.InitializeWalkableTilemaps();
            this.InitializeWorldObjects();
        }

        private void InitializeWalkableTilemaps() {
            this.walkableTilemaps = new LinkedList<WalkableTilemap>(this.GetComponentsInChildren<WalkableTilemap>());
            foreach (WalkableTilemap walkableTilemap in this.walkableTilemaps) {
                walkableTilemap.SetWorld(this);
                walkableTilemap.gameObject.SetActive(false);
            }
        }

        private void InitializeWorldObjects() {
            this.objects = new LinkedList<WorldObject>(this.GetComponentsInChildren<WorldObject>());
            foreach (WorldObject worldObject in this.objects)
                worldObject.SetWorld(this);
        }
        #endregion
    }
}
