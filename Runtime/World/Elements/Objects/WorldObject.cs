using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    [DisallowMultipleComponent]
    public class WorldObject : WorldElement {
        #region Fields
        [SerializeField] protected bool isObstacle = true;
        #endregion

        #region Getters
        public bool IsObstacle() {
            return this.gameObject.activeInHierarchy && this.isObstacle;
        }

        public Bounds GetBounds() {
            return this.world.GetBounds(this.transform.position);
        }
        #endregion

        #region Initialization
        protected override void InitializeWorldElement() {
            this.world.WorldObjectDatabase.Add(this);
            this.transform.localPosition = Vector3Int.FloorToInt(this.transform.localPosition) + this.world.GetGrid().cellSize / 2;
        }

        protected override void DisableWorldElement() {
            this.world.WorldObjectDatabase.Remove(this);
        }
        #endregion
    }
}
