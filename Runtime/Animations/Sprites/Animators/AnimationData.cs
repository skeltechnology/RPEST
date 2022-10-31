using SkelTech.RPEST.Utilities.Structures;
using System.Collections;

namespace SkelTech.RPEST.Animations.Sprites.Animators {
    /// <summary>
    /// Model to store the necessary information about the animation.
    /// </summary>
    public class AnimationData {
        #region Properties
        /// <summary>
        /// Reference to the custom RPEST Coroutine that will play the animation.
        /// </summary>
        public RPESTCoroutine Coroutine { get; private set; }

        /// <summary>
        /// Tag that identifies the animation.
        /// Must be unique for each animation type.
        /// </summary>
        public string Tag { get; private set; }
        
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the <c>AnimationData</c>.
        /// </summary>
        /// <param name="coroutine"><c>IEnumerator</c> that contains the animation.</param>
        /// <param name="tag">Tag of the animation.</param>
        public AnimationData(IEnumerator coroutine, string tag) : this(new RPESTCoroutine(coroutine), tag) {}

        /// <summary>
        /// Constructor of the <c>AnimationData</c>.
        /// </summary>
        /// <param name="coroutine"><c>RPESTCoroutine</c> that contains the animation.</param>
        /// <param name="tag">Tag of the animation.</param>
        public AnimationData(RPESTCoroutine coroutine, string tag) {
            this.Coroutine = coroutine;
            this.Tag = tag;
        }
        #endregion
    }
}