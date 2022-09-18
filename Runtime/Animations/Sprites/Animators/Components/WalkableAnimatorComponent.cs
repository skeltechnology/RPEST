using SkelTech.RPEST.World.Elements.Objects;
using SkelTech.RPEST.Utilities.Structures;

using System.Collections;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    [System.Serializable]
    /// <summary>
    /// Component responsible for animating a <c>WalkableObject</c> while moving.
    /// </summary>
    public class WalkableAnimatorComponent : WorldObjectAnimatorComponent {
        #region Fields
        /// <summary>
        /// Walkable object that the animator component will be listening to.
        /// </summary>
        [SerializeField] protected WalkableObject walkableObject;

        /// <summary>
        /// Animation that will be played when the walkable object walks.
        /// </summary>
        [SerializeField] private DirectedAnimation walkableAnimation;

        /// <summary>
        /// Float that stores the total progress of the cell movement.
        /// </summary>
        private float progress = 0f;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the walkable animator component.
        /// </summary>
        /// <param name="animator">Animator that manages this component.</param>
        public WalkableAnimatorComponent(WorldObjectAnimator animator) : base(animator, "WALKING") {}
        #endregion

        #region Initialization
        protected override void Initialize() {
            this.walkableObject.OnStartedCellMovement += this.OnStartedCellMovement;
            this.walkableObject.OnUpdateMovement += this.OnUpdateMovement;
            this.walkableObject.OnUpdateDirection += this.OnUpdateDirection;
        }
        protected override void Disable() {
            this.walkableObject.OnStartedCellMovement -= this.OnStartedCellMovement;
            this.walkableObject.OnUpdateMovement -= this.OnUpdateMovement;
            this.walkableObject.OnUpdateDirection -= this.OnUpdateDirection;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Callback responsible for updating the sprite at the beginning of the movement.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="e">Callback arguments.</param>
        private void OnStartedCellMovement(object sender, System.EventArgs e) {
            AnimationData animation = new AnimationData(this.WalkingAnimation(this.walkableObject.GetDirection()), this.tag);
            this.progress = 0f;
            this.animator.StartAnimation(animation, true);
        }

        /// <summary>
        /// Callback responsible for updating the sprite while the walkable object moves.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="progress">Progress in percentage, between 0 and 1, of the walking.</param>
        private void OnUpdateMovement(object sender, float progress) {
            this.progress = progress;
        }

        /// <summary>
        /// Callback responsible for updating the sprite when the walkable object changes its direction.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="direction">Direction of the walkable object.</param>
        private void OnUpdateDirection(object sender, Direction direction) {
            SpriteAnimation animation = this.walkableAnimation.GetAnimation(direction);
            this.animator.UpdateSprite(animation.GetSprites(), 0f);
        }

        // TODO: DOCUMENTATION
        private IEnumerator WalkingAnimation(Direction direction) {
            Sprite[] sprites = this.walkableAnimation.GetAnimation(direction).GetSprites();
            while (this.progress < 1f) {
                this.animator.UpdateSprite(sprites, progress);
                yield return null;
            }
            this.animator.UpdateSprite(sprites, 0f);
        }
        #endregion
    }
}
