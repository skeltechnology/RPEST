using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    [CreateAssetMenu(fileName = "SpriteAnimation", menuName = "RPEST/Animations/Sprites/SpriteAnimation")]
    public class SpriteAnimation : ScriptableObject {
        #region Fields
        [SerializeField] private Sprite[] sprites;
        #endregion

        #region Getters
        public Sprite[] GetSprites() {
            return this.sprites;
        }
        #endregion
    }
}
