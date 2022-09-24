using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkelTech.RPEST.Utilities.Structures {
    /// <summary>
    /// Interface that represents a class that can be paused and unpaused (played).
    /// </summary>
    public interface Pausable {
        #region Operators
        /// <summary>
        /// Pauses the class operations.
        /// </summary>
        /// <returns>Boolean indicating if the class operations were paused.</returns>
        public bool Pause();

        /// <summary>
        /// Plays (unpauses) the class operations.
        /// </summary>
        /// <returns>Boolean indicating if the class operations were played.</returns>
        public bool Play();

        /// <summary>
        /// Changes the pause state to the given one.
        /// </summary>
        /// <param name="paused">Boolean indicating if the class will be paused.</param>
        /// <returns>Boolean indicating if the class operations were paused or played successfully.</returns>
        public bool SetPause(bool paused) { return paused ? this.Pause() : this.Play(); }
        #endregion
    }
}
