namespace SkelTech.RPEST.World.Objects {
    public interface Interactable {
        public WorldObject GetWorldObject();
        public bool Interact(InteractorObject interactor);
    }
}
