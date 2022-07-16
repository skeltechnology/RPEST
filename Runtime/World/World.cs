using SkelTech.RPEST.World.Elements;
using SkelTech.RPEST.World.Database;

using UnityEngine;

namespace SkelTech.RPEST.World {
    /// <summary>
    /// Class that represents the scene world.
    /// This class is responsible for storing and initializing all the <c>WorldElements<c> contained inside its game object.
    /// </summary>
    // TODO: REQUIRES GRID
    [DisallowMultipleComponent]
    public class World : MonoBehaviour {
        #region Properties
        /// <summary>
        /// Reference to the <c>WalkableTilemapDatabase</c>.
        /// </summary>
        public WalkableTilemapDatabase WalkableTilemapDatabase { get; private set; } = new WalkableTilemapDatabase();

        /// <summary>
        /// Reference to the <c>ColliderObjectDatabase</c>.
        /// </summary>
        public ColliderObjectDatabase ColliderObjectDatabase { get; private set; } = new ColliderObjectDatabase();

        /// <summary>
        /// Reference to the <c>InteractableDatabase</c>.
        /// </summary>
        public InteractableDatabase InteractableDatabase { get; private set; } = new InteractableDatabase();

        /// <summary>
        /// Reference to the <c>TriggerDatabase</c>.
        /// </summary>
        public TriggerDatabase TriggerDatabase { get; private set; } = new TriggerDatabase();
        #endregion

        #region Fields
        /// <summary>
        /// Reference to <c>Grid</c> component.
        /// </summary>
        private Grid grid;
        #endregion
        
        #region Unity
        private void Awake() {
            this.grid = this.GetComponent<Grid>();
            this.InitializeWorld();
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the <c>Grid</c> component.
        /// </summary>
        /// <returns>Reference to the <c>Grid</c> component.</returns>
        public Grid GetGrid() {
            return this.grid;
        }

        /// <summary>
        /// Converts the given position to a world position.
        /// </summary>
        /// <param name="position">Position to be converted.</param>
        /// <returns></returns>
        public Vector3 GetWorldPosition(Vector3Int position) {
            return position + this.grid.cellSize / 2;
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the world elements.
        /// </summary>
        private void InitializeWorld() {
            foreach (WorldElement element in this.GetComponentsInChildren<WorldElement>())
                element.SetWorld(this);
        }
        #endregion
    }
}
