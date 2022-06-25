namespace SkelTech.RPEST.World.Elements.Objects {
    public interface Trigger {
        // Note: World Objects that implement this interface must notify the respective database
        public WorldObject GetWorldObject();
        public void OnEnterTrigger(InteractorObject interactor);
        public void OnExitTrigger(InteractorObject interactor);
    }
}
