using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    /// <summary>
    /// <c>ScriptableObject</c> that stores a collection of sprites, that are used to create an animation.
    /// </summary>
    [CreateAssetMenu(fileName = "SpriteAnimation", menuName = "RPEST/Animations/Sprites/SpriteAnimation")]
    public class SpriteAnimation : ScriptableObject {
        #region Fields
        /// <summary>
        /// Collection of sprites.
        /// </summary>
        /// <returns></returns>
        [SerializeField] private Sprite[] sprites;
        #endregion

        #region Getters
        /// <summary>
        /// Gets the collection of sprites.
        /// </summary>
        /// <returns>Collection of sprites.</returns>
        public Sprite[] GetSprites() {
            return this.sprites;
        }
        #endregion
    }
}
