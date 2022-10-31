using SkelTech.RPEST.World.Elements.Objects;
using SkelTech.RPEST.Utilities.Structures;

using System.Collections;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    /// <summary>
    /// Component responsible for animating a <c>WalkableObject</c> that is not moving.
    /// </summary>
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

        /// <summary>
        /// Duration of a single animation cycle.
        /// </summary>
        [SerializeField] private float loopDuration = 0.5f;

        /// <summary>
        /// Direction of the most recent standing animation played.
        /// </summary>
        private Direction standingDirection;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the standing animator component.
        /// </summary>
        /// <param name="animator">Animator that manages this component.</param>
        public StandingAnimatorComponent(WorldObjectAnimator animator) : base(animator, "STANDING") {}
        #endregion

        #region Initialization
        protected override void Initialize() {
            this.walkableObject.OnFinishedMovement += this.OnFinishedMovement;
            this.walkableObject.OnUpdateDirection += this.OnUpdateDirection;

            if (!this.walkableObject.IsMoving) this.StartStandingAnimation();
        }
        protected override void Disable() {
            this.walkableObject.OnFinishedMovement -= this.OnFinishedMovement;
            this.walkableObject.OnUpdateDirection -= this.OnUpdateDirection;

            if (!this.walkableObject.IsMoving) this.animator.StopAnimation(this.tag);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Callback responsible for updating the animation when the walkable object finishes its movement.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="e">Callback arguments.</param>
        private void OnFinishedMovement(object sender, System.EventArgs e) {
            this.animator.StopAnimation(this.tag);
            this.StartStandingAnimation();
        }

        /// <summary>
        /// Callback responsible for updating the animation when the walkable object changes its direction.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="direction">Direction of the walkable object.</param>
        private void OnUpdateDirection(object sender, Direction direction) {
            if (direction != this.standingDirection && !this.walkableObject.IsMoving) {
                this.animator.StopAnimation(this.tag);
                this.StartStandingAnimation(direction);
            }
        }

        /// <summary>
        /// Helper method that starts the standing animation in the current direction of the walkable object.
        /// </summary>
        private void StartStandingAnimation() {
            this.StartStandingAnimation(this.walkableObject.GetDirection());
        }

        /// <summary>
        /// Helper method that starts the standing animation in the given direction.
        /// </summary>
        /// <param name="direction">Direction of the animation.</param>
        private void StartStandingAnimation(Direction direction) {
            this.standingDirection = direction;
            SpriteAnimation spriteAnimation = this.standingAnimation.GetAnimation(direction);
            IEnumerator coroutine = this.AnimationLoopCoroutine(spriteAnimation, this.loopDuration);
            this.animator.StartAnimation(new AnimationData(coroutine, this.tag), false);
        }
        #endregion
    }
}
