using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace SkelTech.RPEST.World {
    [RequireComponent(typeof(Tilemap))]
    public class WalkableTilemap : MonoBehaviour {
        #region Fields
        private Tilemap tilemap;
        #endregion;

        #region Unity
        private void Awake() {
            this.tilemap = this.GetComponent<Tilemap>();
        }
        #endregion

        #region Getters
        public Tilemap GetTilemap() {
            return this.tilemap;
        }
        #endregion
    }
}
