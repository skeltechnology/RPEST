using SkelTech.RPEST.World.Elements.Objects;
using SkelTech.RPEST.Utilities.Structures;

using System.Collections;

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
        public StandingAnimatorComponent(WorldObjectAnimator animator) : base(animator, "STANDING") {}
        #endregion

        #region Initialization
        protected override void Initialize() {
            this.walkableObject.OnStartedMovement += this.OnStartedMovement;
            this.walkableObject.OnFinishedMovement += this.OnFinishedMovement;
            this.walkableObject.OnUpdateDirection += this.OnUpdateDirection;

            if (!this.walkableObject.IsMoving) this.StartStandingAnimation();
        }
        protected override void Disable() {
            this.walkableObject.OnStartedMovement -= this.OnStartedMovement;
            this.walkableObject.OnFinishedMovement -= this.OnFinishedMovement;
            this.walkableObject.OnUpdateDirection -= this.OnUpdateDirection;

            if (!this.walkableObject.IsMoving) this.animator.StopAnimation(this.tag);
        }
        #endregion

        #region Helpers
        // TODO: DOCUMENTATION
        private void OnStartedMovement(object sender, System.EventArgs e) {
            this.animator.StopAnimation(this.tag);

        }

        private void OnFinishedMovement(object sender, System.EventArgs e) {
            this.StartStandingAnimation();
        }

        private void OnUpdateDirection(object sender, Direction direction) {
            if (direction != this.standingDirection && !this.walkableObject.IsMoving) {
                this.animator.StopAnimation();
                this.StartStandingAnimation(direction);
            }
        }

        private void StartStandingAnimation() {
            this.StartStandingAnimation(this.walkableObject.GetDirection());
        }

        private void StartStandingAnimation(Direction direction) {
            this.standingDirection = direction;
            SpriteAnimation spriteAnimation = this.standingAnimation.GetAnimation(direction);
            IEnumerator coroutine = this.AnimationLoopCoroutine(spriteAnimation, this.loopDuration);
            this.animator.StartAnimation(new AnimationData(coroutine, this.tag), false);
        }
        #endregion
    }
}
