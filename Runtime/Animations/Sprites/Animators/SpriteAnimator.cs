using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites {
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour {
        #region Fields
        private new SpriteRenderer renderer;
        private new Sprite[] animation;
        private Stack<Sprite> stack = new Stack<Sprite>();
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

        protected void SetSprite(Sprite sprite) {
            if (this.renderer.sprite != sprite)
                this.renderer.sprite = sprite;
        }
        #endregion

        #region Operators
        protected void UpdateSprite(float progress) {
            if (animation != null) {
                progress = Mathf.Clamp01(progress);
                int index = Mathf.FloorToInt(this.animation.Length * progress);
                index = Mathf.Clamp(index, 0, this.animation.Length - 1);

                this.SetSprite(this.animation[index]);
            }
        }

        protected void PushSprite() {
            Sprite sprite = this.renderer.sprite;
            if (sprite != null)
                this.stack.Push(sprite);
        }

        protected void PopSprite() {
            if (this.stack.Count > 0)
                this.stack.Pop();
        }

        protected void LoadSpriteFromStack() {
            if (this.stack.Count > 0)
                this.renderer.sprite = this.stack.Peek();
        }
        #endregion
    }
}
