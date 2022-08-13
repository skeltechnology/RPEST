using System.Collections;

namespace SkelTech.RPEST.Animations.Sprites.Animators {
    public enum AnimationStatus { Waiting, Animating, Canceled, Finished }

    public class AnimationData {
        #region Properties
        public IEnumerator Coroutine { get; private set; }
        public string Tag { get; private set; }
        public AnimationStatus Status { get; set; }
        #endregion

        #region Constructors
        public AnimationData(IEnumerator coroutine, string tag) {
            this.Coroutine = coroutine;
            this.Tag = tag;
            this.Status = AnimationStatus.Waiting;
        }
        #endregion

        #region Helpers
        public AnimationData Copy() {
            return new AnimationData(this.Coroutine, this.Tag);
        }
        #endregion
    }
}