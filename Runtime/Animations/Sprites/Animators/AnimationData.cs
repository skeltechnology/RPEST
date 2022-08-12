using System.Collections;

namespace SkelTech.RPEST.Animations.Sprites.Animators {
    public class AnimationData {
        #region Properties
        public IEnumerator Coroutine { get; private set; }
        public string Tag { get; private set; }
        public bool IsFinished { get; set; } 
        #endregion

        #region Constructors
        public AnimationData(IEnumerator coroutine, string tag) {
            this.Coroutine = coroutine;
            this.Tag = tag;
            this.IsFinished = false;
        }
        #endregion

        #region Helpers
        public AnimationData Copy() {
            return new AnimationData(this.Coroutine, this.Tag);
        }
        #endregion
    }
}