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
        private ICollection<Interactable> interactables;
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

        public Bounds GetBounds(Vector3 position) {
            return new Bounds(position, this.grid.cellSize * 0.99f);  // Avoid edges collision
        }

        public WorldObject GetObstacle(Vector3Int globalPosition) {
            // Gets only the first object
            // TODO: GLOBAL POSITION NOT WORKING, REFACTOR POSITIONS IN WALKABLE
            Vector3 target = globalPosition + this.grid.cellSize / 2;
            foreach (WorldObject worldObject in this.objects) {
                if (this.HasCollision(target, worldObject))
                    return worldObject;
            }
            return null;
        }

        public ICollection<WorldObject> GetObstacles(Bounds bounds) {
            ICollection<WorldObject> result = new LinkedList<WorldObject>();

            foreach (WorldObject worldObject in this.objects) {
                if (worldObject.IsObstacle() && bounds.Intersects(worldObject.GetBounds()))
                    result.Add(worldObject);
            }

            return result;
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

        #region Helpers
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
