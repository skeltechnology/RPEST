using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    public class WalkableAnimator : SpriteAnimator {
        #region Fields
        [SerializeField] private DirectedAnimation walkableAnimation;
        [SerializeField] protected WalkableObject walkableObject;
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();
            this.walkableObject.OnFinishedMovement += this.OnFinishedMovement;
            this.walkableObject.OnUpdateMovement += this.OnUpdateMovement;
            this.walkableObject.OnUpdateDirection += this.OnUpdateDirection;
        }
        #endregion

        #region Helpers
        private void OnFinishedMovement(object sender, System.EventArgs e) {
            this.UpdateSprite(0f);
        }

        private void OnUpdateMovement(object sender, float progress) {
            this.UpdateSprite(progress);
        }

        private void OnUpdateDirection(object sender, Vector3Int direction) {
            SpriteAnimation animation = this.walkableAnimation.GetAnimation(direction);
            this.SetAnimation(animation);
            this.UpdateSprite(0f);
        }
        #endregion
    }
}
