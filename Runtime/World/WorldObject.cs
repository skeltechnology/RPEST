using UnityEngine;

namespace SkelTech.RPEST.World {
    public class WorldObject : MonoBehaviour {

        #region Fields
        protected World world;
        #endregion

        #region Unity
        protected virtual void Awake() {
            // TODO: IN FUTURE, HAVE SETTER AND WORLD WOULD PERFORM DEPTH SEARCH
            this.world = GameObject.Find("World").GetComponent<World>();
        }

        protected virtual void Start() {
            this.transform.localPosition = Vector3Int.FloorToInt(this.transform.localPosition) + this.world.GetGrid().cellSize / 2;
        }
        #endregion
    }
}
