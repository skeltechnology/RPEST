using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World {
    public class World : MonoBehaviour {
        #region Fields
        private Grid grid;
        private Tilemap[] walkables = new Tilemap[0];
        private WalkableObject[] characters = new WalkableObject[0];
        // TODO: CHARACTERS, OBSTACLES, ...
        #endregion
        
        #region Unity
        private void Awake() {
            this.grid = this.GetComponent<Grid>();
            this.InitializeWalkables();
            System.DateTime before = System.DateTime.Now;
            SkelTech.RPEST.Pathfinding.Path pa = this.characters[0].FindShortestPath(new Vector2Int(1, 1), new Vector2Int(-10, -3), 1000);
            System.DateTime after = System.DateTime.Now;
            System.TimeSpan duration = after.Subtract(before);
            ICollection<Vector2Int> path = pa.GetPositions();
            foreach (var p in path) {
                Debug.Log(p);
            }
            Debug.Log("Duration: " + duration.Milliseconds + "ms");
        }
        #endregion

        #region Initialization
        private void InitializeWalkables() {
            // TODO: IN THE FUTURE, A DEPTH SEARCH CAN BE PERFORMED TO RETREIVE DATA, ALLOWING THE USER TO HAVE IT'S OWN STRUCTURE
            foreach (Transform child in this.transform) {
                if (child.name.Equals("Walkables")) {
                    child.gameObject.SetActive(false);
                    this.walkables = child.GetComponentsInChildren<Tilemap>();
                } else if (child.name.Equals("Characters")) {
                    child.gameObject.SetActive(false);
                    this.characters = child.GetComponentsInChildren<WalkableObject>();
                }
            }
        }
        #endregion
    }
}
