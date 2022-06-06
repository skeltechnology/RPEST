using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World {
    public class WalkableObject : MonoBehaviour {
        #region Fields
        [SerializeField] private Tilemap walkable;
        private World world;
        #endregion

        #region Unity
        private void Awake() {
            this.world = GameObject.Find("World").GetComponent<World>();
        }
        #endregion

        #region Getters
        public Tilemap GetWalkable() {
            return this.walkable;
        }
        #endregion
    }
}
