using System;

using UnityEngine;

namespace SkelTech.RPEST.World.Elements.Objects {
    /// <summary>
    /// <c>MonoBehaviour</c> that represents a walkable object.
    /// It must be a (sub-)child of a <c>World</c> component.
    /// </summary>
    public class InteractorObject : WalkableObject {
        #region Events
        /// <summary>
        /// Called when this <c>InteractorObject</c> interacts with an interactable.
        /// </summary>
        public event EventHandler<Interactable> OnInteract;
        
        /// <summary>
        /// Called when this <c>InteractorObject</c> interacts with a trigger.
        /// </summary>
        public event EventHandler<Trigger> OnTrigger;
        #endregion

        #region Fields
        /// <summary>
        /// Id of the interactor object.
        /// It must be unique, so the the other scripts can easily identify the interactor.
        /// </summary>
        [SerializeField] private string interactorId;

        /// <summary>
        /// Indicates if the interactor can currently interact with <c>Interactable</c>s.
        /// </summary>
        [SerializeField] private bool canInteract = true;

        /// <summary>
        /// Indicates if the interactor can currently interact with <c>Trigger</c>s.
        /// </summary>
        [SerializeField] private bool canTrigger = true;
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();
            this.OnStartedMovement += this.OnStartedMovementHandler;
            this.OnFinishedMovement += this.OnFinishedMovementHandler;
        }

        protected override void OnDestroy() {
            this.OnStartedMovement -= this.OnStartedMovementHandler;
            this.OnFinishedMovement -= this.OnFinishedMovementHandler;
            base.OnDestroy();
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the id of the interactor object.
        /// </summary>
        /// <returns>Id of the interactor object.</returns>
        public string GetInteractorId() {
            return this.interactorId;
        }
        #endregion

        #region Setters
        /// <summary>
        /// Changes if the object can interact with <c>Interactable</c>s.
        /// </summary>
        /// <param name="canInteract">Boolean indicating if the object can interact with <c>Interactable</c>s.</param>
        public void SetCanInteract(bool canInteract) {
            this.canInteract = canInteract;
        }

        /// <summary>
        /// Changes if the object can interact with <c>Trigger</c>s.
        /// </summary>
        /// <param name="canTrigger">Boolean indicating if the object can interact with <c>Trigger</c>s.</param>
        public void SetCanTrigger(bool canTrigger) {
            this.canTrigger = canTrigger;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Interacts with the interactable in front of the object.
        /// </summary>
        public void Interact() {
            // TODO: ALSO INTERACT IN THE CURRENT POSITION
            if (this.CanInteract()) {
                Vector3 interactablePosition = this.transform.position + this.lastDirection;
                this.Interact(interactablePosition);
            }
        }

        /// <summary>
        /// Interacts with the interactable in the given position.
        /// </summary>
        /// <param name="interactablePosition">Interactable position.</param>
        public void Interact(Vector3 interactablePosition) {
            if (this.CanInteract()) {
                Interactable interactable = this.world.InteractableDatabase.GetInteractable(interactablePosition);
                this.Interact(interactable);
            }
        }

        /// <summary>
        /// Interacts with the given interactable.
        /// </summary>
        /// <param name="interactable">Interactable.</param>
        private void Interact(Interactable interactable) {
            // Check if there's an interactable in the next cell
            if (interactable != null && this.CanInteract()) {
                interactable.Interact(this);
                this.OnInteract?.Invoke(this, interactable);
            }
        }

        /// <summary>
        /// Interacts with the trigger in the given position.
        /// </summary>
        /// <param name="triggerPosition">Trigger position.</param>
        /// <param name="onEnter">Boolean indicating if the object is entering the trigger.</param>
        private void Trigger(Vector3 triggerPosition, bool onEnter) {
            if (this.canTrigger) {
                Trigger trigger = this.world.TriggerDatabase.GetTrigger(triggerPosition);
                this.Trigger(trigger, onEnter);
            }
        }

        /// <summary>
        /// Interacts with the given trigger.
        /// </summary>
        /// <param name="trigger">Trigger.</param>
        /// <param name="onEnter">Boolean indicating if the object is entering the trigger.</param>
        private void Trigger(Trigger trigger, bool onEnter) {
            if (trigger != null) {
                if (onEnter) trigger.OnEnterTrigger(this);
                else trigger.OnExitTrigger(this);
                this.OnTrigger?.Invoke(this, trigger);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Helper callback to handle OnStartedMovement.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="e">Arguments.</param>
        private void OnStartedMovementHandler(object sender, System.EventArgs e) {
            this.Trigger(this.transform.position, false);
        }

        /// <summary>
        /// Helper callback to handle OnFinishedMovement.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="e">Arguments.</param>
        protected void OnFinishedMovementHandler(object sender, System.EventArgs e) {
            this.Trigger(this.transform.position, true);
        }

        /// <summary>
        /// Indicates if the object can currently interact with <c>Interactable</c>s.
        /// </summary>
        /// <returns>Boolean indicating if the object can currently interact with <c>Interactable</c>s.</returns>
        private bool CanInteract() {
            return this.canInteract && !this.IsMoving;
        }
        #endregion
    }
}
