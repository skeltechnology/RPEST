using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    /// <summary>
    /// <c>ScriptableObject</c> that stores and manages <c>SpriteAnimation</c>s for every direction (UP, DOWN, LEFT, RIGHT).
    /// </summary>
    [CreateAssetMenu(fileName = "DirectedAnimation", menuName = "RPEST/Animations/Sprites/DirectedAnimation")]
    public class DirectedAnimation : ScriptableObject {
        #region Fields
        /// <summary>
        /// <c>SpriteAnimation</c> for the given direction.
        /// </summary>
        [SerializeField] private SpriteAnimation upAnimation, downAnimation, leftAnimation, rightAnimation;
        #endregion

        #region Getters
        /// <summary>
        /// Gets the correspondent animation for the given direction.
        /// </summary>
        /// <param name="direction">Animation direction.</param>
        /// <returns>Correspondent sprite animation.</returns>
        public SpriteAnimation GetAnimation(Vector3Int direction){
            if (direction == Vector3Int.up) return this.GetUpAnimation();
            else if (direction == Vector3Int.down) return this.GetDownAnimation();
            else if (direction == Vector3Int.left) return this.GetLeftAnimation();
            else if (direction == Vector3Int.right) return this.GetRightAnimation();
            return null;
        }

        /// <summary>
        /// Gets the animation for the UP direction.
        /// </summary>
        /// <returns>Up sprite animation.</returns>
        public SpriteAnimation GetUpAnimation() {
            return this.upAnimation;
        }

        /// <summary>
        /// Gets the animation for the DOWN direction.
        /// </summary>
        /// <returns>Down sprite animation.</returns>
        public SpriteAnimation GetDownAnimation() {
            return this.downAnimation;
        }

        /// <summary>
        /// Gets the animation for the LEFT direction.
        /// </summary>
        /// <returns>Left sprite animation.</returns>
        public SpriteAnimation GetLeftAnimation() {
            return this.leftAnimation;
        }

        /// <summary>
        /// Gets the animation for the RIGHT direction.
        /// </summary>
        /// <returns>Right sprite animation.</returns>
        public SpriteAnimation GetRightAnimation() {
            return this.rightAnimation;
        }
        #endregion
    }
}
