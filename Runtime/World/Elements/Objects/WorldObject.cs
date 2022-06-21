using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    [DisallowMultipleComponent]
    public class WorldObject : WorldElement {
        #region Convertion
        public Vector3 LocalToWorld(Vector3 position) {
            return Vector3Int.FloorToInt(position) + this.world.GetGrid().cellSize / 2;
        }
        #endregion

        #region Initialization
        protected override void InitializeWorldElement() {
            this.transform.localPosition = this.LocalToWorld(this.transform.localPosition);
        }
        #endregion
    }
}
