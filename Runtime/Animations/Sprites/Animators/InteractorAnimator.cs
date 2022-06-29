using SkelTech.RPEST.World.Elements.Objects;

using System.Collections;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    public class InteractorAnimator : WalkableAnimator {
        #region Fields
        [SerializeField] private DirectedAnimation interactionAnimation;
        [SerializeField] protected InteractorObject interactableObject;
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();
            this.interactableObject.OnInteract += this.OnInteract;
        }
        #endregion

        #region Helpers
        private void OnInteract(object sender, Interactable interactable) {
            // TODO: block movement
            StartCoroutine(this.InteractionAnimation(0.5f));
        }

        private IEnumerator InteractionAnimation(float totalTime) {
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
        }
        #endregion
    }
}
