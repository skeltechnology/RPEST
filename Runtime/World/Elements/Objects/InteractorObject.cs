using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    public class InteractorObject : WalkableObject {
        #region Fields
        [SerializeField] private string interactorId;
        [SerializeField] private bool canInteract = true, canTrigger = true;
        #endregion

        #region Getters
        public string GetInteractorId() {
            return this.interactorId;
        }
        #endregion

        #region Operators
        protected override void OnFinishedMovement() {
            this.Trigger(this.transform.position);
        }

        public void Interact() {
            if (this.canInteract) {
                Vector3 interactablePosition = this.transform.position + this.lastDirection;
                this.Interact(interactablePosition);
            }
        }

        public void Interact(Vector3 interactablePosition) {
            if (this.canInteract) {
                Interactable interactable = this.world.InteractableDatabase.GetInteractable(interactablePosition);
                this.Interact(interactable);
            }
        }

        private void Interact(Interactable interactable) {
            // Check if there's an interactable in the next cell
            if (interactable != null) {
                interactable.Interact(this);
            }
        }
        #endregion

        #region Helpers
        private void Trigger(Vector3 triggerPosition) {
            if (this.canTrigger) {
                Interactable interactable = this.world.TriggerDatabase.GetInteractable(triggerPosition);
                this.Interact(interactable);
            }
        }
        #endregion
    }
}
