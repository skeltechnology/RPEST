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

        /// <summary>
        /// Sprite collection that is currently being used to animate.
        /// </summary>
        private Sprite[] spriteAnimation;

        /// <summary>
        /// Stack used to push and pop sprites, facilitating animations to the programmer.
        /// </summary>
        private Stack<Sprite> stack = new Stack<Sprite>();

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
        /// Sets a new collection of sprites that can be used to animate.
        /// </summary>
        /// <param name="animation"><c>SpriteAnimation</c> that contains a collection of sprites.</param>
        public void SetSpriteAnimation(SpriteAnimation animation) {
            this.spriteAnimation = animation.GetSprites();
        }

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
            if (!this.IsAnimating) {
                this.animations.AddFirst(animationData);
                this.StartCoroutine(this.AnimationCoroutine());
            } else if (force) {
                AnimationData currentAnimation = this.animations.First.Value;
                currentAnimation.IsFinished = true;

                LinkedListNode<AnimationData> firstNode = this.animations.First;
                this.animations.AddAfter(firstNode, currentAnimation.Copy());
                this.animations.AddAfter(firstNode, animationData);

                this.StopCoroutine(currentAnimation.Coroutine);
            }
        }

        // TODO: DOCUMENTATION
        public void StopAnimation() {
            if (this.IsAnimating) {
                AnimationData animation = this.animations.First.Value;
                this.animations.RemoveFirst();
                animation.IsFinished = true;
                this.StopCoroutine(animation.Coroutine);
            }
        }
        // TODO: STOP ALL ANIMATION
        // TODO: STOP ANIMATION BY TAG

        /// <summary>
        /// Updates the <c>SpriteRenderer></c> with a sprite in the sprite collection, based on a certain progress.
        /// </summary>
        /// <param name="progress">Progress in percentage, between 0 and 1.</param>
        public void UpdateSprite(float progress) {
            if (this.spriteAnimation != null) {
                progress = Mathf.Clamp01(progress);
                int index = Mathf.FloorToInt(this.spriteAnimation.Length * progress);
                index = Mathf.Clamp(index, 0, this.spriteAnimation.Length - 1);

                this.SetSprite(this.spriteAnimation[index]);
            }
        }

        /// <summary>
        /// Pushes the current sprite of the <c>SpriteRenderer</c> to the stack.
        /// </summary>
        public void PushSprite() {
            Sprite sprite = this.spriteRenderer.sprite;
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
                this.spriteRenderer.sprite = this.stack.Peek();
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
                animation = this.animations.First.Value;
                this.StartCoroutine(this.AnimationWrapperCoroutine(animation));
                yield return new WaitUntil(() => {
                    return animation.IsFinished;
                });
                if (this.animations.Count > 0)
                    this.animations.RemoveFirst();
            }
        }

        private IEnumerator AnimationWrapperCoroutine(AnimationData animation) {
            animation.IsFinished = false;
            yield return this.StartCoroutine(animation.Coroutine);
            animation.IsFinished = true;

        }
        #endregion
    }
}
