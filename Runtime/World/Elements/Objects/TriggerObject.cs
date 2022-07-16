namespace SkelTech.RPEST.World.Elements.Objects {
    /// <summary>
    /// Base class for a trigger object.
    /// It must be a (sub-)child of a <c>World</c> component.
    /// </summary>
    public abstract class TriggerObject : WorldObject, Trigger {
        #region Getters
        public WorldObject GetWorldObject() { return this; }
        #endregion

        #region Operators
        public abstract void OnEnterTrigger(InteractorObject interactor);
        public abstract void OnExitTrigger(InteractorObject interactor);
        #endregion

        #region Initialization
        protected override void InitializeWorldElement() {
            base.InitializeWorldElement();
            this.world.TriggerDatabase.Add(this);
        }

        protected override void DisableWorldElement() {
            this.world.TriggerDatabase.Remove(this);
            base.DisableWorldElement();
        }
        #endregion
    }
}
