namespace SkelTech.RPEST.World.Elements.Objects {
    public interface Interactable {
        public WorldObject GetWorldObject();
        public bool Interact(InteractorObject interactor);
    }
}
