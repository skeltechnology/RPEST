using SkelTech.RPEST.World.Elements.Objects;

using System.Collections;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    public class InteractorAnimator : WalkableAnimator {
        #region Fields
        [SerializeField] private DirectedAnimation interactionAnimation;
        [SerializeField] protected InteractorObject interactableObject;  // TODO: HAVE ONLY ONE WORLD OBJECT REFERENCE
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();
            this.interactableObject.OnInteract += this.OnInteract;
        }
        #endregion

        #region Helpers
        private void OnInteract(object sender, Interactable interactable) {
            StartCoroutine(this.InteractionAnimation(0.5f));
        }

        private IEnumerator InteractionAnimation(float totalTime) {
            this.LockActions(false);
            this.PushSprite();

            SpriteAnimation animation = this.interactionAnimation.GetAnimation(this.interactableObject.GetCurrentDirection());
            this.SetAnimation(animation);

            float time = 0;
            while (time < totalTime) {
                time += Time.deltaTime;
                this.UpdateSprite(time / totalTime);
                yield return null;
            }

            this.LoadSpriteFromStack();
            this.PopSprite();
            this.LockActions(true);
        }

        private void LockActions(bool isLocked) {
            this.interactableObject.SetCanMove(isLocked);
            this.interactableObject.SetCanInteract(isLocked);
        }
        #endregion
    }
}
