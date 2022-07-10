using SkelTech.RPEST.World.Elements;
using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.Animations.Tilemaps {
    /// <summary>
    /// <c>MonoBehaviour</c> that is responsible for highlighting tiles, according to the given parameters.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Tilemap))]
    public class TileHighlighter : MonoBehaviour {
        #region Fields
        /// <summary>
        /// Reference to the camera component.
        /// </summary>
        [SerializeField] private new Camera camera;

        /// <summary>
        /// Reference to the walkable object.
        /// </summary>
        [SerializeField] private WalkableObject walkableObject;

        /// <summary>
        /// Reference to the highlighted tile prefab.
        /// </summary>
        [SerializeField] private Tile highlightTile;

        /// <summary>
        /// Reference to the tilemap that will have its tiles highlighted.
        /// </summary>
        private Tilemap highlighterTilemap;

        /// <summary>
        /// Mouse position of the previous frame.
        /// </summary>
        private Vector3Int previousMousePosition = new Vector3Int();
        #endregion

        #region Unity
        private void Awake() {
            this.highlighterTilemap = this.GetComponent<Tilemap>();
        }

        private void Update() {
            WalkableTilemap walkableTilemap = this.walkableObject.GetWalkableTilemap();
            Tilemap tilemap = walkableTilemap.GetTilemap();
            Vector3Int mousePosition = GetMousePosition(tilemap, this.camera);

            if (!mousePosition.Equals(this.previousMousePosition)) {
                this.highlighterTilemap.SetTile(previousMousePosition, null);  // Remove old highlight
                if (this.walkableObject.CanMoveTo(mousePosition)) {
                    this.highlighterTilemap.SetTile(mousePosition, this.highlightTile);  // Add new highlight
                }
                previousMousePosition = mousePosition;
            }
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the mouse position according to the camera and tilemap.
        /// </summary>
        /// <param name="tilemap">Tilemap</param>
        /// <param name="camera">Camera</param>
        /// <returns></returns>
        private static Vector3Int GetMousePosition(Tilemap tilemap, Camera camera) {
            return tilemap.WorldToCell(camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition));
        }
        #endregion
    }
}
