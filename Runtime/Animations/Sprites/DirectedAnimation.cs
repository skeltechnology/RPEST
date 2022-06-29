using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    [CreateAssetMenu(fileName = "DirectedAnimation", menuName = "RPEST/Animations/Sprites/DirectedAnimation")]
    public class DirectedAnimation : ScriptableObject {
        #region Fields
        [SerializeField] private SpriteAnimation upAnimation, downAnimation, leftAnimation, rightAnimation;
        #endregion

        #region Getters
        public SpriteAnimation GetAnimation(Vector3Int direction){
            if (direction == Vector3Int.up) return this.GetUpAnimation();
            else if (direction == Vector3Int.down) return this.GetDownAnimation();
            else if (direction == Vector3Int.left) return this.GetLeftAnimation();
            else if (direction == Vector3Int.right) return this.GetRightAnimation();
            return null;
        }

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
