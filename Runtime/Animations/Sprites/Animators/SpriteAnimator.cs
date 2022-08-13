using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators {
    /// <summary>
    /// Base class and <c>MonoBehaviour</c> for creating sprite animators.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class SpriteAnimator : MonoBehaviour {
        #region Properties
        /// <summary>
        /// Boolean indicating if the sprite is currently being animated.
        /// </summary>
        public bool IsAnimating { get { return this.animations.Count > 0; }}
        #endregion

        #region Fields
        /// <summary>
        /// Reference to the sprite renderer that will be animated.
        /// </summary>
        private SpriteRenderer spriteRenderer;

        // TODO: DOCUMENTATION
        private LinkedList<AnimationData> animations = new LinkedList<AnimationData>();
        #endregion

        #region Unity
        protected virtual void Awake() {
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the sprite renderer of the animator.
        /// </summary>
        /// <returns>Sprite renderer of the animator.</returns>
        public SpriteRenderer GetSpriteRenderer() {
            return this.spriteRenderer;
        }
        #endregion

        #region Setters
        /// <summary>
        /// Changes the current sprite of the <c>SpriteRenderer</c>.
        /// </summary>
        /// <param name="sprite"></param>
        private void SetSprite(Sprite sprite) {
            if (this.spriteRenderer.sprite != sprite)
                this.spriteRenderer.sprite = sprite;
        }
        #endregion

        #region Operators
        // TODO: DOCUMENTATE BOOL
        /// <summary>
        /// Animates the <c>SpriteRenderer</c>, by starting the given coroutine.
        /// The coroutine is only executed of the animator is not animating.
        /// </summary>
        /// <param name="coroutine">Coroutine that will be executed.</param>
        public void StartAnimation(AnimationData animationData, bool force) {
            Debug.Log("sta");
            Debug.Log(this.animations.Count);
            if (!this.IsAnimating) {
                this.animations.AddFirst(animationData);
                this.StartCoroutine(this.AnimationCoroutine());
            } else if (force) {
                AnimationData currentAnimation = this.animations.First.Value;
                currentAnimation.Status = AnimationStatus.Canceled;

                this.animations.AddFirst(animationData);

                this.StopCoroutine(currentAnimation.Coroutine);
            }
        }

        // TODO: DOCUMENTATION
        public void StopAnimation() {
            if (this.IsAnimating) {
                AnimationData animation = this.animations.First.Value;
                animation.Status = AnimationStatus.Canceled;
                this.animations.RemoveFirst();
                this.StopCoroutine(animation.Coroutine);
            }
        }
        // TODO: STOP ALL ANIMATION
        // TODO: STOP ANIMATION BY TAG

        /// <summary>
        /// Updates the <c>SpriteRenderer></c> with a sprite in the sprite collection, based on a certain progress.
        /// </summary>
        /// <param name="progress">Progress in percentage, between 0 and 1.</param>
        public void UpdateSprite(Sprite[] sprites, float progress) {
            if (sprites != null) {
                progress = Mathf.Clamp01(progress);
                int index = Mathf.FloorToInt(sprites.Length * progress);
                index = Mathf.Clamp(index, 0, sprites.Length - 1);

                this.SetSprite(sprites[index]);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Helper coroutine that is used to execute the animations at the list.
        /// </summary>
        private IEnumerator AnimationCoroutine() {
            // TODO: ADD NULL VERIFICATION
            AnimationData animation;
            while (this.animations.Count > 0) {
                Debug.Log("beg");
                Debug.Log(this.animations.Count);
                animation = this.animations.First.Value;
                this.StartCoroutine(this.AnimationWrapperCoroutine(animation));
                yield return new WaitWhile(() => {
                    return animation.Status == AnimationStatus.Animating;
                });
                if (this.animations.Count > 0 && animation.Status == AnimationStatus.Finished)
                    this.animations.RemoveFirst();
                Debug.Log("end");
                Debug.Log(this.animations.Count);
            }
        }

        private IEnumerator AnimationWrapperCoroutine(AnimationData animation) {
            animation.Status = AnimationStatus.Animating;
            yield return this.StartCoroutine(animation.Coroutine);
            animation.Status = AnimationStatus.Finished;

        }
        #endregion
    }
}
