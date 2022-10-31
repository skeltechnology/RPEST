using SkelTech.RPEST.Utilities.Structures;

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

        /// <summary>
        /// List of animations that will be played by the animator.
        /// </summary>
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
        /// <summary>
        /// Animates the <c>SpriteRenderer</c>, by starting the given coroutine.
        /// The coroutine is only executed of the animator is not animating.
        /// </summary>
        /// <param name="coroutine">Coroutine that will be executed.</param>
        /// <param name="force">Boolean indicating if the animator should pause the current animation (if any) and force the execution of this one.</param>
        public void StartAnimation(AnimationData animationData, bool force) {
            if (this.animations.Count == 0) {
                this.animations.AddFirst(animationData);
                if (!this.IsAnimating) {
                    this.IsAnimating = true;
                    this.StartCoroutine(this.AnimationCoroutine());
                }
            } else if (force) {
                AnimationData currentAnimation = this.animations.First.Value;
                currentAnimation.Coroutine.Pause();

                this.animations.AddFirst(animationData);
            }
        }

        /// <summary>
        /// Stops and removes the current animation.
        /// </summary>
        public void StopAnimation() {
            if (this.IsAnimating) {
                this.StopAnimation(this.animations.First);
            }
        }

        /// <summary>
        /// Stops and removes the first animation that has the given tag
        /// </summary>
        /// <param name="tag"></param>
        public void StopAnimation(string tag) {
            if (this.IsAnimating) {
                for(LinkedListNode<AnimationData> node = this.animations.First; node != null; node = node.Next) {
                    if (node.Value.Tag.Equals(tag)) {
                        this.StopAnimation(node);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Stops and removes the given animation.
        /// </summary>
        /// <param name="node">Node that stores the animation.</param>
        private void StopAnimation(LinkedListNode<AnimationData> node) {
            AnimationData animation = node.Value;
            this.animations.Remove(node);
            animation.Coroutine.Stop();
        }

        /// <summary>
        /// Stops and removes all the animations.
        /// </summary>
        public void StopAllAnimations() {
            // TODO: BUG: NOT WORKING
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
            while (this.animations.Count > 0) {
                animation = this.animations.First.Value;

                if (animation.Coroutine.Status == RPESTCoroutineStatus.Created) animation.Coroutine.Start(this);
                else if (animation.Coroutine.Status == RPESTCoroutineStatus.Paused) animation.Coroutine.Play();

                // Wait while coroutine is running
                yield return animation.Coroutine;

                if (animation.Coroutine.Status == RPESTCoroutineStatus.Finished) {
                    if (this.animations.Count > 0) this.animations.RemoveFirst();
                }
            }

            this.IsAnimating = false;
        }
        #endregion
    }
}
