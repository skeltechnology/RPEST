using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    // TODO: DOCUMENTATION
    public interface Pausable {
        #region Operators
        public void Pause();

        public void Play();

        public void SetPause(bool paused) {
            if (paused) this.Pause();
            else this.Play();
        }
        #endregion
    }
}
