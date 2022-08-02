namespace SkelTech.RPEST.World.Elements.Objects {
    /// <summary>
    /// Base interface for classes that implement an interactable behaviour.
    /// Note that World Objects that implement this interface must notify the respective database.
    /// </summary>
    public interface Interactable {
        /// <summary>
        /// Gets the world object reference.
        /// </summary>
        /// <returns>Reference to the world object.</returns>
        public WorldObject GetWorldObject();

        /// <summary>
        /// Method that will be called when an <c>InteractorObject</c> interacts with the interactable.
        /// </summary>
        /// <param name="interactor">Reference to the interactor object.</param>
        public void Interact(InteractorObject interactor);
    }
}
