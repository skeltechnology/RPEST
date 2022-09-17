using SkelTech.RPEST.Utilities.Structures;
using System.Collections;

// TODO: DOCUMENTATION
namespace SkelTech.RPEST.Animations.Sprites.Animators {
    public class AnimationData {
        #region Properties
        public RPESTCoroutine Coroutine { get; private set; }
        public string Tag { get; private set; }
        
        #endregion

        #region Constructors
        public AnimationData(IEnumerator coroutine, string tag) : this(new RPESTCoroutine(coroutine), tag) {}

        public AnimationData(RPESTCoroutine coroutine, string tag) {
            this.Coroutine = coroutine;
            this.Tag = tag;
        }
        #endregion

        #region Helpers
        // TODO: CHECK IF NEEDED
        public AnimationData Copy() {
            return new AnimationData(this.Coroutine, this.Tag);
        }
        #endregion
    }
}