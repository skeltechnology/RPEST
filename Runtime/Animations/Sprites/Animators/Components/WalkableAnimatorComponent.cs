using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    [System.Serializable]
    /// <summary>
    /// Component responsible for animating a <c>WalkableObject</c>.
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
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the walkable animator component.
        /// </summary>
        /// <param name="animator">Animator that manages this component.</param>
        public WalkableAnimatorComponent(WorldObjectAnimator animator) : base(animator) {}
        #endregion

        #region Initialization
        public override void Initialize() {
            this.walkableObject.OnFinishedMovement += this.OnFinishedMovement;
            this.walkableObject.OnUpdateMovement += this.OnUpdateMovement;
            this.walkableObject.OnUpdateDirection += this.OnUpdateDirection;
        }
        public override void Disable() {
            this.walkableObject.OnFinishedMovement -= this.OnFinishedMovement;
            this.walkableObject.OnUpdateMovement -= this.OnUpdateMovement;
            this.walkableObject.OnUpdateDirection -= this.OnUpdateDirection;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Callback responsible for updating the sprite at the end of the movement.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="e">Callback arguments.</param>
        private void OnFinishedMovement(object sender, System.EventArgs e) {
            this.animator.UpdateSprite(0f);
        }

        /// <summary>
        /// Callback responsible for updating the sprite while the walkable object moves.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="progress">Progress in percentage, between 0 and 1, of the walking.</param>
        private void OnUpdateMovement(object sender, float progress) {
            this.animator.UpdateSprite(progress);
        }

        /// <summary>
        /// Callback responsible for updating the sprite when the walkable object changes its direction.
        /// </summary>
        /// <param name="sender">Sender of the callback.</param>
        /// <param name="direction">Direction of the walkable object.</param>
        private void OnUpdateDirection(object sender, Vector3Int direction) {
            SpriteAnimation animation = this.walkableAnimation.GetAnimation(direction);
            this.animator.SetAnimation(animation);
            this.animator.UpdateSprite(0f);
        }
        #endregion
    }
}
