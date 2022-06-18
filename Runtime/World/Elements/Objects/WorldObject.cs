using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    [DisallowMultipleComponent]
    public class WorldObject : WorldElement {
        #region Fields
        [SerializeField] protected bool isObstacle = true;
        #endregion

        #region Unity
        protected virtual void Awake() {}

        protected virtual void Start() {}

        protected virtual void OnDestroy() {
            this.world.WorldObjectDatabase.Remove(this);
        }
        #endregion

        #region Getters
        public bool IsObstacle() {
            return this.gameObject.activeInHierarchy && this.isObstacle;
        }

        public Bounds GetBounds() {
            return this.world.GetBounds(this.transform.position);
        }
        #endregion

        #region Setters
        public override void SetWorld(World world) {
            this.world?.WorldObjectDatabase.Remove(this);
            base.SetWorld(world);
            this.world.WorldObjectDatabase.Add(this);
            this.transform.localPosition = Vector3Int.FloorToInt(this.transform.localPosition) + this.world.GetGrid().cellSize / 2;
        }
        #endregion
    }
}
