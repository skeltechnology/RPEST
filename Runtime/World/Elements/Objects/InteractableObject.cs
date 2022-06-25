namespace SkelTech.RPEST.World.Elements.Objects {
    public abstract class InteractableObject : ColliderObject, Interactable {
        #region Getters
        public WorldObject GetWorldObject() {
            return this;
        }
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
