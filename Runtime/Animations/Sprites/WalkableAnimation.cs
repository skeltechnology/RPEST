using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    [CreateAssetMenu(fileName = "WalkableAnimation", menuName = "RPEST/Animations/Sprites/WalkableAnimation")]
    public class WalkableAnimation : ScriptableObject {
        #region Fields
        [SerializeField] private SpriteAnimation upAnimation, downAnimation, leftAnimation, rightAnimation;
        #endregion

        #region Getters
        public SpriteAnimation GetUpAnimation() {
            return this.upAnimation;
        }

        public SpriteAnimation GetDownAnimation() {
            return this.downAnimation;
        }

        public SpriteAnimation GetLeftAnimation() {
            return this.leftAnimation;
        }

        public SpriteAnimation GetRightAnimation() {
            return this.rightAnimation;
        }
        #endregion
    }
}
