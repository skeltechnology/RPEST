using SkelTech.RPEST.World.Elements.Objects;

using System.Collections;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    /// <summary>
    /// Component responsible for animating an <c>InteractableObject</c>.
    /// </summary>
    public class InteractorAnimatorComponent : WorldObjectAnimatorComponent {
        #region Fields
        /// <summary>
        /// Interactor object that the animator component will be listening to.
        /// </summary>
        [SerializeReference] protected InteractorObject interactableObject;

        /// <summary>
        /// Animation that will be played when the interactor object interacts with an interactable.
        /// </summary>
        [SerializeReference] private DirectedAnimation interactionAnimation;

        /// <summary>
        /// Duration, in seconds, of the interaction animation.
        /// </summary>
        [SerializeReference] private float interactionDuration = 0.5f;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the interactor animator component.
        /// </summary>
        /// <param name="animator">Animator that manages this component.</param>
        public InteractorAnimatorComponent(WorldObjectAnimator animator) : base(animator) {}
        #endregion

        #region Initialization
        public override void Initialize() {
            this.interactableObject.OnInteract += this.OnInteract;
        }
        public override void Disable() {
            this.interactableObject.OnInteract -= this.OnInteract;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Callback responsible for starting the interaction animation.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="interactable">Interactable that the interactor object is interacting with.</param>
        protected virtual void OnInteract(object sender, Interactable interactable) {
            if (!this.animator.IsAnimating)
                this.animator.Animate(this.InteractionAnimation());
        }

        /// <summary>
        /// Coroutine that plays the interaction animation.
        /// </summary>
        protected IEnumerator InteractionAnimation() {
            // TODO: THIS COULD BE IN BASE CLASS
            this.LockActions(false);
            this.animator.PushSprite();

            SpriteAnimation animation = this.interactionAnimation.GetAnimation(this.interactableObject.GetCurrentDirection());
            this.animator.SetAnimation(animation);

            float time = 0;
            while (time < this.interactionDuration) {
                time += Time.deltaTime;
                this.animator.UpdateSprite(time / this.interactionDuration);
                yield return null;
            }

            this.animator.LoadSpriteFromStack();
            this.animator.PopSprite();
            this.LockActions(true);
        }

        /// <summary>
        /// Method responsible for locking / unlocking the actions of the object.
        /// </summary>
        /// <param name="isLocked"></param>
        private void LockActions(bool isLocked) {
            this.interactableObject.SetCanMove(isLocked);
            this.interactableObject.SetCanInteract(isLocked);
        }
        #endregion
    }
}
