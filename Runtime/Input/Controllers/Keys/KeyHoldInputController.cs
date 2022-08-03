using System.Collections.Generic;

using UnityEngine;

namespace SkelTech.RPEST.Input.Controllers.Keys {
    /// <summary>
    /// Class for managing KeyHold inputs.
    /// </summary>
    public class KeyHoldInputController : KeyInputController {
        #region Fields
        // TODO: DOCUMENT
        private LinkedList<KeyCode> activeKeys = new LinkedList<KeyCode>();
        #endregion

        #region Getters
        protected override bool IsInputKeyActive(KeyCode key) {
            return this.activeKeys.Contains(key);
        }

        protected override ICollection<KeyCode> GetInputEvents() {
            foreach (KeyCode key in this.listenedKeys) {
                if (UnityEngine.Input.GetKeyDown(key)) {  // Key pressed
                    this.activeKeys.AddFirst(key);
                } else if (UnityEngine.Input.GetKeyUp(key)) {  // Key released
                    this.activeKeys.Remove(key);
                }
            }

            return this.activeKeys;
        }
        #endregion
    }
}
