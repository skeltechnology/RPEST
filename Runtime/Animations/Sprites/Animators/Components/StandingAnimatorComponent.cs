using SkelTech.RPEST.World.Elements.Objects;
using SkelTech.RPEST.Utilities.Structures;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    // TODO: DOCUMENTATION
    public class StandingAnimatorComponent : WorldObjectAnimatorComponent {
        #region Fields
        /// <summary>
        /// Walkable object that the animator component will be listening to.
        /// </summary>
        [SerializeField] protected WalkableObject walkableObject;

        /// <summary>
        /// Animation that will be played when the walkable object stays still.
        /// </summary>
        [SerializeField] private DirectedAnimation standingAnimation;

        [SerializeField] private float loopDuration = 0.5f;

        private Direction standingDirection;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the standing animator component.
        /// </summary>
        /// <param name="animator">Animator that manages this component.</param>
        public StandingAnimatorComponent(WorldObjectAnimator animator) : base(animator) {}
        #endregion

        #region Initialization
        public override void Initialize() {
            this.walkableObject.OnStartedMovement += this.OnStartedMovement;
            this.walkableObject.OnFinishedMovement += this.OnFinishedMovement;
            this.walkableObject.OnUpdateDirection += this.OnUpdateDirection;

            if (!this.walkableObject.IsMoving) this.OnFinishedMovement(null, System.EventArgs.Empty); // TODO: HELPER FUNC
        }
        public override void Disable() {
            this.walkableObject.OnStartedMovement -= this.OnStartedMovement;
            this.walkableObject.OnFinishedMovement -= this.OnFinishedMovement;
            this.walkableObject.OnUpdateDirection -= this.OnUpdateDirection;
        }
        #endregion

        #region Helpers
        // TODO: DOCUMENTATION
        private void OnStartedMovement(object sender, System.EventArgs e) {
            Debug.Log("STARTED MOVING");
            this.animator.StopAnimation();
            // TODO: CONFLICTS WITH INTERACTION.

        }

        private void OnFinishedMovement(object sender, System.EventArgs e) {
            Debug.Log("STOPPED MOVING");
            this.standingDirection = this.walkableObject.GetDirection();
            SpriteAnimation spriteAnimation = this.standingAnimation.GetAnimation(this.standingDirection);
            IEnumerator coroutine = this.AnimationLoopCoroutine(spriteAnimation, this.loopDuration);
            this.animator.StartAnimation(coroutine);
        }

        private void OnUpdateDirection(object sender, Direction direction) {
            // if not walking, then start standing animation with new direction
            if (direction != this.standingDirection) {
                this.animator.StopAnimation();

                this.standingDirection = direction;
                SpriteAnimation spriteAnimation = this.standingAnimation.GetAnimation(this.standingDirection);
                IEnumerator coroutine = this.AnimationLoopCoroutine(spriteAnimation, this.loopDuration);
                this.animator.StartAnimation(coroutine);
            }
        }
        #endregion
    }
}
