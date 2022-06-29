using System;

using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    public class InteractorObject : WalkableObject {
        #region Events
        public event EventHandler<Interactable> OnInteract;
        #endregion

        #region Fields
        [SerializeField] private string interactorId;
        [SerializeField] private bool canInteract = true, canTrigger = true;
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();
            this.OnStartedMovement += this.OnStartedMovementHandler;
            this.OnFinishedMovement += this.OnFinishedMovementHandler;
        }
        #endregion

        #region Getters
        public string GetInteractorId() {
            return this.interactorId;
        }
        #endregion

        #region Operators
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
                this.OnInteract.Invoke(this, interactable);
            }
        }

        private void Trigger(Vector3 triggerPosition, bool onEnter) {
            if (this.canTrigger) {
                Trigger trigger = this.world.TriggerDatabase.GetTrigger(triggerPosition);
                this.Trigger(trigger, onEnter);
            }
        }

        private void Trigger(Trigger trigger, bool onEnter) {
            if (trigger != null) {
                if (onEnter) trigger.OnEnterTrigger(this);
                else trigger.OnExitTrigger(this);
            }
        }
        #endregion

        #region Helpers
        private void OnStartedMovementHandler(object sender, System.EventArgs e) {
            this.Trigger(this.transform.position, false);
        }

        protected void OnFinishedMovementHandler(object sender, System.EventArgs e) {
            this.Trigger(this.transform.position, true);
        }
        #endregion
    }
}
