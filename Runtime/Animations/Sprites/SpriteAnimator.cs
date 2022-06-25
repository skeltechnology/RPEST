using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour {
        #region Fields
        private new SpriteRenderer renderer;
        private Sprite[] sprites;

        private int lastIndex;
        #endregion

        #region Unity
        private void Awake() {
            this.renderer = this.GetComponent<SpriteRenderer>();
        }
        #endregion

        #region Setters
        public void SetAnimation(SpriteAnimation animation) {
            this.sprites = animation.GetSprites();
            this.lastIndex = -1;
        }
        #endregion

        #region Operators
        public void UpdateSprite(float progress) {
            if (this.sprites != null) {
                progress = Mathf.Clamp01(progress);
                int index = Mathf.FloorToInt(sprites.Length * progress);
                index = Mathf.Clamp(index, 0, sprites.Length - 1);

                if (index != this.lastIndex) {
                    this.lastIndex = index;
                    this.renderer.sprite = sprites[index];
                }
            }
        }
        #endregion
    }
}
