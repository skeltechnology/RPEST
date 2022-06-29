using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour {
        #region Fields
        private new SpriteRenderer renderer;
        private new Sprite[] animation;
        #endregion

        #region Unity
        protected virtual void Awake() {
            this.renderer = this.GetComponent<SpriteRenderer>();
        }
        #endregion

        #region Setters
        protected void SetAnimation(SpriteAnimation animation) {
            this.animation = animation.GetSprites();
        }
        #endregion

        #region Operators
        protected void UpdateSprite(float progress) {
            if (animation != null) {
                progress = Mathf.Clamp01(progress);
                int index = Mathf.FloorToInt(this.animation.Length * progress);
                index = Mathf.Clamp(index, 0, this.animation.Length - 1);

                if (this.renderer.sprite != this.animation[index])
                    this.renderer.sprite = this.animation[index];
            }
        }
        #endregion
    }
}
