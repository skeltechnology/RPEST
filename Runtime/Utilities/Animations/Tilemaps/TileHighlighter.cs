using SkelTech.RPEST.World;
using SkelTech.RPEST.World.Objects;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.Utilities.Tilemaps {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Tilemap))]
    public class TileHighlighter : MonoBehaviour {
        #region Fields
        [SerializeField] private new Camera camera;
        [SerializeField] private WalkableObject walkableObject;
        [SerializeField] private Tile highlightTile;

        private Tilemap highlighterTilemap;
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
                if (walkableTilemap.IsWalkable(mousePosition)) {
                    this.highlighterTilemap.SetTile(mousePosition, this.highlightTile);  // Add new highlight
                }
                previousMousePosition = mousePosition;
            }
        }
        #endregion

        #region Helpers
        private static Vector3Int GetMousePosition(Tilemap tilemap, Camera camera) {
            return tilemap.WorldToCell(camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition));
        }
        #endregion
    }
}
