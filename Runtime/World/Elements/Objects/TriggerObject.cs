namespace SkelTech.RPEST.World.Elements.Objects {
    public abstract class TriggerObject : WorldObject, Trigger {
        #region Getters
        public WorldObject GetWorldObject() {
            return this;
        }
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
