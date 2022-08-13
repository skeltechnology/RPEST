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

        // TODO: DOCUMENTATION
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
        // TODO: DOCUMENTATION
        public void Pause() {
            if (!this.IsInitialized) return;

            this.IsInitialized = false;
            this.Disable();
        }

        public void Play() {
            if (this.IsInitialized) return;

            this.IsInitialized = true;
            this.Initialize();
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

        // TODO: DOCUMENTATION
        protected IEnumerator AnimationLoopCoroutine(SpriteAnimation animation, float duration) {
            return this.AnimationLoopCoroutine(animation, duration, int.MaxValue);
        }

        protected IEnumerator AnimationLoopCoroutine(SpriteAnimation animation, float duration, int count) {
            while (count > 0) {
                yield return this.AnimationCoroutine(animation, duration);
                --count;
            }
        }
        #endregion
    }
}
