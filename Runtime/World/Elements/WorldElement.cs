using UnityEngine;

namespace SkelTech.RPEST.World.Elements {
    /// <summary>
    /// <c>MonoBehaviour</c> that represents a world element.
    /// In order to work properly, it must be a (sub-)child of a game object that has a <c>World</c> component.
    /// </summary>
    public class WorldElement : MonoBehaviour {
        #region Fields
        /// <summary>
        /// Reference to the world component.
        /// </summary>
        protected World world;
        #endregion;

        #region Unity
        protected virtual void OnDestroy() {
            if (this.world) {
                this.DisableWorldElement();
                this.world = null;
            }
        }
        #endregion

        #region Setters
        /// <summary>
        /// Sets the world reference to the given one and permorms the corresponding disable and initialize operations.
        /// </summary>
        /// <param name="world">World reference.</param>
        public void SetWorld(World world) {
            if (this.world) this.DisableWorldElement();
            this.world = world;
            this.InitializeWorldElement();
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Method responsible for initializing this class.
        /// This method is called after setting a new world reference.
        /// </summary>
        protected virtual void InitializeWorldElement() {}

        /// <summary>
        /// Method responsible for disabling this class.
        /// This method is called before setting a new world reference and is only called if a world reference was setted before.
        /// </summary>
        protected virtual void DisableWorldElement() {}
        #endregion
    }
}
