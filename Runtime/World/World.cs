using SkelTech.RPEST.World.Elements;
using SkelTech.RPEST.World.Database;

using UnityEngine;

namespace SkelTech.RPEST.World {
    [DisallowMultipleComponent]
    public class World : MonoBehaviour {
        #region Properties
        public WalkableTilemapDatabase WalkableTilemapDatabase { get; private set; } = new WalkableTilemapDatabase();
        public WorldObjectDatabase WorldObjectDatabase { get; private set; } = new WorldObjectDatabase();
        public InteractableDatabase InteractableDatabase { get; private set; } = new InteractableDatabase();
        #endregion

        #region Fields
        private Grid grid;
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

        public Vector3 GetWorldPosition(Vector3Int position) {
            return position + this.grid.cellSize / 2;
        }
        #endregion

        #region Initialization
        private void InitializeWorld() {
            foreach (WorldElement element in this.GetComponentsInChildren<WorldElement>())
                element.SetWorld(this);
        }
        #endregion
    }
}
