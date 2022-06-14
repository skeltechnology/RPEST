using SkelTech.RPEST.World.Objects;

using UnityEngine;

namespace SkelTech.RPEST.World {
    [DisallowMultipleComponent]
    public class World : MonoBehaviour {
        #region Fields
        private Grid grid;
        private WalkableTilemap[] walkableTilemaps;
        private WalkableObject[] walkableObjects;
        // TODO: CHARACTERS, OBSTACLES, ...
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
        
        public WalkableObject[] GetWalkableObjects() {
            return this.walkableObjects;
        }
        #endregion

        #region Initialization
        private void InitializeWorld() {
            this.InitializeWalkableTilemaps();
            this.InitializeWalkableObjects();
        }

        private void InitializeWalkableTilemaps() {
            this.walkableTilemaps = this.GetComponentsInChildren<WalkableTilemap>();
            foreach (WalkableTilemap walkableTilemap in walkableTilemaps)
                walkableTilemap.gameObject.SetActive(false);
        }

        private void InitializeWalkableObjects() {
            this.walkableObjects = this.GetComponentsInChildren<WalkableObject>();
            Debug.Log(this.walkableObjects.Length);
        }
        #endregion
    }
}
