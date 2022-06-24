namespace SkelTech.RPEST.World.Elements.Objects {
    public interface Interactable {
        // Note: World Objects that implement this interface must notify the respective database
        public WorldObject GetWorldObject();
        public void Interact(InteractorObject interactor);
    }
}
