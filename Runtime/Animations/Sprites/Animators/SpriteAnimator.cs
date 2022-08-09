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
        public bool IsAnimating { get { return this.animationCoroutine != null; }}
        #endregion

        #region Fields
        /// <summary>
        /// Reference to the sprite renderer that will be animated.
        /// </summary>
        private new SpriteRenderer renderer;

        /// <summary>
        /// Sprite collection that is currently being used to animate.
        /// </summary>
        private new Sprite[] animation;

        /// <summary>
        /// Stack used to push and pop sprites, facilitating animations to the programmer.
        /// </summary>
        private Stack<Sprite> stack = new Stack<Sprite>();

        // TODO: DOCUMENTATION
        private IEnumerator animationCoroutine = null;
        #endregion

        #region Unity
        protected virtual void Awake() {
            this.renderer = this.GetComponent<SpriteRenderer>();
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the sprite renderer of the animator.
        /// </summary>
        /// <returns>Sprite renderer of the animator.</returns>
        public SpriteRenderer GetRenderer() {
            return this.renderer;
        }
        #endregion

        #region Setters
        /// <summary>
        /// Sets a new collection of sprites that can be used to animate.
        /// </summary>
        /// <param name="animation"><c>SpriteAnimation</c> that contains a collection of sprites.</param>
        public void SetAnimation(SpriteAnimation animation) {
            this.animation = animation.GetSprites();
        }

        /// <summary>
        /// Changes the current sprite of the <c>SpriteRenderer</c>.
        /// </summary>
        /// <param name="sprite"></param>
        public void SetSprite(Sprite sprite) {
            if (this.renderer.sprite != sprite)
                this.renderer.sprite = sprite;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Animates the <c>SpriteRenderer</c>, by starting the given coroutine.
        /// The coroutine is only executed of the animator is not animating.
        /// </summary>
        /// <param name="coroutine">Coroutine that will be executed.</param>
        public void StartAnimation(IEnumerator coroutine) {
            if (!this.IsAnimating)
                this.StartCoroutine(this.AnimateCoroutine(coroutine));
        }

        // TODO: DOCUMENTATION
        public void StopAnimation() {
            if (this.IsAnimating && this.animationCoroutine != null) {
                //Debug.Log(this.animationCoroutine);
                this.StopCoroutine(this.animationCoroutine);
                this.animationCoroutine = null;
            }
        }

        /// <summary>
        /// Updates the <c>SpriteRenderer></c> with a sprite in the sprite collection, based on a certain progress.
        /// </summary>
        /// <param name="progress">Progress in percentage, between 0 and 1.</param>
        public void UpdateSprite(float progress) {
            if (animation != null) {
                progress = Mathf.Clamp01(progress);
                int index = Mathf.FloorToInt(this.animation.Length * progress);
                index = Mathf.Clamp(index, 0, this.animation.Length - 1);

                this.SetSprite(this.animation[index]);
            }
        }

        /// <summary>
        /// Pushes the current sprite of the <c>SpriteRenderer</c> to the stack.
        /// </summary>
        public void PushSprite() {
            Sprite sprite = this.renderer.sprite;
            if (sprite != null)
                this.stack.Push(sprite);
        }

        /// <summary>
        /// Pops the sprite at the top of the stack.
        /// </summary>
        public void PopSprite() {
            if (this.stack.Count > 0)
                this.stack.Pop();
        }

        /// <summary>
        /// Loads the sprite that is at the top of the stack to the <c>SpriteRenderer</c>.
        /// </summary>
        public void LoadSpriteFromStack() {
            if (this.stack.Count > 0)
                this.renderer.sprite = this.stack.Peek();
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Helper coroutine that is used to execute an animation coroutine.
        /// </summary>
        /// <param name="coroutine">Coroutine that will be executed</param>
        private IEnumerator AnimateCoroutine(IEnumerator coroutine) {
            this.animationCoroutine = coroutine;
            yield return this.StartCoroutine(coroutine);
            this.animationCoroutine = null;
        }
        #endregion
    }
}
