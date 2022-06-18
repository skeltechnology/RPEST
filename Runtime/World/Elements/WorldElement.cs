using UnityEngine;

namespace SkelTech.RPEST.World.Elements {
    public class WorldElement : MonoBehaviour {
        #region Fields
        protected World world;
        #endregion;

        #region Setters
        public virtual void SetWorld(World world) {
            this.world = world;
        }
        #endregion
    }
}
