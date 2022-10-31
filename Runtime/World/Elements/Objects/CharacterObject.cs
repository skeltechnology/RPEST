using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    /// <summary>
    /// <c>MonoBehaviour</c> that represents a character object.
    /// It must be a (sub-)child of a <c>World</c> component.
    /// </summary>
    public abstract class CharacterObject<T> : InteractorObject, Interactable {
        #region Properties
        /// <summary>
        /// Reference to the character data.
        /// </summary>
        public T Data { get { return this.data; } set { this.data = value; }}
        #endregion

        #region Fields
        /// <summary>
        /// Reference to the character data.
        /// </summary>
        /// <returns></returns>
        [SerializeField] private T data;
        #endregion

        #region Getters
        public WorldObject GetWorldObject() { return this; }
        #endregion

        #region Operators
        public abstract void Interact(InteractorObject interactor);
        #endregion

        #region Initialization
        protected override void InitializeWorldElement() {
            base.InitializeWorldElement();
            this.world.InteractableDatabase.Add(this);
        }

        protected override void DisableWorldElement() {
            this.world.InteractableDatabase.Remove(this);
            base.DisableWorldElement();
        }
        #endregion
    }
}
