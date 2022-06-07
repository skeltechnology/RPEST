using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World {
    public class World : MonoBehaviour {
        #region Fields
        private Grid grid;
        private Walkable[] walkables = new Walkable[0];
        [SerializeField] private GameObject circle;
        #endregion
        
        #region Unity
        private void Awake() {
            this.grid = this.GetComponent<Grid>();
            this.InitializeWalkables();
            System.DateTime before = System.DateTime.Now;
            SkelTech.RPEST.Pathfinding.Path pa = this.walkables[0].GetPath(new Vector2Int(1, 1), new Vector2Int(-10, -3), 1000);
            System.DateTime after = System.DateTime.Now;
            System.TimeSpan duration = after.Subtract(before);
            ICollection<Vector2Int> path = pa.GetPositions();
            foreach (var p in path) {
                Debug.Log(p);
            }
            Debug.Log("Duration: " + duration.Milliseconds + "ms");
        }
        #endregion

        #region Movement
        public bool CanMoveTo(WalkableObject walkableObject, Vector3Int position) {
            Tilemap tilemap = walkableObject.GetWalkable();
            if (tilemap) {
                // TODO: IMPLEMENT PATHFINDING LOGIC
            }

            return false;
        }
        #endregion

        #region Initialization
        private void InitializeWalkables() {
            Tilemap[] childTilemaps;
            foreach (Transform child in this.transform) {
                childTilemaps = child.GetComponentsInChildren<Tilemap>();
                if (child.name.Equals("Walkables")) {
                    child.gameObject.SetActive(false);
                    this.walkables = new Walkable[childTilemaps.Length];
                    for (int i = 0; i < childTilemaps.Length; ++i) {
                        this.walkables[i] = new Walkable(childTilemaps[i]);
                    }
                    return;
                }
                // TODO: CHECK FOR WALKABLE OBJECTS
            }
            // TODO: WARNING
        }
        #endregion
    }
}
