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
        public bool IsAnimating { get; private set; }
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
        public void SetSprite(Sprite sprite) {
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
            if (this.animations.Count == 0) {
                this.animations.AddFirst(animationData);
                if (!this.IsAnimating) {
                    this.IsAnimating = true;
                    this.StartCoroutine(this.AnimationCoroutine());
                }
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
                this.StopAnimation(this.animations.First);
            }
        }

        public void StopAnimation(string tag) {
            if (this.IsAnimating) {
                if (this.animations.First.Value.Tag.Equals(tag)) {
                    this.StopAnimation();
                } else if (this.animations.Count > 1) {
                    for(LinkedListNode<AnimationData> node = this.animations.First; node != null; node = node.Next) {
                        if (node.Value.Tag.Equals(tag)) {
                            this.StopAnimation(node);
                            break;
                        }
                    }
                }
            }
        }

        private void StopAnimation(LinkedListNode<AnimationData> node) {
            AnimationData animation = node.Value;
            this.animations.Remove(node);

            if (animation.Status == AnimationStatus.Animating) {
                animation.Status = AnimationStatus.Canceled;
                this.StopCoroutine(animation.Coroutine);
            } else {
                animation.Status = AnimationStatus.Canceled;
            }

        }

        public void StopAllAnimations() {
            if (this.IsAnimating) {
                LinkedListNode<AnimationData> first = this.animations.First;
                this.animations.Clear();
                this.StopAnimation(first);
            }
        }

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
            AnimationData animation;
            IEnumerator wrapper;
            while (this.animations.Count > 0) {
                animation = this.animations.First.Value;
                wrapper = this.AnimationWrapperCoroutine(animation);

                this.StartCoroutine(wrapper);
                yield return new WaitWhile(() => {
                    return animation.Status == AnimationStatus.Animating;
                });

                if (animation.Status == AnimationStatus.Finished) {
                    if (this.animations.Count > 0) this.animations.RemoveFirst();
                } else {  // Canceled
                    this.StopCoroutine(wrapper);
                }
            }

            this.IsAnimating = false;
        }

        private IEnumerator AnimationWrapperCoroutine(AnimationData animation) {
            animation.Status = AnimationStatus.Animating;
            yield return this.StartCoroutine(animation.Coroutine);
            animation.Status = AnimationStatus.Finished;

        }
        #endregion
    }
}
