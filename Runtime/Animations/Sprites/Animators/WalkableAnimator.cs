using SkelTech.RPEST.World.Elements.Objects;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    public class WalkableAnimator : SpriteAnimator {
        #region Fields
        [SerializeField] private WalkableAnimation walkableAnimation;
        [SerializeField] private WalkableObject worldObject;
        #endregion

        #region Unity
        protected override void Awake() {
            base.Awake();
            this.worldObject.OnFinishedMovement += this.OnFinishedMovement;
            this.worldObject.OnUpdateMovement += this.OnUpdateMovement;
            this.worldObject.OnUpdateDirection += this.OnUpdateDirection;
        }
        #endregion

        #region Getters
        private SpriteAnimation GetAnimation(Vector3Int direction){
            if (direction == Vector3Int.up) return this.walkableAnimation.GetUpAnimation();
            else if (direction == Vector3Int.down) return this.walkableAnimation.GetDownAnimation();
            else if (direction == Vector3Int.left) return this.walkableAnimation.GetLeftAnimation();
            else if (direction == Vector3Int.right) return this.walkableAnimation.GetRightAnimation();
            return null;
        }
        #endregion

        #region Operators
        
        #endregion

        #region Helpers
        private void OnFinishedMovement(object sender, System.EventArgs e) {
            this.UpdateSprite(0f);
        }

        private void OnUpdateMovement(object sender, float progress) {
            this.UpdateSprite(progress);
        }

        private void OnUpdateDirection(object sender, Vector3Int direction) {
            SpriteAnimation animation = this.GetAnimation(direction);
            this.SetAnimation(animation);
            this.UpdateSprite(0f);
        }
        #endregion
    }
}
