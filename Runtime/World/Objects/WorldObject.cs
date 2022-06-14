using UnityEngine;

namespace SkelTech.RPEST.World.Objects {
    [DisallowMultipleComponent]
    public class WorldObject : MonoBehaviour {
        #region Fields
        [SerializeField] protected bool isObstacle = true;

        protected World world;
        #endregion

        #region Unity
        protected virtual void Awake() {}

        protected virtual void Start() {
            this.transform.localPosition = Vector3Int.FloorToInt(this.transform.localPosition) + this.world.GetGrid().cellSize / 2;
        }

        protected virtual void OnDestroy() {
            this.world.RemoveObject(this);
        }
        #endregion

        #region Getters
        public bool IsObstacle() {
            return this.gameObject.activeInHierarchy && this.isObstacle;
        }
        #endregion

        #region Setters
        public void SetWorld(World world) {
            this.world = world;
        }
        #endregion
    }
}
