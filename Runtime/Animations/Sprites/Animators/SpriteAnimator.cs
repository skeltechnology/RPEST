using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Animations.Sprites.Animators {
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour {
        #region Properties
        public bool IsAnimating { get; private set; } = false;
        #endregion

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

        #region Getters
        public SpriteRenderer GetRenderer() {
            return this.renderer;
        }
        #endregion

        #region Setters
        public void SetAnimation(SpriteAnimation animation) {
            this.animation = animation.GetSprites();
        }

        public void SetSprite(Sprite sprite) {
            if (this.renderer.sprite != sprite)
                this.renderer.sprite = sprite;
        }
        #endregion

        #region Operators
        public void Animate(IEnumerator coroutine) {
            if (!this.IsAnimating)
                this.StartCoroutine(this.AnimateCoroutine(coroutine));
        }

        public void UpdateSprite(float progress) {
            if (animation != null) {
                progress = Mathf.Clamp01(progress);
                int index = Mathf.FloorToInt(this.animation.Length * progress);
                index = Mathf.Clamp(index, 0, this.animation.Length - 1);

                this.SetSprite(this.animation[index]);
            }
        }

        public void PushSprite() {
            Sprite sprite = this.renderer.sprite;
            if (sprite != null)
                this.stack.Push(sprite);
        }

        public void PopSprite() {
            if (this.stack.Count > 0)
                this.stack.Pop();
        }

        public void LoadSpriteFromStack() {
            if (this.stack.Count > 0)
                this.renderer.sprite = this.stack.Peek();
        }
        #endregion

        #region Helpers
        private IEnumerator AnimateCoroutine(IEnumerator coroutine) {
            this.IsAnimating = true;
            yield return this.StartCoroutine(coroutine);
            this.IsAnimating = false;
        }
        #endregion
    }
}
