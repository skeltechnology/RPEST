using System;
using System.Collections;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    [Serializable]
    /// <summary>
    /// Base class for animator components.
    /// </summary>
    public abstract class WorldObjectAnimatorComponent {
        #region Fields
        /// <summary>
        /// Reference to the animator that manages this component.
        /// </summary>
        [SerializeReference, HideInInspector] protected WorldObjectAnimator animator;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the animator component.
        /// </summary>
        /// <param name="animator">Animator that manages this component.</param>
        public WorldObjectAnimatorComponent(WorldObjectAnimator animator) {
            this.animator = animator;
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Method responsible for initializing the component.
        /// Every listening operation should be performed here.
        /// This method is called as soon as the animator is created.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Method responsible for disabling the necessary operations.
        /// Every unlistening operation should be performed here.
        /// This mehod is called as soon as the animator is destroyed.
        /// </summary>
        public abstract void Disable();
        #endregion

        #region Helpers
        /// <summary>
        /// Coroutine that play an animation during the given amount of time.
        /// </summary>
        /// <param name="animation">Animation that will be played.</param>
        /// <param name="duration">Duration of the animation.</param>
        protected IEnumerator AnimationCoroutine(SpriteAnimation animation, float duration) {
            this.animator.PushSprite();
            this.animator.SetAnimation(animation);

            float time = 0;
            while (time < duration) {
                time += Time.deltaTime;
                this.animator.UpdateSprite(time / duration);
                yield return null;
            }

            this.animator.LoadSpriteFromStack();
            this.animator.PopSprite();
        }
        #endregion
    }
}
