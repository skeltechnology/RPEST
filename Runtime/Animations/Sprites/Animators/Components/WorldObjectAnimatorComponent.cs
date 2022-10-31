using SkelTech.RPEST.Utilities.Structures;

using System;
using System.Collections;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    [Serializable]
    /// <summary>
    /// Base class for animator components.
    /// </summary>
    public abstract class WorldObjectAnimatorComponent : Pausable {
        #region Properties
        public bool IsInitialized { get; private set; } = false;
        #endregion

        #region Fields
        /// <summary>
        /// Reference to the animator that manages this component.
        /// </summary>
        [SerializeReference, HideInInspector] protected WorldObjectAnimator animator;

        /// <summary>
        /// Tag that identifies the animator component.
        /// Must be unique for each component type.
        /// </summary>
        [SerializeField, HideInInspector] protected string tag;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the animator component.
        /// </summary>
        /// <param name="animator">Animator that manages this component.</param>
        public WorldObjectAnimatorComponent(WorldObjectAnimator animator, string tag) {
            this.animator = animator;
            this.tag = tag;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Pauses the execution of the component.
        /// </summary>
        /// <returns>Boolean indicating if the component execution was paused.</returns>
        public bool Pause() {
            if (!this.IsInitialized) return false;;

            this.IsInitialized = false;
            this.Disable();
            return true;
        }

        /// <summary>
        /// Plays the execution of the component.
        /// </summary>
        /// <returns>Boolean indicating if the component execution was played.</returns>
        public bool Play() {
            if (this.IsInitialized) return false;;

            this.IsInitialized = true;
            this.Initialize();
            return true;
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Method responsible for initializing the component.
        /// Every listening operation should be performed here.
        /// This method is called as soon as the animator is created.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Method responsible for disabling the necessary operations.
        /// Every unlistening operation should be performed here.
        /// This mehod is called as soon as the animator is destroyed.
        /// </summary>
        protected abstract void Disable();
        #endregion

        #region Helpers
        /// <summary>
        /// Coroutine that play an animation during the given amount of time.
        /// </summary>
        /// <param name="animation">Animation that will be played.</param>
        /// <param name="duration">Duration of the animation.</param>
        protected IEnumerator AnimationCoroutine(SpriteAnimation animation, float duration) {
            Sprite[] sprites = animation.GetSprites();
            float time = 0;
            while (time < duration) {
                time += Time.deltaTime;
                this.animator.UpdateSprite(sprites, time / duration);
                yield return null;
            }
        }

        /// <summary>
        /// Coroutine that animates the given animation for a certain durating infinitely.
        /// </summary>
        /// <param name="animation"><c>SpriteAnimation</c> to be played.</param>
        /// <param name="duration">Duration of a single animation.</param>
        protected IEnumerator AnimationLoopCoroutine(SpriteAnimation animation, float duration) {
            return this.AnimationLoopCoroutine(animation, duration, int.MaxValue);
        }

        /// <summary>
        /// Coroutine that animates the given animation for a certain durating and the specified amount of times.
        /// </summary>
        /// <param name="animation"><c>SpriteAnimation</c> to be played.</param>
        /// <param name="duration">Duration of a single animation.</param>
        /// <param name="count">Number of times the animation will be played.</param>
        protected IEnumerator AnimationLoopCoroutine(SpriteAnimation animation, float duration, int count) {
            while (count > 0) {
                yield return this.AnimationCoroutine(animation, duration);
                --count;
            }
        }
        #endregion
    }
}
