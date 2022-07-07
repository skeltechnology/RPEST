using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators.Components {
    public class WalkableAnimatorComponent : WorldObjectAnimatorComponent {
        #region Fields
        [SerializeField] private DirectedAnimation walkableAnimation;
        [SerializeField] protected WalkableObject walkableObject;
        #endregion

        #region Constructors
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
        private void OnFinishedMovement(object sender, System.EventArgs e) {
            this.animator.UpdateSprite(0f);
        }

        private void OnUpdateMovement(object sender, float progress) {
            this.animator.UpdateSprite(progress);
        }

        private void OnUpdateDirection(object sender, Vector3Int direction) {
            SpriteAnimation animation = this.walkableAnimation.GetAnimation(direction);
            this.animator.SetAnimation(animation);
            this.animator.UpdateSprite(0f);
        }
        #endregion
    }
}
