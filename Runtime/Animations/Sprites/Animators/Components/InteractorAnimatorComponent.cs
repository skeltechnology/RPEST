using SkelTech.RPEST.World.Elements.Objects;

using System.Collections;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    public class InteractorAnimatorComponent : WorldObjectAnimatorComponent {
        #region Fields
        [SerializeReference] private DirectedAnimation interactionAnimation;
        [SerializeReference] protected InteractorObject interactableObject;
        [SerializeReference] private float interactionDuration = 0.5f;
        #endregion

        #region Constructors
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
        private void OnInteract(object sender, Interactable interactable) {
            if (!this.animator.IsAnimating)
                this.animator.Animate(this.InteractionAnimation());
        }

        private IEnumerator InteractionAnimation() {
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

        private void LockActions(bool isLocked) {
            this.interactableObject.SetCanMove(isLocked);
            this.interactableObject.SetCanInteract(isLocked);
        }
        #endregion
    }
}
