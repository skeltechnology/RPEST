using UnityEngine;

namespace SkelTech.RPEST.World.Elements {
    public class WorldElement : MonoBehaviour {
        #region Fields
        protected World world;
        #endregion;

        #region Unity
        protected virtual void OnDestroy() {
            if (this.world) this.DisableWorldElement();
        }
        #endregion

        #region Setters
        public void SetWorld(World world) {
            if (this.world) this.DisableWorldElement();
            this.world = world;
            this.InitializeWorldElement();
        }
        #endregion

        #region Initialization
        protected virtual void InitializeWorldElement() {}

        protected virtual void DisableWorldElement() {}
        #endregion
    }
}
