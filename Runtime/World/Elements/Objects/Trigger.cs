namespace SkelTech.RPEST.World.Elements.Objects {
    /// <summary>
    /// Base interface for classes that implement a trigger behaviour.
    /// Note that World Objects that implement this interface must notify the respective database.
    /// </summary>
    public interface Trigger {
        /// <summary>
        /// Gets the world object reference.
        /// </summary>
        /// <returns>Reference to the world object.</returns>
        public WorldObject GetWorldObject();

        /// <summary>
        /// Method that will be called when an <c>InteractorObject</c> arrives to the trigger.
        /// </summary>
        /// <param name="interactor">Reference to the interactor object.</param>
        public void OnEnterTrigger(InteractorObject interactor);

        /// <summary>
        /// Method that will be called when an <c>InteractorObject</c> leaves the trigger.
        /// </summary>
        /// <param name="interactor">Reference to the interactor object.</param>
        public void OnExitTrigger(InteractorObject interactor);
    }
}
